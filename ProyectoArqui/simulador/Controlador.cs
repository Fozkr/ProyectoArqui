using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using ProyectoArqui.ui;

namespace ProyectoArqui.simulador
{
    class Controlador : Observable
    {
        //Atributos
        Barrier barrier;
        Procesador[] procesadores;
        CacheInstrucciones cacheInstrucciones;
        bool[] programasTerminados;
        int ticksReloj;

        public Controlador(int numeroProcesadores, Procesador[] procesadores, CacheInstrucciones cacheInstrucciones)
        {
            
            Debug.WriteLine("Controlador: Creando una barrera para " + numeroProcesadores + "procesadores");
            this.barrier = new Barrier(numeroProcesadores, entreCiclosDeReloj);
            this.procesadores = procesadores;
            this.cacheInstrucciones = cacheInstrucciones;
            programasTerminados = new bool[numeroProcesadores]; //***
            ticksReloj = 1;

            // Decirle a a cada procesador cual programa va a ejecutar

            // TODO Modularizar esto
            for (int i = 0; i < procesadores.Length; ++i)
            {
                if (cacheInstrucciones.HaySiguientePrograma())
                {
                    int direccionSiguientePrograma = cacheInstrucciones.GetDireccionSiguientePrograma();
                    procesadores[i].SetProgramCounter(direccionSiguientePrograma);
                    fireProgramNameChanged(cacheInstrucciones.GetNombrePrograma(direccionSiguientePrograma), i);
                    Debug.WriteLine("Controlador: El procesador " + i + " va a ejecutar el programa que empieza en " + direccionSiguientePrograma);
                }
                else
                {
                    Debug.WriteLine("Controlador: El procesador " + i + " va a finalizar porque no hay mas programas que ejecutar");
                    procesadores[i].Finalizado = true;
                }
            }
        }

        public void esperar(int ticksDeReloj)
        {
            for (int i = 0; i < ticksDeReloj; ++i)
            {
                barrier.SignalAndWait();
            }
        }

        public void programaTerminado(int idProcesador)
        {
            Debug.WriteLine("Controlador: El procesador " + idProcesador + " acaba de terminar su programa");
            programasTerminados[idProcesador] = true;
        }

        // Este metodo se ejecuta entre ciclos de controlador
        // Aqui se pueden procesar mensajes de caches, etc
        public void entreCiclosDeReloj(Barrier b)
        {
            // Se aumenta la cantidad de tics de Reloj
            ++ticksReloj;

            // Se notifica a los listeners que hay un nuevo tick de relo
            fireTickChanged(ticksReloj);

            // Ver si los procesadres han terminado de procesar un programa
            // TODO Modularizar esto
            for (int i = 0; i < procesadores.Length;  ++i )
            {
                if (programasTerminados[i])
                {
                    programasTerminados[i] = false;
                    if (cacheInstrucciones.HaySiguientePrograma())
                    {
                        int direccionSiguientePrograma = cacheInstrucciones.GetDireccionSiguientePrograma();
                        procesadores[i].SetProgramCounter(direccionSiguientePrograma);
                        fireProgramNameChanged(cacheInstrucciones.GetNombrePrograma(direccionSiguientePrograma), i);
                        Debug.WriteLine("Controlador: El procesador " + i + " va a ejecutar el programa que empieza en " + direccionSiguientePrograma);
                    }
                    else
                    {
                        Debug.WriteLine("Controlador: El procesador " + i + " va a finalizar porque no hay mas programas que ejecutar");
                        procesadores[i].Finalizado = true;
                    }
                }
            }

            // Ver si todos los procesadores ya terminaron
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
