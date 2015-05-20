using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model
{
    /// <summary>
    /// Esta clase se utilizara en proximas implementaciones para definir un objeto abstracto con capacidad de bloquearse
    /// El proposito es que otras clases extiendan de esta clase y adquieran dicha habilidad.
    /// </summary>
    class Bloqueable
    {

        protected Controlador controlador;
        private Object candado;
        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        /// <summary>
        /// Construye un bloqueable, requiere un controlador para ser capaz de esperar ciclos antes de intentar volver a bloquear.
        /// </summary>
        /// <param name="controlador">Controlador para poder esperar ciclos</param>
        public Bloqueable(Controlador controlador)
        {
            this.controlador = controlador;
            this.candado = new Object();
        }

        /// <summary>
        /// Este metodo se encarga de obtener un candado sobre el objeto bloqueable.
        /// Una vez que este metodo retorna todos los llamados son seguros hasta que termine
        /// </summary>
        public void Bloquear()
        {
            Debug.WriteLine("Bloqueable: Intentando obtener bloque sobre " + this.Nombre);
            // Se intenta obtener el candado
            while (!Monitor.TryEnter(candado))
            {
                Debug.WriteLine("Bloqueable: Esperando un ciclo para intentar bloquear " + this.Nombre);
                // Si no se logra obtener el candado este ciclo, se espera al siguiente
                controlador.Esperar(1);
            }
            Debug.WriteLine("Bloqueable: Bloque obtenido sobre " + this.Nombre);
            // Una vez obtenido el candado este metodo puede devolver control y todos los llamados son seguros, 
            // hasta que se llame al metodo Desbloquear
        }

        /// <summary>
        /// Este metodo se encarga de desbloquear un objeto bloqueable para que alguien mas lo pueda solicitar
        /// </summary>
        public void Desbloquear()
        {
            Debug.WriteLine("Bloqueable: Bloque quitado de " + this.Nombre);
            Monitor.Exit(candado);
        }

    }
}
