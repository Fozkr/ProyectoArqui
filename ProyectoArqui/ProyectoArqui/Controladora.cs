using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui
{
    /*
     * 
     */
    class Controladora
    {
        //Atributos
        private List<int> memoriaInstrucciones;
        private int ticsReloj;

        /*
         * Constructor,
         */
        public Controladora()
        {
            memoriaInstrucciones = new List<int>();
            ticsReloj = 1;
        }
    }
}
