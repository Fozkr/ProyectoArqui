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
            return instrucciones[dirMem / 4];
        }

    }
}
