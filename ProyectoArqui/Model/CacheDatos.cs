using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {
    /// <summary>
    /// Representa una cacheDatos de datos para un procesador.
    /// Se compone de 4 bloques.
    /// </summary>
    class CacheDatos : Bloqueable {

        private MemoriaPrincipal memoriaPrincipal;
        private Bloque[] bloquesDeCache = new Bloque[4];
        private int idCache;
        private Directorio directorio;

        // Contiene el estado de cada bloque
        //      'I' Invalido
        //      'C' Compartido
        //      'M' Modificado
        private char[] estadosDeBloque = new char[4];

        // Indica el numero de bloque en memoria principal
        private int[] numerosDeBloque = new int[4];

        // Para conocer los otros directorios y las otras caches
        // caches[idCache] es esta cache
        // directorios[idCache] es el directorio de esta cache
        private Directorio[] directorios;
        private CacheDatos[] caches;

        // Para actualizar o no la interfaz
        private bool modificado = true;
        public bool Modificado {
            get {
                return this.modificado;
            }
            set {
                this.modificado = value;
            }
        }

        public Bloque[] Bloques {
            get {
                return bloquesDeCache;
            }
        }

        // Para obtener los numerosDeBloque
        public int[] NumerosDeBloque {
            get {
                return this.numerosDeBloque;
            }
        }

        // Para obtener los estadosDeBloque
        public char[] EstadosDeBloque {
            get {
                return this.estadosDeBloque;
            }
        }

        public CacheDatos[] Caches {
            get {
                return this.caches;
            }
            set {
                this.caches = value;
            }
        }

        public Directorio[] Directorios {
            get {
                return this.directorios;
            }
            set {
                this.directorios = value;
            }
        }

        // Numero de Bloque = direccionMemoria / 16
        // Numeros de Palabra = (direccionMemoria % 16) / 4

        /// <summary>
        /// Crea una nueva cacheDatos de datos.
        /// Recibe una memoria principal para la cual sirve de cacheDatos y
        /// un controlador que utiliza para Esperar cierta cantidad de ticks 
        /// cuando ocurren fallos de cacheDatos
        /// </summary>
        /// <param name="memoriaPrincipal">Memoria principal de la que se reciben y escriben bloques</param>
        /// <param name="controlador">controlador que controla el reloj de la simulacion</param>
        public CacheDatos(Directorio directorio, MemoriaPrincipal memoriaPrincipal, Controlador controlador, int idProcesador)
            : base(controlador) {
            this.idCache = idProcesador;
            this.Nombre = "Cache " + idProcesador;
            this.memoriaPrincipal = memoriaPrincipal;
            this.directorio = directorios[idProcesador];
            for (int i = 0; i < estadosDeBloque.Length; ++i) {
                this.bloquesDeCache[i] = new Bloque();
                this.estadosDeBloque[i] = 'I';
                this.numerosDeBloque[i] = -1; // Da error si se intenta acceder a la posicion -1
            }
        }

        public void Escribir(int direccionMemoria, int palabra) {
            throw new NotImplementedException();
        }

        public int Leer(int direccionMemoria) {
            int palabraLeida = -1;
            bool terminado = false;
            while (!terminado) {
                try {
                    palabraLeida = Leer(new InformacionPalabra(direccionMemoria, this, directorios));
                    terminado = true;
                } catch (RebootNeededException) {
                    terminado = false;
                }
            }
            return palabraLeida;
        }

        private int Leer(InformacionPalabra info) {
            int palabraLeida = -1;
            this.Bloquear();
            if (info.EsHit()) {
                palabraLeida = info.Palabra;
            } else {
                if (info.EstadoBloque == 'M') {
                    directorio.Bloquear();
                    EnviarBloqueAMemoria(info, this);
                    directorio.Desbloquear();
                }



            }
            return palabraLeida;
        }

        /// <summary>
        /// Escribe un bloque de memoria de la cacheDatos en su posicion respectiva en la memoria principal
        /// </summary>
        /// <param name="indiceDeCache">Indice del bloque que se quiere enviar a memoria principal</param>
        private void EnviarBloqueAMemoria(InformacionPalabra info, CacheDatos cachePeticion) {
            if (this == cachePeticion) {
                controlador.Esperar(16);
            } else {
                controlador.Esperar(32);
            }
            memoriaPrincipal.SetBloque(info.NumeroBloque, info.Bloque);
            info.EstadoBloque = 'I';
            // TODO Mejorar el llamado en la parte de local
            directorio.SetEstadoBloque(info.NumeroBloque, 'U', this == cachePeticion);
        }

        /// <summary>
        /// Convierte los bloques de la Cache en un vector para las vistas
        /// </summary>
        /// <returns>Vector de datos</returns>
        public int[] ToArray() {
            int[] vector = new int[16];
            int i = 0;
            foreach (Bloque bloque in bloquesDeCache) {
                foreach (int palabra in bloque.ToArray()) {
                    vector[i++] = palabra;
                }
            }
            return vector;
        }

    }
}
