using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Esta clase representa un bloque de memoria para la bloques o memoria principal.
    /// Contiene 4 palabras. Cada palabra es un int.
    /// </summary>
    class Bloque : Constantes {

        private int direccionInicial;
        private int[] palabras = new int[PalabrasPorBloque];

        /// <summary>
        /// Crea un nuevo bloque inicializado en ceros.
        /// </summary>
        public Bloque(int direccionInicial) {
            this.direccionInicial = direccionInicial;
            for (int i = 0; i < PalabrasPorBloque; ++i) {
                palabras[i] = 0;
            }
        }

        /// <summary>
        /// Propiedad indexada para acceder directamente a las palabras de un bloque.
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
                int[] copia = new int[PalabrasPorBloque];
                for (int i = 0; i < PalabrasPorBloque; i++) {
                    copia[i] = palabras[i];
                }
                return copia;
            }
        }

        /// <summary>
        /// Devuelve la direccion inicial de este bloque. Esto es 
        /// utilizado por las caches para saber cual bloque contienen.
        /// Específicamente para saber si contienen un bloque local
        /// o uno remoto.
        /// </summary>
        public int DireccionBloque {
            get {
                return direccionInicial;
            }
        }

        /// <summary>
        /// Devuelve una copia de este bloque.
        /// </summary>
        /// <returns>Copia de bloque</returns>
        public Bloque Copiar() {
            Bloque copia = new Bloque(this.direccionInicial);
            for (int i = 0; i < PalabrasPorBloque; ++i) {
                copia.palabras[i] = this.palabras[i];
            }
            return copia;
        }
    }
}
