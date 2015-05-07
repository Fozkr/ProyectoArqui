using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    /// <summary>
    /// Representa una memoria principal para un procesador.
    /// Se compone de 8 bloques.
    /// </summary>
    class MemoriaPrincipal
    {
        private Bloque[] bloquesDeMemoria = new Bloque[8];

        public MemoriaPrincipal()
        {
            for (int i = 0; i < 8; ++i)
            {
                bloquesDeMemoria[i] = new Bloque();
            }
        }

        /// <summary>
        /// Devuelve un bloque de la memoria
        /// </summary>
        /// <param name="i">Indice del bloque a modificar</param>
        /// <returns>Devuelve el bloque i de la memoria</returns>
        public Bloque GetBloque(int i)
        {
            // TODO Verificar si este metodo devuelve una copia del bloque
            return bloquesDeMemoria[i];
        }

        /// <summary>
        /// Asigna un bloque nuevo en una posicion de la memoria
        /// Este bloque deberia venir de la cache de datos
        /// </summary>
        /// <param name="i">Indice donde se coloca el bloque nuevo</param>
        /// <param name="bloqueNuevo">Nuevo bloque a colocar</param>
        public void SetBloque(int i, Bloque bloqueNuevo)
        {
            bloquesDeMemoria[i] = bloqueNuevo;
        }

    }
}
