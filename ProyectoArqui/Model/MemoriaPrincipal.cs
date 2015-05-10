using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model
{
    /// <summary>
    /// Representa una memoria principal para un procesador.
    /// Se compone de 8 bloques.
    /// </summary>
    class MemoriaPrincipal : Modificable
    {
        private Bloque[] bloquesDeMemoria = new Bloque[8];

        /// <summary>
        /// Crea los bloques de la memoria principal
        /// Se inicializa en ceros
        /// </summary>
        public MemoriaPrincipal()
        {
            for (int i = 0; i < 8; ++i)
            {
                bloquesDeMemoria[i] = new Bloque();
            }
        }

        /// <summary>
        /// Devuelve una copia de un bloque de la memoria
        /// </summary>
        /// <param name="palabra">Indice del bloque a modificar</param>
        /// <returns>Devuelve el bloque palabra de la memoria</returns>
        public Bloque GetBloque(int i)
        {
            // TODO Verificar si este metodo devuelve una copia del bloque
            return bloquesDeMemoria[i].CopiarBloque();
        }

        /// <summary>
        /// Asigna un bloque nuevo en una posicion de la memoria.
        /// Copia el contenido del bloque antes de asignarlo
        /// Este bloque deberia venir de la cacheDatos de datos
        /// </summary>
        /// <param name="palabra">Indice donde se coloca el bloque nuevo</param>
        /// <param name="bloqueNuevo">Nuevo bloque a colocar</param>
        public void SetBloque(int i, Bloque bloqueNuevo)
        {
            bloquesDeMemoria[i] = bloqueNuevo.CopiarBloque();
            Modificado = true;
        }

        /// <summary>
        /// Convierte los bloques de la Memoria en un vector para las vistas
        /// </summary>
        /// <returns>Vector de datos</returns>
        public int[] ToArray()
        {
            int[] vector = new int[32];
            int i = 0;
            foreach (Bloque bloque in bloquesDeMemoria)
            {
                foreach (int palabra in bloque.ToArray())
                {
                    vector[i++] = palabra;
                }
            }
            return vector;
        }

    }
}
