using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Controller
{
    /// <summary>
    /// Este interfaz la deben implementar todas las vistas que deseen escuchar los cambios en el modelo
    /// de la simulacion del procesador
    /// </summary>
    interface Listener
    {
        /// <summary>
        /// Indica que el tick de reloj ha cambiado
        /// </summary>
        /// <param name="newTick">Nuevo tick de reloj</param>
        void onTickChanged(int newTick);

        /// <summary>
        /// Indica que un procesador ha palabraLeida de ejecutar un programa
        /// Y ahora se encuentra ejecutando otro
        /// </summary>
        /// <param name="newName">El nombre del programa que ahora se encuentra en ejecucion</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onProgramChanged(int idProcesador, String nombrePrograma, int ticksReloj, int[] registros, int[] cache);

        /// <summary>
        /// Indica que el pc de un procesador ha cambiado
        /// </summary>
        /// <param name="newPc">Nuevo pc del procesador</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onProgramCounterChanged(int newPc, int idProcesador);

        /// <summary>
        /// Indica que un estado de registros ha cambiado
        /// </summary>
        /// <param name="nuevosRegistros">Vector con las palabras de los registros del procesador</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onRegistersChanged(int[] nuevosRegistros, int idProcesador);

        /// <summary>
        /// Indica que un estado de cache ha cambiado
        /// </summary>
        /// <param name="palabrasCache">Vector con las palabras de la cache</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onCacheChanged(int[] palabrasCache, int[] numerosBloquesCache, char[] estadosBloquesCache, int idProcesador);

        /// <summary>
        /// Indica que un estado de memoria principal ha cambiado
        /// </summary>
        /// <param name="palabrasMemoria">Vector con las palabras de la memroria</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onMemoryChanged(int[] palabrasMemoria, int idProcesador);

        /// <summary>
        /// Indica que un programa ha termiando su ejecucion
        /// Envia los registros finales en el procesador de dicho programa
        /// </summary>
        /// <param name="nombrePrograma">Nombre del programa finalizado</param>
        /// <param name="registrosFinales">Vector con los registros finales del programa</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador);

        /// <summary>
        /// Indica que la simulacion ha palabraLeida
        /// </summary>
        void onSimulationFinished();

    }
}
