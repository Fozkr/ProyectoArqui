using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class Instruccion
    {
        private int[] instruccion;

        public Instruccion(int op, int p1, int p2, int p3) {
            instruccion = new int[]{op, p1, p2, p3};
        }

        public int getCodigo()
        {
            return instruccion[0];
        }

        // 1, 2 o 3
        public int getParametro(int i)
        {
            return instruccion[i];
        }


    }
}
