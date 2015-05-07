using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        private int cantidadProgramas;

        /// <summary>
        /// Crea una cache de instrucciones
        /// </summary>
        /// <param name="numeroDeProgramas"></param>
        public CacheInstrucciones(List<int> instruccionesRecibidas, List<int> iniciosProgramas)
        {

            cantidadProgramas = iniciosProgramas.Count;
            Debug.WriteLine("CacheInstrucciones: Hay " + cantidadProgramas + " programas");

            instrucciones = new Instruccion[instruccionesRecibidas.Count / 4];
            for (short inst = 0; inst < instruccionesRecibidas.Count; inst += 4)
            {
                instrucciones[inst / 4] = new Instruccion(instruccionesRecibidas[inst], instruccionesRecibidas[inst + 1], instruccionesRecibidas[inst + 2], instruccionesRecibidas[inst + 3]);
            }
            Debug.WriteLine("CacheInstrucciones: Hay " + instrucciones.Length + " instrucciones en todos los programas");

            instruccionesIniciales = new int[iniciosProgramas.Count];
            for (short i = 0; i < iniciosProgramas.Count; ++i)
            {
                instruccionesIniciales[i] = iniciosProgramas[i];
            }

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

        public bool HaySiguientePrograma()
        {
            return indiceSiguientePrograma < cantidadProgramas;
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
