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
    ///    ¿En cual memoriaPrincipal está?
    ///    ¿Cual directorio hay que consultar?
    ///    ¿Se encuentra en alguna solicitante?
    /// </summary>
    class InformacionPalabra : Constantes {

        private Controlador controlador;
        private CacheDatos solicitante;
        private Bloque bloqueMemoria;
        private int id;
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
            int direccionPalabraLocal = direccionPalabra % BytesPorMemoria;
            int indiceBloqueMemoria = direccionPalabraLocal / BytesPorBloque;
            this.indicePalabra = direccionPalabraLocal % BytesPorBloque;
            this.bloqueMemoria = controlador.MemoriasPrincipales[id][indiceBloqueMemoria];
        }

        /// <summary>
        /// Devuelve el id de la memoriaPrincipal dueña de la palabra
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
        /// Devuelve la memoriaPrincipal memoriaPrincipal dueña de la palbra buscada.
        /// </summary>
        public MemoriaPrincipal MemoriaPrincipal {
            get {
                return controlador.MemoriasPrincipales[id];
            }
        }

        public int IndicePalabra {
            get {
                return indicePalabra;
            }
        }

        public int IndiceCache {
            get {
                return bloqueMemoria.IndiceCache;
            }
        }

        /// <summary>
        /// Devuelve el bloqueMemoria de memoria donde se encuentra la palabra buscada.
        /// La copia no contiene datos de palabras, solo información del bloqueMemoria para
        /// consultar el indice en memoria, el indice en cache, entre otros.
        /// </summary>
        public Bloque Bloque {
            get {
                return bloqueMemoria;
            }
        }

        /// <summary>
        /// Averigua si el bloqueMemoria donde se encuentra la solicitante está en la solicitante de datos del solicitante
        /// </summary>
        /// <returns>true si es Hit, false si es Miss</returns>
        public bool EsHit() {
            return solicitante[Bloque.IndiceCache].Direccion == Bloque.Direccion;
        }

    }
}
