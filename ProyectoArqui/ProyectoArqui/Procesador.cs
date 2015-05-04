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
    class Procesador
    {
        //Atributos
        private short programCounter;
        private int* registros;
        private int** cacheDatos;
        private int** memoriaPrincipal;
        //sem semaforoCache;
        //int** directorio;
        //sem semaforoDirectorio
        Boolean atrasado;

        public Procesador()
        {
            programCounter = -1;
            //registros = new int[32];

        }
    }
}
