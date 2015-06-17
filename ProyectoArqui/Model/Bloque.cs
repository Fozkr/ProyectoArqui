using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Esta clase representa un bloqueMemoria de memoriaPrincipal para la bloques o memoriaPrincipal memoriaPrincipal.
    /// Contiene 4 palabras. Cada palabra es un int.
    /// </summary>
    class Bloque : Constantes {

        protected int id;
        protected int direccionInicial;
        protected int indiceMemoriaPrincipal;
        protected int[] palabras = new int[PalabrasPorBloque];

        // Para interfaz
        private int[] palabrasArray = new int[PalabrasPorBloque];

        /// <summary>
        /// Crea un nuevo bloqueMemoria inicializado en ceros.
        /// </summary>
        /// <param name="id">Id del dueño del bloqueMemoria</param>
        /// <param name="direccionInicial">Dirección de la primera palabra del bloqueMemoria</param>
        public Bloque(int id, int direccionInicial, int indiceMemoriaPrincipal) {
            this.id = id;
            this.direccionInicial = direccionInicial;
            this.indiceMemoriaPrincipal = indiceMemoriaPrincipal;
            for (int i = 0; i < PalabrasPorBloque; ++i) {
                palabras[i] = 0;
            }
        }

        /// <summary>
        /// Constrctor por copia para los hijos
        /// </summary>
        /// <param name="bloqueMemoria">Bloque a copiar</param>
        protected Bloque(Bloque bloque) {
            this.id = bloque.id;
            this.direccionInicial = bloque.direccionInicial;
            this.indiceMemoriaPrincipal = bloque.indiceMemoriaPrincipal;
            for (int i = 0; i < PalabrasPorBloque; i++) {
                this.palabras[i] = bloque.palabras[i];
            }
        }

        /// <summary>
        /// Propiedad indexada para acceder directamente a las palabras de un bloqueMemoria.
        /// </summary>
        /// <param name="index">Índice de la palabra que se quiere accesar</param>
        /// <returns>Palabra que se quiere accesar</returns>
        public int this[int index] {
            get {
                return palabras[index];
            }
            set {
                palabras[index] = value;
            }
        }

        /// <summary>
        /// Propiedad para acceder a la estructura interna del Bloque.
        /// Devuelve una copia.
        /// </summary>
        public int[] Array {
            get {
                for (int i = 0; i < PalabrasPorBloque; i++) {
                    palabrasArray[i] = palabras[i];
                }
                return palabrasArray;
            }
        }

        /// <summary>
        /// Devuelve el id del dueño del bloqueMemoria.
        /// </summary>
        public int ID {
            get {
                return id;
            }
        }

        /// <summary>
        /// Devuelve la direccion inicial de este bloqueMemoria. 
        /// </summary>
        public int Direccion {
            get {
                return direccionInicial;
            }
        }

        /// <summary>
        /// Devuelve donde debería ubicarse el bloqueMemoria en la cache
        /// </summary>
        public int IndiceCache {
            get {
                return MapeoDirecto();
            }
        }

        /// <summary>
        /// Devuelve el indice de este bloqueMemoria en memoria principal
        /// </summary>
        public int IndiceMemoriaPrincipal {
            get {
                return indiceMemoriaPrincipal;
            }
        }

        /// <summary>
        /// Devuelve una copia de este bloqueMemoria.
        /// </summary>
        /// <returns>Copia de bloqueMemoria</returns>
        public Bloque Copiar() {
            return new Bloque(this);
        }

        /// <summary>
        /// Indica donde deberia ubicarse el bloqueMemoria en la cache de datos
        /// </summary>
        /// <returns>Devuelve el indice que el bloqueMemoria deberia tener en la cacheDatos</returns>
        private int MapeoDirecto() {
            return IndiceMemoriaPrincipal % BloquesPorCache;
        }
    }
}
