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
        /// Se llama cuando el reloj cambia.
        /// </summary>
        /// <param name="newTick">Nuevo tick de reloj</param>
        void onTickChanged(int newTick);

        /// <summary>
        /// Se llama cuando un procesador ejecuta un nuevo programa.
        /// </summary>
        /// <param name="newName">El nombre del programa que ahora se encuentra en ejecucion</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onProgramChanged(int idProcesador, String nombrePrograma, int ticksReloj, int[] registros, int[] cache);

        /// <summary>
        /// Se llama cuando se modifica el pc de un procesador.
        /// </summary>
        /// <param name="newPc">Nuevo pc del procesador</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onProgramCounterChanged(int newPc, int idProcesador);

        /// <summary>
        /// Se llama cuando se modifican los registros de un procesador
        /// </summary>
        /// <param name="nuevosRegistros">Arreglo con las palabras de los registros del procesador</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onRegistersChanged(int[] nuevosRegistros, int idProcesador);

        /// <summary>
        /// Se llama cuando se modifica algún bloque de una cache
        /// </summary>
        /// <param name="palabrasCache">Vector con las palabras de la solicitante</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onCacheChanged(int[] palabrasCache, int[] numerosBloquesCache, char[] estadosBloquesCache, int idProcesador);

        /// <summary>
        /// Se llama cuando se modifica una memoria principal
        /// </summary>
        /// <param name="palabrasMemoria">Vector con las palabras de la memroria</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onMemoryChanged(int[] palabrasMemoria, int idProcesador);

        /// <summary>
        /// Se llama cuando termina de ejecutarse un programa.
        /// </summary>
        /// <param name="nombrePrograma">Nombre del programa finalizado</param>
        /// <param name="registrosFinales">Vector con los registros finales del programa</param>
        /// <param name="id">Id del procesador donde ocurrio el cambio</param>
        void onProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador);

        /// <summary>
        /// Se llama cuando la simulación termina
        /// </summary>
        void onSimulationFinished();

    }
}
