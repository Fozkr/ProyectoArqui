using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class Bloque
    {
        private int[] palabras = new int[4];

        Bloque()
        {
            for (int i = 0; i < palabras.Length; ++i)
            {
                palabras[i] = 0;
            }
        }

        public int get(int index)
        {
            return palabras[index];
        }

        public void set(int index, int element)
        {
            palabras[index] = element;
        }

    }
}
