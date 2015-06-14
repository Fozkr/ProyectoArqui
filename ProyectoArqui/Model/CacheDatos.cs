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
        private Bloque[] bloques = new Bloque[BloquesPorCache];

        // Contiene el estado de cada bloque
        private EstadosB[] estados = new EstadosB[BloquesPorCache];

        // Indica la direccion de memoria inicial de cada bloque de la cache
        private int[] direcciones = new int[BloquesPorCache];

        // Indica cual es el id del dueño de los bloques
        // Para saber cual memoria consultar
        private int[] idDueños = new int[BloquesPorMemoria];

        /// <summary>
        /// Crea una nueva cacheDatos de datos. Utiliza el controlador 
        /// para acceder a otros objetos de la simulación
        /// </summary>
        /// <param name="controlador">Controlador de la simulación</param>
        /// <param name="id">Id de la cache</param>
        public CacheDatos(Controlador controlador, int id)
            : base(controlador, "Cache " + id) {
            this.id = id;
            for (int i = 0; i < BloquesPorCache; ++i) {
                this.bloques[i] = new Bloque(-1);
                this.estados[i] = EstadosB.Invalido;
                this.direcciones[i] = -1; // Da error si se intenta acceder a la posicion -1
                this.idDueños[i] = -1;
            }
        }

        /// <summary>
        /// Propiedad indexada para acceder directamente a los bloques de la cache de datos.
        /// Devuelve y asigna copias de objetos.
        /// </summary>
        /// <param name="index">Índice del bloque a accesar</param>
        /// <returns>Bloque que se quiere accesar</returns>
        public Bloque this[int index] {
            get {
                return bloques[index].Copiar();
            }
            set {
                bloques[index] = value.Copiar();
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
        /// Guarda la direccion de memoria de la primera palabra de cada bloque.
        /// </summary>
        public int[] Direcciones {
            get {
                return this.direcciones;
            }
        }

        /// <summary>
        /// Para obtener el estado de cada bloque
        /// </summary>
        public EstadosB[] Estados {
            get {
                return this.estados;
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
                    switch (estados[i]) {
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
        /// Para obtener la memoria principal de esta cache.
        /// </summary>
        public MemoriaPrincipal MemoriaPrincipal {
            get {
                return controlador.MemoriasPrincipales[id];
            }
        }

        /// <summary>
        /// Para obtener el directorio de esta cache.
        /// </summary>
        public Directorio Directorio {
            get {
                return controlador.Directorios[id];
            }
        }

        /// <summary>
        /// Propiedad para acceder a la estructura interna de la cache de datos.
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
        /// Metodo para que el procesador escriba en una direccion de memoria una palabra
        /// </summary>
        /// <param name="direccionMemoria">Direccion de memoria donde se quiere escribir una palabra</param>
        /// <param name="palabra">Palabra que se quiere escribir</param>
        public void Escribir(int direccionPalabra, int palabra) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo para que el procesador lea una palabra en una direccion de memoria
        /// Si ocurre una excepción del tipo RebootNeededException, entonces se reinicia el proceso de leer el dato.
        /// </summary>
        /// <param name="direccionPalabra">Direccion de memoria donde se quiere leer una palabra</param>
        /// <returns>Devuelve la palabra que se encuentra en la direccion de memoria</returns>
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
            if (info.EsHit()) {

                // Si es Hit se lee la palabra
                palabraLeida = this[info.IndiceCache][info.IndicePalabra];

            } else {

                // Se pregunta si el bloque a reemplazar en mi cache está modificado
                if (estados[info.IndiceCache] == EstadosB.Modificado) {

                    // Bloqueo mi directorio
                    Directorio.Bloquear();

                    // Envio el bloque a memoria
                    controlador.Esperar(EsperaEnvioMemoriaLocal);
                    MemoriaPrincipal[info.IndiceCache] = this[info.IndiceCache];

                    // Invalido la cache
                    estados[info.IndiceCache] = EstadosB.Invalido;

                    // Pongo uncached en directorio
                    Directorio.EliminarUsuarioBloque(info.IndiceMemoria, this);
                    Directorio.SetEstadoBloque(info.IndiceMemoria, EstadosD.Uncached, id);

                    // Desbloqueo mi directorio
                    Directorio.Desbloquear();
                }

                // Bloqueo el directorio que contiene la palabra que busco
                info.Directorio.Bloquear();

                // Consulto la lista de usuarios del bloque que contiene la palabra que busco
                // para ver si alguna cache lo tiene modificado
                List<CacheDatos> usuarios = info.Directorio.GetUsuariosBloque(info.IndiceMemoria);
                usuarios = GetUsuariosBloque(usuarios, info, EstadosB.Modificado);

                // Si el tamaño de la lista es 0, ninguna cache tiene el bloque modificado
                if (usuarios.Count == 0) {

                    // Copio el bloque desde memoria remota
                    controlador.Esperar(EsperaTraerMemoriaRemota);
                    this[info.IndiceCache] = info.MemoriaPrincipal[info.IndiceMemoria];
                    idDueños[info.IndiceCache] = info.MemoriaPrincipal.ID; // Indico en la cache quien es el dueño del bloque

                    // Pongo C en mi Cache
                    estados[info.IndiceCache] = EstadosB.Compartido;

                    // Agrego C en directorio
                    info.Directorio.SetEstadoBloque(info.IndiceMemoria, EstadosD.Compartido, id);
                    info.Directorio.AgregarUsuarioBloque(info.IndiceMemoria, this);

                    // Se lee la palabra
                    palabraLeida = this[info.IndiceCache][info.IndicePalabra];

                    // Desbloqueo el directorio
                    info.Directorio.Desbloquear();

                } else {

                    // Bloqueo la cache remota
                    CacheDatos remota = usuarios[0];
                    remota.Bloquear();

                    // Obtengo la memoria dueña del bloque modificado
                    int idMemoria = remota.idDueños[info.IndiceCache];
                    MemoriaPrincipal memoria = controlador.MemoriasPrincipales[idMemoria];

                    // Envio el dato de la cache remota a la memoria correspondiente
                    // que podría o no ser la memoria principal de la misma cache
                    if (remota.id == idMemoria) {
                        controlador.Esperar(EsperaEnvioMemoriaLocal);
                    } else {
                        controlador.Esperar(EsperaEnvioMemoriaRemota);
                    }
                    memoria[info.IndiceMemoria] = remota[info.IndiceCache];

                    // Copio el dato en mi cache
                    controlador.Esperar(EsperaTraerCacheRemota);
                    this[info.IndiceCache] = remota[info.IndiceCache];
                    idDueños[info.IndiceCache] = idMemoria;

                    // Pongo C en 2 caches
                    this.Estados[info.IndiceCache] = EstadosB.Compartido;
                    remota.Estados[info.IndiceCache] = EstadosB.Compartido;

                    // Pongo C en directorio
                    info.Directorio.SetEstadoBloque(info.IndiceMemoria, EstadosD.Compartido, id);

                    // Me agrego a la lista de usuarios
                    // No agrego la cache remota porque ya está en la lista
                    info.Directorio.AgregarUsuarioBloque(info.IndiceMemoria, this);

                    // Se lee la palabra
                    palabraLeida = this[info.IndiceCache][info.IndicePalabra];

                    // Desbloqueo el directorio
                    info.Directorio.Desbloquear();

                }
            }

            // Desbloqueo la cache
            this.Desbloquear();

            // Devuelvo la palabra leída
            return palabraLeida;
        }

        /// <summary>
        /// Procesa una lista de caches en busqueda de las caches que tienen un bloque
        /// modificado o compartido. Si el bloque está modificado, entonces este método 
        /// debería devolver una lista de tamaño 1. Para pruebas se coloca un Assert en 
        /// el método que verifica esto.
        /// </summary>
        /// <param name="usuarios">Lista de Caches</param>
        /// <param name="info">Información de la palabra buscada</param>
        /// <param name="estado">Estado que se busca</param>
        /// <returns></returns>
        public List<CacheDatos> GetUsuariosBloque(List<CacheDatos> usuarios, InformacionPalabra info, EstadosB estado) {
            List<CacheDatos> resultado = new List<CacheDatos>();
            foreach (CacheDatos cache in usuarios) {
                if (cache.Estados[info.IndiceCache] == estado) {
                    resultado.Add(cache);
                }
            }
            // El propósito de este assert es verificar que la lista de usuarios solo
            // puede contener una cache con el dato modificado.
            // Si este assert da errores, debe haber un error en algún sitio del código.
            Debug.Assert(estado == EstadosB.Modificado && resultado.Count == 1);
            return resultado;
        }

    }
}
