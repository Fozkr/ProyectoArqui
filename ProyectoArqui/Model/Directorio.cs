using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {
    /// <summary>
    /// En futuras implementaciones, esta clase contendra informacion de los bloques de las cachesDatos
    /// de cada procesador 
    /// </summary>
    class Directorio : Bloqueable {

        private CacheDatos cacheDeDatos;

        // Contiene el estado de cada bloque
        //      'U' Uncached
        //      'C' Compartido
        //      'M' Modificado
        private char[] estadosDeBloque = new char[8];

        //Matriz dinámica de control para saber cuál o cuáles cachés tienen cada uno de los bloques
        private List<Directorio>[] usuariosDeBloque = new List<Directorio>[8];

        /// <summary>
        /// Se inicializa directorio 
        /// </summary>
        /// <param name="controlador">Se asigna controlador</param>
        public Directorio(Controlador controlador)
            : base(controlador) {
            for (int i = 0; i < estadosDeBloque.Length; i++) {
                estadosDeBloque[i] = 'U';
                usuariosDeBloque[i] = new List<Directorio>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheDeDatos"></param>
        public void SetCacheDatos(CacheDatos cacheDeDatos) {
            this.cacheDeDatos = cacheDeDatos;
        }

        /// <summary>
        /// Modifica el estado de un bloque en específico.
        /// </summary>
        /// <param name="numeroBloque">Número de bloque al que le voy a modificar el estado</param>
        /// <param name="estado">Nuevo estado.</param>
        /// <param name="local">Booleano que se usa para saber si es un directorio local o remoto</param>
        public void SetEstadoBloque(int numeroBloque, char estado, bool local) {
            this.Esperar(local);
            estadosDeBloque[numeroBloque] = estado;
        }

        /// <summary>
        /// Obtengo el estado de un bloque en específico.
        /// </summary>
        /// <param name="numeroBloque">Número de bloque del que voy a revisar el estado</param>
        /// <param name="local">Booleano que se usa para saber si es un directorio local o remoto</param>
        /// <returns>Retorna el estado del bloque correspondiente</returns>
        public char GetEstadoBloque(int numeroBloque, bool local) {
            this.Esperar(local);
            return estadosDeBloque[numeroBloque];
        }

        /// <summary>
        /// Se agrega directorio a la lista respectiva
        /// </summary>
        /// <param name="numeroBloque">Número de bloque que usará el directorio respectivo</param>
        /// <param name="directorio">Directorio que utilizará uno de los bloques</param>
        public void AgregarUsuarioBloque(int numeroBloque, Directorio directorio) {
            usuariosDeBloque[numeroBloque].Add(directorio);
        }

        /// <summary>
        /// Se elimina directorio de la lista respectiva
        /// </summary>
        /// <param name="numeroBloque">Número de bloque que dejará de usar el directorio respectivo</param>
        /// <param name="directorio">Directorio que dejará de utilizar uno de los bloques</param>
        public void EliminarUsuarioBloque(int numeroBloque, Directorio directorio) {
            usuariosDeBloque[numeroBloque].Remove(directorio);
        }

        /// <summary>
        /// Cantidad de ciclos a esperar en caso de que se trate de una operación local o remota
        /// </summary>
        /// <param name="local">Booleano que se usa para saber si es un directorio local o remoto</param>
        private void Esperar(bool local) {
            if (local) {
                controlador.Esperar(2);
            } else {
                controlador.Esperar(4);
            }
        }

        /// <summary>
        /// Se invalida el bloque en todas las cachés que lo usen
        /// </summary>
        /// <param name="numeroBloque">Número de bloque a invalidar</param>
        public void InvalidarBloque(int numeroBloque) {
            foreach(Directorio directorio in usuariosDeBloque[numeroBloque]){
                directorio.cacheDeDatos.Invalidar(numeroBloque);
            }
        }

    }
}
