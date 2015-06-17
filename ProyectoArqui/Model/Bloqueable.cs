using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProyectoArqui.Controller;
using ProyectoArqui.Model.Exceptions;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Esta clase sirve para darle la habilidad de bloquearse a las caches y a los directorios
    /// </summary>
    abstract class Bloqueable : Constantes {

        protected Controlador controlador;
        private Object candado;
        private String nombre;

        /// <summary>
        /// Propiedad que indica el nombre del objeto que es bloqueable
        /// </summary>
        public String Nombre {
            get {
                return nombre;
            }
            set {
                this.nombre = value;
            }
        }

        /// <summary>
        /// Construye un bloqueable, requiere un controlador para ser capaz de esperar ciclos antes de intentar volver a bloquear.
        /// </summary>
        /// <param name="controlador">Controlador para poder esperar ciclos</param>
        public Bloqueable(Controlador controlador, string nombre) {
            this.controlador = controlador;
            this.candado = new Object();
            this.Nombre = nombre;
        }


        /// <summary>
        /// Este metodo se encarga de intentar obtener un candado sobre el objeto bloqueable.
        /// Una vez que este metodo retorna todos los llamados son seguros hasta que termine
        /// </summary>
        /// <returns></returns>
        public void Bloquear(string nombreBloqueante) {
            Debug.WriteLine(nombreBloqueante + " llama a Bloqueable: Intentando obtener bloqueo sobre " + this.Nombre);
            if (Monitor.TryEnter(candado)) {
                // Se espera a que termine el ciclo actual
                controlador.Esperar(1);
                Debug.WriteLine(nombreBloqueante + " llama a Bloqueable: Bloqueo obtenido en " + this.Nombre);
            } else {
                Debug.WriteLine(nombreBloqueante + " llama a Bloqueable: Bloque NO obtenido en " + this.Nombre);
                throw new RebootNeededException();
            }
            // Una vez obtenido el candado este metodo puede devolver control y todos los llamados son seguros, 
            // hasta que se llame al metodo Desbloquear
        }

        /// <summary>
        /// Este metodo se encarga de desbloquear un objeto bloqueable para que alguien mas lo pueda solicitar
        /// </summary>
        public void Desbloquear(string nombreDesbloqueante) {
            Debug.WriteLine(nombreDesbloqueante + " llama a Bloqueable: Bloqueo quitado de " + this.Nombre);
            Monitor.Exit(candado);
        }

        public bool EstaBloqueado() {
            return Monitor.IsEntered(candado);
        }

    }
}
