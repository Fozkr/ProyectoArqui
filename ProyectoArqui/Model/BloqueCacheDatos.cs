using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Este objeto representa un bloqueMemoria de la Cache de Datos.
    /// Hereda de Bloque para ampliar la funcionalidad del mismo con los
    /// datos que la solicitante requiere almacenar.
    /// </summary>
    class BloqueCacheDatos : Bloque {

        private Controlador controlador;
        private CacheDatos cache;
        private EstadosB estado;

        /// <summary>
        /// Constructor para crear los bloques por defecto de la cache
        /// </summary>
        /// <param name="controlador">Controlador de la simulacion</param>
        /// <param name="solicitante">Cache dueña del bloqueMemoria</param>
        public BloqueCacheDatos(Controlador controlador, CacheDatos cache)
            : base(-1, -1, -1) {
            this.controlador = controlador;
            this.cache = cache;
            this.estado = EstadosB.Invalido;
        }

        /// <summary>
        /// Constructor de la clase BloqueCacheDatos.
        /// "Trae" el bloqueMemoria de la memoria correspondiente.
        /// Por defecto los bloques se traen con estado compartido.
        /// </summary>
        /// <param name="bloqueMemoria">Bloque a copiar</param>
        /// <param name="controlador">Controlador de la simulación</param>
        /// <param name="solicitante">Cache dueña de este bloqueMemoria</param>
        /// <param name="vieneDeMemoria">Indica si el bloqueMemoria viene de memoria (true) o de otra solicitante (false)</param>
        public BloqueCacheDatos(Bloque bloque, Controlador controlador, CacheDatos cache, bool vieneDeMemoria)
            : base(bloque) {

            // Asigna el controlador y la cache
            this.controlador = controlador;
            this.cache = cache;

            Debug.Assert(cache.EstaBloqueado());
            Debug.Assert(Directorio.EstaBloqueado());

            // espera que el bloque venga de memoria
            EsperarTraida(vieneDeMemoria);

            // Se pone el bloque como compartido
            this.estado = EstadosB.Compartido;

            // Se refleja el cambio en el directorio
            // Se pone como compartido en el directorio
            Directorio.AgregarUsuarioBloque(cache, this);
        }

        /// <summary>
        /// Para obtener la cache de datos que contiene este bloqueMemoria
        /// </summary>
        public CacheDatos Cache {
            get {
                return cache;
            }
        }

        /// <summary>
        /// Para obtener el directorio dueño de este bloqueMemoria
        /// </summary>
        public Directorio Directorio {
            get {
                return controlador.Directorios[id];
            }
        }

        /// <summary>
        /// Para obtener la memoria principal dueña de este bloqueMemoria
        /// </summary>
        public MemoriaPrincipal MemoriaPrincipal {
            get {
                return controlador.MemoriasPrincipales[id];
            }
        }

        /// <summary>
        /// Para obtener el estado de este bloqueMemoria
        /// </summary>
        public EstadosB Estado {
            get {
                return estado;
            }
            set {
                estado = value;
            }
        }

        /// <summary>
        /// Indica si el bloqueMemoria está en la solicitante local o en una remota
        /// </summary>
        /// <returns>true si esta en la solicitante local, false si es la remota</returns>
        public bool EsLocal() {
            return this.id == cache.ID;
        }

        /// <summary>
        /// Envía este bloqueMemoria a la memoria correspondiente.
        /// Espera el tiempo correspondiente a si es un envio local o remoto.
        /// Pone el estado en Compartido.
        /// </summary>
        public void EnviarAMemoria() {

            // Solo tiene sentido enviar un bloqueMemoria a memoria si está modificado
            Debug.Assert(Cache.EstaBloqueado());
            Debug.Assert(Estado == EstadosB.Modificado);

            // Se espera a que se envien los datos
            EsperarEnvio();

            // Se copia el bloqueMemoria a la memoria principal correspondiente
            int indice = MemoriaPrincipal.GetIndice(Direccion);
            MemoriaPrincipal[indice] = this;

            // Se invalida el bloque
            this.Invalidar();
        }

        /// <summary>
        /// Espera la cantidad de tiempo necesaria a que este bloqueMemoria 
        /// llegue a memoria, según sea una solicitante remota o local
        /// </summary>
        private void EsperarEnvio() {
            if (EsLocal()) {
                controlador.Esperar(EsperaEnvioMemoriaLocal);
            } else {
                controlador.Esperar(EsperaEnvioMemoriaRemota);
            }
        }

        /// <summary>
        /// Espera la cantidad de tiempo necesaria a que este bloqueMemoria
        /// llegue de memoria, según sea una solicitante remota o local
        /// </summary>
        private void EsperarTraida(bool vieneDeMemoria) {
            if (vieneDeMemoria) {
                if (EsLocal()) {
                    controlador.Esperar(EsperaTraerMemoriaLocal);
                } else {
                    controlador.Esperar(EsperaTraerMemoriaRemota);
                }
            } else {
                controlador.Esperar(EsperaTraerCacheRemota);
            }
        }

        /// <summary>
        /// Para poner el bloque en un estado completamente inválido.
        /// </summary>
        public void Invalidar() {

            Debug.Assert(Directorio.EstaBloqueado());
            Debug.Assert(Cache.EstaBloqueado());

            // Quito del directorio
            Directorio.EliminarUsuarioBloque(Cache, this);

            // Invalido el bloque
            this.id = -1;
            this.direccionInicial = -1;
            this.indiceMemoriaPrincipal = -1;
            this.Estado = EstadosB.Invalido;
        }

    }
}
