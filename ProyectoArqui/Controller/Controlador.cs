﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using ProyectoArqui.Model;
using ProyectoArqui.View;

namespace ProyectoArqui.Controller
{
    /// <summary>
    /// Funciona como mecanismo de sincronizacion entre los diferentes procesadores, actuando como una barrera
    /// Asimismo informa a las vistas cuando ocurre un cambio
    /// </summary>
    class Controlador : Observable
    {
        //Atributos
        private Barrier barrier;
        private Procesador[] procesadores;
        private CacheDatos[] cachesDatos;
        private CacheInstrucciones cacheInstrucciones;
        private Directorio[] directorios;
        private MemoriaPrincipal[] memoriasPrincipales;
        private int ticksReloj;

        /// <summary>
        /// El contructor del controlador.
        /// No inicializa ningun atributo para obligar al programador a llamar 
        /// </summary>
        public Controlador()
        {
            // * IMPORTANTE *
            // No se inicializa ningun atributo para que el programador
            // llame obligatoriamente al metodo inicializa antes de ejecutar la simulacion!
            this.barrier = null;
            this.procesadores = null;
            this.cachesDatos = null;
            this.cacheInstrucciones = null;
            this.memoriasPrincipales = null;
            this.ticksReloj = -1;
        }

        /// <summary>
        /// 
        /// *IMPORTANTE*
        /// 
        /// Este metodo debe ejecutarse antes de iniciar la simulacion!!
        /// 
        /// Este metodo inicializa todos los atributos necesarios para la simulacion
        /// 
        /// </summary>
        /// <param name="procesadores">Procesadores de la simulacion</param>
        /// <param name="cachesDatos">Caches de Datos de la simulacion</param>
        /// <param name="cacheInstrucciones">Cache de instrucciones para todos los procesadores</param>
        /// <param name="memoriasPrincipales">Memorias Principales de todos los procesadores</param>
        public void Inicializar(Procesador[] procesadores, CacheDatos[] cachesDatos, CacheInstrucciones cacheInstrucciones, Directorio[] directorios, MemoriaPrincipal[] memoriasPrincipales)
        {
            int numeroProcesadores = procesadores.Length;
            Debug.WriteLine("Controlador: Creando una barrera para " + numeroProcesadores + "procesadores");
            this.barrier = new Barrier(numeroProcesadores, entreCiclosDeReloj);
            this.procesadores = procesadores;
            this.cachesDatos = cachesDatos;
            this.cacheInstrucciones = cacheInstrucciones;
            this.directorios = directorios;
            this.memoriasPrincipales = memoriasPrincipales;
            this.ticksReloj = 1;
            // Se asigna el programa inicial a cada procesador
            // Por defecto los procesadores deben empezar en finalizado = true para que
            // el siguiente metodo les asigne un programa
            AsignarProgramasAProcesadores();
        }

        /// <summary>
        /// Este es el metodo que los procesadores, cachesDatos y otros objetos llaman
        /// cuando deben Esperar cierta cantidad de ticks de reloj
        /// 
        /// Este es el mecanismo de sincronizacion que implementa que todos los procesadores
        /// tengan el mismo reloj!
        /// 
        /// </summary>
        /// <param name="ticksDeRelojPorEsperar">Ticks de reloj que se deben Esperar</param>
        public void Esperar(int ticksDeRelojPorEsperar)
        {
            for (int i = 0; i < ticksDeRelojPorEsperar; ++i)
            {
                // Esta barrera es donde esperan los hilos de los procesadores
                barrier.SignalAndWait();
            }
        }

        // Este metodo se ejecuta entre ciclos de controlador
        // Aqui se pueden Procesar mensajes de cachesDatos, etc
        public void entreCiclosDeReloj(Barrier b)
        {
            // Se notifica a las vistas del tick de reloj que acaba de terminar
            // Se realiza cuando el tick termine
            fireTickChanged(ticksReloj);

            // Se notifica a las vistas del cambio en el Pc
            NotificarCambioPC();

            // Se notifica a las vistas si hubo cambios en algun componente de la simulacion
            NotificarCambioRegistros();
            NotificarCambioCaches();
            NotificarCambioMemorias();

            // Ver si los procesadres han terminado de Procesar un programa
            AsignarProgramasAProcesadores();

            // Ver si ya se termino la simulacion
            VerificarSimulacionTerminada();

            // Se aumenta el tick de reloj al final porque cuenta hasta la proxima vez que los procesadores continuen
            // y se notifica hasta la proxima vez que se este en "medio" de 2 ciclos de reloj
            ++ticksReloj;
        }

