using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Esta clase es para indicar los elementos de la interfaz que necesitan ser modificados
    /// </summary>
    interface IModificable {

        /// <summary>
        /// Para saber si se debe modificar un campo en la interfaz, es necesario
        /// saber si se encuentra modificado.
        /// </summary>
        bool Modificado {
            get;
            set;
        }

    }
}
