using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model
{
    /// <summary>
    /// El proposito de esta clase es dar capacidad a los objetos de indicar si han sido modificados
    /// para notificar o no a las vistas del cambio que haya sucedido
    /// </summary>
    class Modificable
    {

        // Por defecto se asume que un objeto que acaba ser creado 
        // ha sido notificado y este cambio debe ser notificado a las vistas
        private bool modificado = true;

        public bool Modificado
        {
            get { return modificado; }
            set { modificado = value; }
        }
    }
}
