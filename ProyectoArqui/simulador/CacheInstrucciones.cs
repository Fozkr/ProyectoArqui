using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class CacheInstrucciones
    {

        private Instruccion[] instrucciones;

        public CacheInstrucciones() 
        {
            //Obtener instrucciones de los archivos
        }

        public Instruccion obtenerInstruccion(int dirMem)
        {
            return new Instruccion(63, 0, 0, 0);
            // return instrucciones[dirMem / 4];
        }

    }
}
