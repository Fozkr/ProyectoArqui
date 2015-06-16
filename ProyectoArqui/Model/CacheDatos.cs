using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProyectoArqui.Controller;
using ProyectoArqui.Model.Exceptions;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Representa una cacheDatos de datos para un procesador.
    /// Se compone de 4 bloques.
    /// </summary>
    class CacheDatos : Bloqueable, IModificable {

        private int id;
        private bool modificado = true;
        private BloqueCacheDatos[] bloques = new BloqueCacheDatos[BloquesPorCache];

        /// <summary>
        /// Crea una nueva cacheDatos de datos. Utiliza el controlador 
        /// para acceder a otros objetos de la simulación
        /// </summary>
        /// <param name="controlador">Controlador de la simulación</param>
        /// <param name="id">Id de la solicitante</param>
        public CacheDatos(Controlador controlador, int id)
            : base(controlador, "Cache " + id) {
            this.id = id;
            for (int i = 0; i < BloquesPorCache; ++i) {
                this.bloques[i] = new BloqueCacheDatos(controlador, this);
            }
        }

        /// <summary>
        /// Propiedad indexada para acceder directamente a los bloques de la solicitante de datos.
        /// </summary>
        /// <param name="index">Índice del bloqueMemoria a accesar</param>
        /// <returns>Bloque que se quiere accesar</returns>
        public BloqueCacheDatos this[int index] {
            get {
                return bloques[index];
            }
            set {
                bloques[index] = value;
                this.Modificado = true;
            }
        }

        /// <summary>
        /// Implementación de la interfaz IModificable.
        /// </summary>
        public bool Modificado {
            get {
                return this.modificado;
            }
            set {
                this.modificado = value;
            }
        }

        /// <summary>
        /// ID de esta solicitante
        /// </summary>
        public int ID {
            get {
                return id;
            }
        }

        /// <summary>
        /// Se convierten las direcciones en un vector de ints para 
        /// mantener compatibilidad con la interfaz
        /// </summary>
        public int[] DireccionesArray {
            get {
                int[] tmp = new int[BloquesPorCache];
                for (int i = 0; i < BloquesPorCache; i++) {
                    tmp[i] = bloques[i].Direccion;
                }
                return tmp;
            }
        }

        /// <summary>
        /// Se convierten los estados en un vector de chars para 
        /// mantener compatibilidad con la interfaz
        /// </summary>
        public char[] EstadosArray {
            get {
                char[] tmp = new char[BloquesPorCache];
                for (int i = 0; i < BloquesPorCache; i++) {
                    switch (bloques[i].Estado) {
                    case EstadosB.Invalido:
                    tmp[i] = 'I';
                    break;
                    case EstadosB.Compartido:
                    tmp[i] = 'C';
                    break;
                    case EstadosB.Modificado:
                    tmp[i] = 'M';
                    break;
                    }
                }
                return tmp;
            }
        }

        /// <summary>
        /// Para obtener la memoriaPrincipal memoriaPrincipal de esta solicitante.
        /// </summary>
        public MemoriaPrincipal MemoriaPrincipal {
            get {
                return controlador.MemoriasPrincipales[id];
            }
        }

        /// <summary>
        /// Para obtener el directorio de esta solicitante.
        /// </summary>
        public Directorio Directorio {
            get {
                return controlador.Directorios[id];
            }
        }

        /// <summary>
        /// Propiedad para acceder a la estructura interna de la solicitante de datos.
        /// Devuelve una copia.
        /// </summary>
        public int[] Array {
            get {
                int[] vector = new int[PalabrasPorCache];
                int k = 0;
                foreach (Bloque bloque in bloques) {
                    foreach (int palabra in bloque.Array) {
                        vector[k++] = palabra;
                    }
                }
                return vector;
            }
        }

        /// <summary>
        /// Metodo para que el procesador escriba en una direccion de memoriaPrincipal una palabra
        /// </summary>
        /// <param name="direccionMemoria">Direccion de memoriaPrincipal donde se quiere escribir una palabra</param>
        /// <param name="palabra">Palabra que se quiere escribir</param>
        public void Escribir(int direccionPalabra, int palabra) {
            bool palabraEscrita = false;
            while (!palabraEscrita) {
                try {
                    Escribir(new InformacionPalabra(controlador, this, direccionPalabra), palabra);
                    palabraEscrita = true;
                } catch (RebootNeededException) {
                }
            }
        }

        public void Escribir(InformacionPalabra info, int palabra) {

            //Se bloquea mi cache
            this.Bloquear();

            // Si es hit
            if (info.EsHit()) {

                BloqueCacheDatos bloqueN = this[info.IndiceCache];

                if (bloqueN.Estado == EstadosB.Modificado) {

                    // Si está en mi cache modificado nada más lo escribo
                    bloqueN[info.IndicePalabra] = palabra;
                    bloqueN.Estado = EstadosB.Modificado;
                    info.Directorio.ModificarBloque(this, bloqueN);

                } else {

                    // Si lo tengo compartido, bloqueo el directorio correspondiente
                    info.Directorio.Bloquear();

                    // Invalido a todos quienes compartan el bloque
                    info.Directorio.InvalidarBloque(this, info.Bloque);

                    // Escribo la palabra
                    bloqueN[info.IndicePalabra] = palabra;
                    bloqueN.Estado = EstadosB.Modificado;
                    info.Directorio.ModificarBloque(this, bloqueN);

                    // Desbloqueo el directorio
                    info.Directorio.Desbloquear();
                }

            } else {

                BloqueCacheDatos bloqueV = this[info.IndiceCache];

                // Se pregunta si el bloque a reemplazar en mi cache está modificado
                if (bloqueV.Estado == EstadosB.Modificado) {

                    // Bloqueo directorio de BloqueV
                    bloqueV.Directorio.Bloquear();

                    // Envio el bloque a memoria
                    // Este método modifica tanto la cache como el directorio (U en directorio e I en Cache)
                    bloqueV.EnviarAMemoria();

                    // Desbloqueo el directorio del BloqueV
                    bloqueV.Directorio.Desbloquear();
                }

                // Bloqueo el directorio que contiene la palabra que busco
                info.Directorio.Bloquear();

                // Consulto el directorio del bloqueMemoria que contiene la palabra que busco
                // para ver si alguna cache lo tiene modificado
                CacheDatos modificante = info.Directorio.GetUsuarioQueModifica(this, info.Bloque);

                if (modificante == null) {  //Si no hay nadie que esté modificando en bloque

                    List<CacheDatos> compartidores = info.Directorio.GetUsuariosQueComparten(this, info.Bloque);

                    if (compartidores.Count != 0) {

                        //Se les invalida el BloqueN a todas las caches que lo tengan compartido
                        info.Directorio.InvalidarBloque(this, info.Bloque);

                    }

                    // Traigo el bloqueMemoria de memoria
                    BloqueCacheDatos bloqueN = new BloqueCacheDatos(info.Bloque, controlador, this, true);
                    this[info.Bloque.IndiceCache] = bloqueN;

                    // Escribo la palabra
                    bloqueN[info.IndicePalabra] = palabra;
                    bloqueN.Estado = EstadosB.Modificado;
                    info.Directorio.ModificarBloque(this, bloqueN);

                } else {

                    // Bloqueo la cache que modificó el dato
                    modificante.Bloquear();

                    // Se envía el bloque en la cache modificante a memoria
                    modificante[info.IndiceCache].EnviarAMemoria();

                    // Desbloqueo la cache que modificó el dato
                    modificante.Desbloquear();

                    // Traigo el dato de la cache modificante
                    BloqueCacheDatos bloqueN = new BloqueCacheDatos(modificante[info.IndiceCache], controlador, this, false);
                    this[info.Bloque.IndiceCache] = bloqueN;

                    // Escribo la palabra
                    bloqueN[info.IndicePalabra] = palabra;
                    bloqueN.Estado = EstadosB.Modificado;
                    info.Directorio.ModificarBloque(this, bloqueN);

                }

                // Desbloqueo el directorio
                info.Directorio.Desbloquear();

            }

            // Desbloqueo mi cache
            Desbloquear();
        }

        /// <summary>
        /// Metodo para que el procesador lea una palabra en una direccion de memoriaPrincipal
        /// Si ocurre una excepción del tipo RebootNeededException, entonces se reinicia el proceso de leer el dato.
        /// </summary>
        /// <param name="direccionPalabra">Direccion de memoriaPrincipal donde se quiere leer una palabra</param>
        /// <returns>Devuelve la palabra que se encuentra en la direccion de memoriaPrincipal</returns>
        public int Leer(int direccionPalabra) {
            int palabra = -1;
            bool palabraLeida = false;
            while (!palabraLeida) {
                try {
                    palabra = Leer(new InformacionPalabra(controlador, this, direccionPalabra));
                    palabraLeida = true;
                } catch (RebootNeededException) {
                }
            }
            return palabra;
        }

        /// <summary>
        /// Método que realmente se encarga de ejecutar la lógica de leer una palabra.
        /// Si no puede bloquear algo, tira una excepción del tipo RebootNeededException.
        /// </summary>
        /// <param name="info">Información de la palabra que se quiere leer</param>
        /// <returns>Palabra que se quiere leer</returns>
        private int Leer(InformacionPalabra info) {
            int palabraLeida = -1;

            // Se bloquea la cache
            this.Bloquear();

            // Se pregunta si es Hit
            if (!info.EsHit()) {

                BloqueCacheDatos bloqueV = this[info.IndiceCache];

                // Se pregunta si el bloque a reemplazar en mi cache está modificado
                if (bloqueV.Estado == EstadosB.Modificado) {

                    // Bloqueo directorio de BloqueV
                    bloqueV.Directorio.Bloquear();

                    // Envio el bloque a memoria
                    // Este método modifica tanto la cache como el directorio (U en directorio e I en Cache)
                    bloqueV.EnviarAMemoria();

                    // Desbloqueo el directorio del BloqueV
                    bloqueV.Directorio.Desbloquear();
                }

                // Bloqueo el directorio que contiene la palabra que busco
                info.Directorio.Bloquear();

                // Consulto el directorio del bloqueMemoria que contiene la palabra que busco
                // para ver si alguna cache lo tiene modificado
                CacheDatos modificante = info.Directorio.GetUsuarioQueModifica(this, info.Bloque);

                if (modificante == null) {

                    // Traigo el bloqueMemoria de memoria
                    this[info.IndiceCache] = new BloqueCacheDatos(info.Bloque, controlador, this, true);

                } else {

                    // Bloqueo la cache que modificó el dato
                    modificante.Bloquear();

                    // Se envía el bloque en la cache modificante a memoria
                    modificante[info.IndiceCache].EnviarAMemoria();

                    // Traigo el dato de la cache modificante
                    this[info.IndiceCache] = new BloqueCacheDatos(modificante[info.IndiceCache], controlador, this, false);

                    // Desbloqueo la cache que modificó el dato
                    modificante.Desbloquear();
                }

                // Desbloqueo el directorio
                info.Directorio.Desbloquear();

            }

            // Leo la palabra
            palabraLeida = this[info.IndiceCache][info.IndicePalabra];

            // Desbloqueo la cache
            this.Desbloquear();

            // Devuelvo la palabra leída
            return palabraLeida;
        }

    }
}
