using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Representa una memoria principal para un procesador.
    /// Se compone de 8 bloques.
    /// </summary>
    class MemoriaPrincipal : Constantes, IModificable {

        private Bloque[] bloques = new Bloque[BloquesPorMemoria];
        private bool modificado = true;
        private int id;

        /// <summary>
        /// Crea los bloques de la memoria principal.
        /// Se inicializa en ceros
        /// </summary>
        public MemoriaPrincipal(Controlador controlador, int id) {
            this.id = id;
            for (int i = 0; i < BloquesPorMemoria; ++i) {
                bloques[i] = new Bloque(id * BytesPorMemoria + i * BytesPorBloque);
            }
        }

        /// <summary>
        /// Propiedad indexada para acceder directamente a los bloques de la memoria principal.
        /// Devuelve y asigna copias de objetos.
        /// </summary>
        /// <param name="index">Índice del bloque a accesar</param>
        /// <returns>Bloque que se quiere accesar</returns>
        public Bloque this[int index] {
            get {
                return bloques[index].Copiar();
            }
            set {
                bloques[index] = value.Copiar();
                this.Modificado = true;
            }
        }

        /// <summary>
        /// Implementación de la interfaz IModificable.
        /// </summary>
        public bool Modificado {
            get {
                return modificado;
            }
            set {
                modificado = value;
            }
        }

        /// <summary>
        /// Id de esta memoria principal
        /// </summary>
        public int ID {
            get {
                return id;
            }
        }

        /// <summary>
        /// Propiedad para acceder a la estructura interna de la Memoria Principal.
        /// Devuelve una copia.
        /// </summary>
        public int[] Array {
            get {
                int[] vector = new int[PalabrasPorMemoria];
                int k = 0;
                foreach (Bloque bloque in bloques) {
                    foreach (int palabra in bloque.Array) {
                        vector[k++] = palabra;
                    }
                }
                return vector;
            }
        }

    }
}