        /// <summary>
        /// Notifica a las vistas que hubo un cambio en el PC 
        /// </summary>
        private void NotificarCambioPC()
        {
            foreach (Procesador p in procesadores)
            {
                fireProgramCounterChanged(p.ProgramCounter, p.ID);
            }
        }

        /// <summary>
        /// Notifica a las vistas si hubo un cambio en los registros del procesador
        /// </summary>
        private void NotificarCambioRegistros()
        {
            foreach (Procesador p in procesadores)
            {
                // p.Modificado == true si los registros se modificaron en el ultimo
                // ciclo de reloj
                if (p.Modificado)
                {
                    p.Modificado = false;
                    fireRegistersChanged(p.GetRegistros(), p.ID);
                }
            }
        }

        /// <summary>
        /// Notifica a las vistas si hubo un cambio en los bloques de las cachesDatos
        /// </summary>
        private void NotificarCambioCaches()
        {
            for (int i = 0; i < procesadores.Length; ++i)
            {
                // p.Modificado == true si los bloques de las cachesDatos se modificaron en el ultimo
                // ciclo de reloj
                if (cachesDatos[i].Modificado)
                {
                    cachesDatos[i].Modificado = false;
                    fireCacheChanged(cachesDatos[i].ToArray(), cachesDatos[i].NumerosDeBloque, cachesDatos[i].EstadosDeBloque, i);
                }
            }
        }

        /// <summary>
        /// Notifica a las vistas si hubo un cambio en los bloques de las memoriasPrincipales 
        /// </summary>
        private void NotificarCambioMemorias()
        {
            for (int i = 0; i < procesadores.Length; ++i)
            {
                // p.Modificado == true si las memoriasPrincipales se modificaron en el ultimo
                // ciclo de reloj
                if (memoriasPrincipales[i].Modificado)
                {
                    memoriasPrincipales[i].Modificado = false;
                    fireMemoryChanged(memoriasPrincipales[i].ToArray(), i);
                }
            }
        }

        /// <summary>
        /// Este metodo revisa todos los procesadores en busqueda de alguno finalizado
        /// Si algun procesador esta finalizado entonces le asigna un nuevo programa si hay
        /// Si no hay mas programas lo deja finalizar en el proximo tick de reloj.
        /// 
        /// Si un programa se cambia, entonces se le avisa a las vistas que un programa se modifico
        /// 
        /// </summary>
        private void AsignarProgramasAProcesadores()
        {
            for (int i = 0; i < procesadores.Length; ++i)
            {
                if (procesadores[i].Finalizado)
                {
                    String nombreProgramaAnterior = cacheInstrucciones.GetNombreProgramaAsignado(procesadores[i]);
                    if (cacheInstrucciones.AsignarPrograma(procesadores[i]))
                    {
                        procesadores[i].Finalizado = false;
                        fireProgramEnded(nombreProgramaAnterior, procesadores[i].GetRegistros(), procesadores[i].ID);
                        String nombreProgramaNuevo = cacheInstrucciones.GetNombrePrograma(procesadores[i].ProgramCounter);
                        fireProgramChanged(i, nombreProgramaNuevo, ticksReloj, procesadores[i].GetRegistros(), cachesDatos[i].ToArray());
                    }
                    else
                    {
                        // Cuando un procesador ya termina de ejecutarse, 
                        // no hace falta mantenerlo vivo
                        // Simplemente se le anuncia a la barrera que dicho "participante"
                        // ya no esta
                        // barrier.RemoveParticipant();
                        // La linea anterior es la solucion pero no se puede llamar dentro de este metodo
                        // Hay que buscar como solucionar esto para la 2da entrega
                    }
                }
            }
        }

        /// <summary>
        /// Este metodo pregunta si ya termino la simulacion
        /// Si la simulacion esta terminada, se le avisa a todas las vistas
        /// </summary>
        private void VerificarSimulacionTerminada()
        {
            bool todosFinalizados = true;
            for (int i = 0; i < procesadores.Length; ++i)
            {
                todosFinalizados = todosFinalizados && procesadores[i].Finalizado;
            }
            if (todosFinalizados)
            {
                fireSimulationFinished();
            }
        }
    }
}
