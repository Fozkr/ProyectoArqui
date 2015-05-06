using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class MemoriaPrincipal
    {

        private Bloque[] memoria = new Bloque[8];

        public Bloque get(int index)
        {
            // Considerar devolver una copia del bloque
            return memoria[index];
        }

        public void set(int index, Bloque element)
        {
            // Considerar copiar cada palabra del bloque
            memoria[index] = element; 
        }

    }
}
