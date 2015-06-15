using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Controller
{
    /// <summary>
    /// Esta clase la implementa un modelo que desea ser observable por los listeners
    /// </summary>
    class Observable
    {

        List<Listener> listeners = new List<Listener>();

        /// <summary>
        /// Agrega un listener a este objeto.
        /// </summary>
        /// <param name="listener">Nuevo listener</param>
        public void AddListener(Listener listener)
        {
            listeners.Add(listener);
        }

        /// <summary>
        /// Se llama cuando el reloj cambia.
        /// </summary>
        /// <param name="newTick">Nuevo tick de reloj</param>
        public void fireTickChanged(int newTick)
        {
            foreach (Listener l in listeners)
            {
                l.onTickChanged(newTick);
            }
        }

        /// <summary>
        /// Se llama cuando un procesador ejecuta un nuevo programa.
        /// </summary>
        /// <param name="newName">El nombre del programa que ahora se encuentra en ejecucion</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        public void fireProgramChanged(int idProcesador, String newName, int ticksRelojs, int[] registros, int[] cache)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramChanged(idProcesador, newName, ticksRelojs, registros, cache);
            }
        }

        /// <summary>
        /// Se llama cuando se modifica el pc de un procesador.
        /// </summary>
        /// <param name="newPc">Nuevo pc del procesador</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        public void fireProgramCounterChanged(int newPc, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramCounterChanged(newPc, idProcesador);
            }
        }

        /// <summary>
        /// Se llama cuando se modifican los registros de un procesador
        /// </summary>
        /// <param name="nuevosRegistros">Vector con las palabras de los registros del procesador</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        public void fireRegistersChanged(int[] nuevosRegistros, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onRegistersChanged(nuevosRegistros, idProcesador);
            }
        }

        /// <summary>
        /// Se llama cuando se modifica algún bloque de una cache
        /// </summary>
        /// <param name="palabrasCache">Vector con las palabras de la solicitante</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        public void fireCacheChanged(int[] palabrasCache, int[] numerosBloquesCache, char[] estadosBloquesCaches, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onCacheChanged(palabrasCache, numerosBloquesCache, estadosBloquesCaches, idProcesador);
            }
        }

        /// <summary>
        /// Se llama cuando se modifica una memoria principal
        /// </summary>
        /// <param name="palabrasMemoria">Vector con las palabras de la memroria</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        public void fireMemoryChanged(int[] palabrasMemoria, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onMemoryChanged(palabrasMemoria, idProcesador);
            }
        }

        /// <summary>
        /// Se llama cuando termina de ejecutarse un programa.
        /// </summary>
        /// <param name="nombrePrograma">Nombre del programa finalizado</param>
        /// <param name="registrosFinales">Vector con los registros finales del programa</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        public void fireProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramEnded(nombrePrograma, registrosFinales, idProcesador);
            }
        }

        /// <summary>
        /// Se llama cuando la simulación termina
        /// </summary>
        public void fireSimulationFinished()
        {
            foreach (Listener l in listeners)
            {
                l.onSimulationFinished();
            }
        }
    }
}
