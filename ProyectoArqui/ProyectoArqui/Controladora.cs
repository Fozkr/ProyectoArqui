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
        private int cantidadProgramas;
        private int ticsReloj;

        /*
         * Constructor, inicializa los atributos creando una lista vacía y asignando 1 al reloj.
         */
        public Controladora()
        {
            memoriaInstrucciones = new List<int>();
            cantidadProgramas = 0;
            ticsReloj = 1;
        }

        /*
         * Getter y Setter de cantidadProgramas.
         */
        public int CantidadProgramas
        {
            get { return cantidadProgramas; }
            set { cantidadProgramas = value; }
        }
    }
}
