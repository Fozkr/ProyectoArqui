using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {

    /// <summary>
    /// El propósito de esta clase es sintetizar la información
    /// necesitada con respecto a una palabra. Principalmente:
    ///    ¿En cual memoria está?
    ///    ¿Cual directorio hay que consultar?
    ///    ¿Se encuentra en alguna cache?
    /// </summary>
    class InformacionPalabra : Constantes {

        private Controlador controlador;
        private CacheDatos solicitante;
        private int id;
        private int direccionPalabraLocal;
        private int direccionBloque;
        private int indiceBloqueMemoria;
        private int indicePalabra;

        /// <summary>
        /// Obtiene la informacion necesaria con respecto a la operacion que se quiere leer
        /// </summary>
        /// <param name="controlador"></param>
        /// <param name="solicitante"></param>
        /// <param name="direccionPalabra"></param>
        public InformacionPalabra(Controlador controlador, CacheDatos solicitante, int direccionPalabra) {
            this.controlador = controlador;
            this.solicitante = solicitante;
            this.id = direccionPalabra / BytesPorMemoria;
            this.direccionPalabraLocal = direccionPalabra % BytesPorMemoria;
            this.indiceBloqueMemoria = this.direccionPalabraLocal / BytesPorBloque;
            this.indicePalabra = this.direccionPalabraLocal % BytesPorBloque;
            // Los bloques guardan su direccion inicial, se puede generar con una formula
            // pero es mas facil ir y consultarla al bloque directamente.
            this.direccionBloque = controlador.MemoriasPrincipales[id][indiceBloqueMemoria].DireccionBloque;
        }

        /// <summary>
        /// Devuelve el id de la memoria dueña de la palabra
        /// </summary>
        public int ID {
            get {
                return id;
            }
        }

        /// <summary>
        /// Devuelve el directorio dueño de la palabra buscada.
        /// En este directorio debe consultarse quien tiene la palabra buscada
        /// </summary>
        public Directorio Directorio {
            get {
                return controlador.Directorios[id];
            }
        }

        /// <summary>
        /// Devuelve la memoria principal dueña de la palbra buscada.
        /// </summary>
        public MemoriaPrincipal MemoriaPrincipal {
            get {
                return controlador.MemoriasPrincipales[id];
            }
        }

        /// <summary>
        /// Devuelve el numero de palabra de la palabra buscada
        /// dentro del bloque donde se encuentra.
        /// </summary>
        public int IndicePalabra {
            get {
                return indicePalabra;
            }
        }

        /// <summary>
        /// Devuelve la posicion en cache donde debe ubicarse el bloque
        /// que contiene a la palabra buscada
        /// </summary>
        public int IndiceCache {
            get {
                return MapeoDirecto(indiceBloqueMemoria);
            }
        }

        /// <summary>
        /// Devuelve la posicion en memoria donde debe ubicarse el bloque
        /// que contiene a la palabra buscada.
        /// </summary>
        public int IndiceMemoria {
            get {
                return indiceBloqueMemoria;
            }
        }

        /// <summary>
        /// Devuelve la direccion del bloque donde debe ubicarse el bloque
        /// que contiene a la palabra buscada.
        /// </summary>
        public int DireccionBloque {
            get {
                return direccionBloque;
            }
        }

        /// <summary>
        /// Averigua si el bloque donde se encuentra la cache está en la cache de datos del solicitante
        /// </summary>
        /// <returns>true si es Hit, false si es Miss</returns>
        public bool EsHit() {
            return solicitante.Direcciones[IndiceCache] == direccionBloque;
        }

        /// <summary>
        /// Indica donde deberia ubicarse determinado bloque de memoria principal en la cacheDatos
        /// </summary>
        /// <param name="indiceBloqueMemoria">Numero de bloque en Memoria que se necesita saber su indice respectivo en cacheDatos</param>
        /// <returns>Devuelve el indice que el bloque deberia tener en la cacheDatos</returns>
        private int MapeoDirecto(int indiceBloqueMemoria) {
            return indiceBloqueMemoria % BloquesPorCache;
        }
    }
}
