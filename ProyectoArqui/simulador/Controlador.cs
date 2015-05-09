using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProyectoArqui.simulador
{
    class Controlador
    {
        //Atributos
        Barrier barrier;
        Procesador[] procesadores;
        CacheInstrucciones cacheInstrucciones;
        bool[] programasTerminados;
        int ticsReloj;

        public Controlador(int numeroProcesadores, Procesador[] procesadores, CacheInstrucciones cacheInstrucciones)
        {
            
            Debug.WriteLine("Controlador: Creando una barrera para " + numeroProcesadores + "procesadores");
            this.barrier = new Barrier(numeroProcesadores, entreCiclosDeReloj);
            this.procesadores = procesadores;
            this.cacheInstrucciones = cacheInstrucciones;
            programasTerminados = new bool[numeroProcesadores]; //***
            ticsReloj = 1;

            // Decirle a a cada procesador cual programa va a ejecutar

            // TODO Modularizar esto
            for (int i = 0; i < procesadores.Length; ++i)
            {
                if (cacheInstrucciones.HaySiguientePrograma())
                {
                    procesadores[i].NombrePrograma = cacheInstrucciones.GetNombreSiguientePrograma();
                    int direccionSiguientePrograma = cacheInstrucciones.GetDireccionSiguientePrograma();
                    procesadores[i].SetProgramCounter(direccionSiguientePrograma);
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
                ++ticsReloj;
            }
        }

        public void programaTerminado(int idProcesador)
        {
            Debug.WriteLine("Controlador: El procesador " + idProcesador + " acaba de terminar su programa");
            programasTerminados[idProcesador] = true;
        }

        public void entreCiclosDeReloj(Barrier b)
        {
            // Este metodo se ejecuta entre ciclos de controlador
            // Aqui se pueden procesar mensajes de caches, etc


            // Ver si los procesadres han terminado de procesar un programa

            // TODO Modularizar esto
            for (int i = 0; i < procesadores.Length;  ++i )
            {
                if (programasTerminados[i])
                {
                    programasTerminados[i] = false;
                    if (cacheInstrucciones.HaySiguientePrograma())
                    {
                        procesadores[i].NombrePrograma = cacheInstrucciones.GetNombreSiguientePrograma();
                        int direccionSiguientePrograma = cacheInstrucciones.GetDireccionSiguientePrograma();
                        procesadores[i].SetProgramCounter(direccionSiguientePrograma);
                        Debug.WriteLine("Controlador: El procesador " + i + " va a ejecutar el programa que empieza en " + direccionSiguientePrograma);
                    }
                    else
                    {
                        Debug.WriteLine("Controlador: El procesador " + i + " va a finalizar porque no hay mas programas que ejecutar");
                        procesadores[i].Finalizado = true;
                    }
                }
            }
        }

        public int TicsReloj
        {
            get { return this.ticsReloj; }
            set { this.ticsReloj = value; }
        }
    }
}
