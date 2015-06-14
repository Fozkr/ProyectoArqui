using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProyectoArqui.Model {
    /// <summary>
    /// Representacion de una cacheDatos de instrucciones.
    /// Contiene un vector con todas las instrucciones de todos los programa
    /// Contiene un vector que indica donde esta la primera instruccion de cada programa a ejecutar
    /// No funciona como la cacheDatos de datos, simplemente contiene las instrucciones.
    /// </summary>
    class CacheInstrucciones {

        private Instruccion[] instrucciones;
        private String[] nombresProgramas;
        private String[] programasAsignados;
        private int[] instruccionesIniciales;
        private int indiceSiguientePrograma;
        private int cantidadProgramas;

        /// <summary>
        /// Crea una cacheDatos de instrucciones
        /// </summary>
        /// <param name="instruccionesRecibidas"></param>
        /// <param name="iniciosProgramas"></param>
        /// <param name="cantidadProgramasRecibida"></param>
        public CacheInstrucciones(int numeroProcesadores, int[] instruccionesRecibidas, int[] iniciosProgramas, int cantidadProgramasRecibida, String[] nombresProgramasRecibidos) {

            cantidadProgramas = cantidadProgramasRecibida;
            nombresProgramas = nombresProgramasRecibidos;
            programasAsignados = new String[numeroProcesadores];
            Debug.WriteLine("CacheInstrucciones: Hay " + cantidadProgramas + " programas");

            instrucciones = new Instruccion[instruccionesRecibidas.Length / 4];
            for (short inst = 0; inst < instruccionesRecibidas.Length; inst += 4) {
                instrucciones[inst / 4] = new Instruccion(instruccionesRecibidas[inst], instruccionesRecibidas[inst + 1], instruccionesRecibidas[inst + 2], instruccionesRecibidas[inst + 3]);
            }
            Debug.WriteLine("CacheInstrucciones: Hay " + instrucciones.Length + " instrucciones en todos los programas");

            instruccionesIniciales = new int[iniciosProgramas.Length];
            for (short i = 0; i < iniciosProgramas.Length; ++i) {
                instruccionesIniciales[i] = iniciosProgramas[i];
            }

            indiceSiguientePrograma = 0;
        }

        /// <summary>
        /// Devuelve la instruccion que el procesador solicite
        /// </summary>
        /// <param name="direccionPalabra">Recibe la direccion de memoria donde se encuentra ubicada la instruccion que el procesador desea</param>
        /// <returns>Devuelve la instruccion solicitada</returns>
        public Instruccion ObtenerInstruccion(int direccionMemoria) {
            int indiceInstruccion = GetIndiceInstruccion(direccionMemoria);
            return instrucciones[indiceInstruccion];
        }

        /// <summary>
        /// Devuelve el nombre del programa cuya primera dirección es la que se indica por parámetro
        /// </summary>
        /// <param name="direccionInicial">Dirección inicial donde se encuentra la primera instruccion del programa</param>
        /// <returns></returns>
        public String GetNombrePrograma(int direccionInicial) {
            String nombre = "Desconocido";
            for (int i = 0; i < cantidadProgramas; ++i) {
                if (direccionInicial == instruccionesIniciales[i]) {
                    nombre = nombresProgramas[i];
                }
            }
            return nombre;
        }

        /// <summary>
        /// Devuelve el nombre del programa asignado en un procesador
        /// </summary>
        /// <param name="id">Id del procesador</param>
        /// <returns>Devuelve el nombre del programa en el procesador</returns>
        public String GetNombreProgramaAsignado(Procesador procesador) {
            return programasAsignados[procesador.ID];
        }

        /// <summary>
        /// Le indica al procesador que entra como parametro cual es el siguiente programa que deberia ejecutar
        /// Si no hay un siguiente programa, indica que el procesador ha palabraLeida su trabajo
        /// </summary>
        /// <param name="procesador">Procesador cuyo programa se quiere modificar</param>
        /// <returns>Debuelve true si cambia el programa del procesador</returns>
        public bool AsignarPrograma(Procesador procesador) {
            bool asignado = HaySiguientePrograma();
            if (asignado) {
                int direccionSiguientePrograma = GetDireccionSiguientePrograma();
                procesador.ProgramCounter = direccionSiguientePrograma;
                procesador.ResetearRegistros();
                programasAsignados[procesador.ID] = GetNombrePrograma(procesador.ProgramCounter);
                Debug.WriteLine("CacheInstrucciones: El procesador " + procesador.ID + " va a ejecutar el programa que empieza en " + direccionSiguientePrograma);
            } else {
                Debug.WriteLine("CacheInstrucciones: El procesador " + procesador.ID + " va a finalizar porque no hay mas programas que ejecutar");
                procesador.Finalizado = true;
            }
            return asignado;
        }

        /// <summary>
        /// Devuelve si hay un siguiente programa que ejecutar
        /// </summary>
        /// <returns>True si aun quedan programas por ejecutar, false en cualquier otro caso</returns>
        private bool HaySiguientePrograma() {
            return indiceSiguientePrograma < cantidadProgramas;
        }

        /// <summary>
        /// Devuelve la direccion de memoria del siguiente programa a ejecutar
        /// Cada llamado a este metodo devuelve una direccion distinta
        /// </summary>
        /// <returns>direccion de memoria del siguiente programa</returns>
        private int GetDireccionSiguientePrograma() {
            return instruccionesIniciales[indiceSiguientePrograma++];
        }

        /// <summary>
        /// Hace el mapeo de una direccion de memoria al indice correspondiente en el vector de instrucciones
        /// </summary>
        /// <param name="direccionPalabra"></param>
        /// <returns></returns>
        private int GetIndiceInstruccion(int direccionMemoria) {
            return direccionMemoria / 4;
        }
    }
}
