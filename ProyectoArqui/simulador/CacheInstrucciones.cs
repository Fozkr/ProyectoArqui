using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    /// <summary>
    /// Representacion de una cache de instrucciones.
    /// Contiene un vector con todas las instrucciones de todos los programa
    /// Contiene un vector que indica donde esta la primera instruccion de cada programa a ejecutar
    /// No funciona como la cache de datos, simplemente contiene las instrucciones.
    /// </summary>
    class CacheInstrucciones
    {

        private Instruccion[] instrucciones;
        private int[] instruccionesIniciales;
        private int indiceSiguientePrograma;

        /// <summary>
        /// Crea una cache de instrucciones
        /// </summary>
        /// <param name="numeroDeProgramas"></param>
        public CacheInstrucciones(int numeroDeProgramas) 
        {
            // TODO Completar este constructor para que añada a la lista las instrucciones

            // Implementacion Dummy
            // 3 "programas" que solo ejecutan una instruccion de finalizacion
            instrucciones = new Instruccion[] { new Instruccion(63, 0, 0, 0), new Instruccion(63, 0, 0, 0), new Instruccion(63, 0, 0, 0)};
            instruccionesIniciales = new int[] { 0, 4, 8 };
            indiceSiguientePrograma = 0;
        }
        
        /// <summary>
        /// Devuelve la instruccion que el procesador solicite
        /// </summary>
        /// <param name="direccionMemoria">Recibe la direccion de memoria donde se encuentra ubicada la instruccion que el procesador desea</param>
        /// <returns>Devuelve la instruccion solicitada</returns>
        public Instruccion obtenerInstruccion(int direccionMemoria)
        {
            int indiceInstruccion = GetIndiceInstruccion(direccionMemoria);
            return instrucciones[indiceInstruccion];
        }

        /// <summary>
        /// Devuelve la direccion de memoria del siguiente programa a ejecutar
        /// </summary>
        /// <returns>direccion de memoria del siguiente programa</returns>
        public int GetDireccionSiguientePrograma()
        {
            return instruccionesIniciales[indiceSiguientePrograma++];
        }

        /// <summary>
        /// Hace el mapeo de una direccion de memoria al indice correspondiente en el vector de instrucciones
        /// </summary>
        /// <param name="direccionMemoria"></param>
        /// <returns></returns>
        private int GetIndiceInstruccion(int direccionMemoria)
        {
            return direccionMemoria / 4;
        }
    }
}
