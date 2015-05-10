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
        /// Agrega un listener a este objeto
        /// </summary>
        /// <param name="listener">Nuevo listener</param>
        public void AddListener(Listener listener)
        {
            listeners.Add(listener);
        }

        /// <summary>
        /// Indica que el tick de reloj ha cambiado
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
        /// Indica que un procesador ha terminado de ejecutar un programa
        /// Y ahora se encuentra ejecutando otro
        /// </summary>
        /// <param name="newName">El nombre del programa que ahora se encuentra en ejecucion</param>
        /// <param name="idProcesador">Id del procesador donde ocurrio el cambio</param>
        public void fireProgramChanged(int idProcesador, String newName, int ticksRelojs, int[] registros, int[] cache)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramChanged(idProcesador, newName, ticksRelojs, registros, cache);
            }
        }

        /// <summary>
        /// Indica que el pc de un procesador ha cambiado
        /// </summary>
        /// <param name="newPc">Nuevo pc del procesador</param>
        /// <param name="idProcesador">Id del procesador donde ocurrio el cambio</param>
        public void fireProgramCounterChanged(int newPc, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramCounterChanged(newPc, idProcesador);
            }
        }

        /// <summary>
        /// Indica que un estado de registros ha cambiado
        /// </summary>
        /// <param name="nuevosRegistros">Vector con las palabras de los registros del procesador</param>
        /// <param name="idProcesador">Id del procesador donde ocurrio el cambio</param>
        public void fireRegistersChanged(int[] nuevosRegistros, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onRegistersChanged(nuevosRegistros, idProcesador);
            }
        }

        /// <summary>
        /// Indica que un estado de cache ha cambiado
        /// </summary>
        /// <param name="palabrasCache">Vector con las palabras de la cache</param>
        /// <param name="idProcesador">Id del procesador donde ocurrio el cambio</param>
        public void fireCacheChanged(int[] palabrasCache, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onCacheChanged(palabrasCache, idProcesador);
            }
        }

        /// <summary>
        /// Indica que un estado de memoria principal ha cambiado
        /// </summary>
        /// <param name="palabrasMemoria">Vector con las palabras de la memroria</param>
        /// <param name="idProcesador">Id del procesador donde ocurrio el cambio</param>
        public void fireMemoryChanged(int[] palabrasMemoria, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onMemoryChanged(palabrasMemoria, idProcesador);
            }
        }

        /// <summary>
        /// Indica que un programa ha termiando su ejecucion
        /// Envia los registros finales en el procesador de dicho programa
        /// </summary>
        /// <param name="nombrePrograma">Nombre del programa finalizado</param>
        /// <param name="registrosFinales">Vector con los registros finales del programa</param>
        /// <param name="idProcesador">Id del procesador donde ocurrio el cambio</param>
        public void fireProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramEnded(nombrePrograma, registrosFinales, idProcesador);
            }
        }

        /// <summary>
        /// Indica que la simulacion ha terminado
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
