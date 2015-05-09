using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model
{
    /// <summary>
    /// Esta clase representa un bloque de memoria para la bloquesDeCache o memoria principal.
    /// Contiene 4 palabras.
    /// Cada palabra es un int.
    /// </summary>
    class Bloque
    {
        private int[] palabrasDelBloque = new int[4];

        /// <summary>
        /// Crea un nuevo bloque inicializado en ceros.
        /// </summary>
        public Bloque()
        {
            for (int i = 0; i < palabrasDelBloque.Length; ++i)
            {
                palabrasDelBloque[i] = 0;
            }
        }

        /// <summary>
        /// Devuelve una palabra del bloque.
        /// </summary>
        /// <param name="palabra">Indice de la palabra</param>
        /// <returns>Devuelve la palabra palabra del bloque</returns>
        public int GetPalabra(int i)
        {
            return palabrasDelBloque[i];
        }

        /// <summary>
        /// Asigna una palabra nueva en una posicion del bloque
        /// </summary>
        /// <param name="palabra">Indice donde se coloca la palabra nueva</param>
        /// <param name="nuevaPalabra">Nueva palabra a colocar</param>
        public void SetPalabra(int i, int nuevaPalabra)
        {
            palabrasDelBloque[i] = nuevaPalabra;
        }

        /// <summary>
        /// Convierte las palabras del Bloque en un vector para las vistas
        /// </summary>
        /// <returns>Vector de datos</returns>
        public int[] ToArray()
        {
            return palabrasDelBloque;
        }
    }
}
