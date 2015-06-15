using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Representa una memoriaPrincipal memoriaPrincipal para un procesador.
    /// Se compone de 8 bloques.
    /// </summary>
    class MemoriaPrincipal : Constantes, IModificable {

        private Bloque[] bloques = new Bloque[BloquesPorMemoria];
        private bool modificado = true;
        private int id;

        /// <summary>
        /// Crea los bloques de la memoriaPrincipal memoriaPrincipal.
        /// Se inicializa en ceros
        /// </summary>
        public MemoriaPrincipal(Controlador controlador, int id) {
            this.id = id;
            for (int i = 0; i < BloquesPorMemoria; ++i) {
                bloques[i] = new Bloque(id, id * BytesPorMemoria + i * BytesPorBloque, i);
            }
        }

        /// <summary>
        /// Propiedad indexada para acceder directamente a los bloques de la memoriaPrincipal memoriaPrincipal.
        /// Devuelve y asigna copias de objetos.
        /// </summary>
        /// <param name="index">Índice del bloqueMemoria a accesar</param>
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
        /// Id de esta memoriaPrincipal memoriaPrincipal
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

        /// <summary>
        /// Devuelve el índice donde se encuentra un bloqueMemoria a partir de 
        /// una dirección inicial de un bloqueMemoria. Si el bloqueMemoria con dicha
        /// direccion no se encuentra en esta memoria, devuelve -1.
        /// </summary>
        /// <param name="direccionInicialBloque">Dirección Inicial del bloqueMemoria que se busca</param>
        /// <returns>Índice del bloqueMemoria o -1 si no se encuentra</returns>
        public int GetIndice(int direccionInicialBloque) {
            int indice = -1;
            for (int i = 0; i < BloquesPorMemoria && indice == -1; i++) {
                if (this[id].Direccion == direccionInicialBloque) {
                    indice = i;
                }
            }
            return indice;
        }

    }
}
