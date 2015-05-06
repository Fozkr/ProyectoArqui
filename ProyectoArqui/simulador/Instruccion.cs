using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    /// <summary>
    /// Abstraccion de una instruccion, para mayor facilidad de programacion en el procesador.
    /// </summary>
    class Instruccion
    {
        private int[] instruccion;

        /// <summary>
        /// Crea un objeto instruccion
        /// </summary>
        /// <param name="codigoInstruccion">Numero de operacion</param>
        /// <param name="parametro1">Parametro 1, es un numero</param>
        /// <param name="parametro2">Parametro 2, es un numero</param>
        /// <param name="parametro3">Parametro 3, es un numero</param>
        public Instruccion(int codigoInstruccion, int parametro1, int parametro2, int parametro3)
        {
            instruccion = new int[]{codigoInstruccion, parametro1, parametro2, parametro3};
        }

        /// <summary>
        /// Devuelve el codigo de instruccion.
        /// </summary>
        /// <returns>Codigo de instruccion</returns>
        public int GetCodigo()
        {
            return instruccion[0];
        }

        /// <summary>
        /// Devuelve uno de los parametros de la instruccion.
        /// Los valores posibles son 1, 2 o 3
        /// </summary>
        /// <param name="i">Numero de parametro que se quiere</param>
        /// <returns>Parametro deseado</returns>
        public int GetParametro(int i)
        {
            return instruccion[i];
        }


    }
}
