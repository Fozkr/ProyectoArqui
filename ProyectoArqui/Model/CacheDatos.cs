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

        // Para obtener los numerosDeBloque
        public int[] Numeros {
            get {
                return this.numerosDeBloque;
            }
        }

        // Para obtener los estadosDeBloque
        public char[] Estados {
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
            for (int i = 0; i < estadosDeBloque.Length; ++i) {
                this.bloquesDeCache[i] = new Bloque();
                this.estadosDeBloque[i] = 'I';
                this.numerosDeBloque[i] = -1; // Da error si se intenta acceder a la posicion -1
            }
        }


        /// <summary>
        /// Metodo para que el procesador escriba en una direccion de memoria una palabra
        /// </summary>
        /// <param name="direccionMemoria">Direccion de memoria donde se quiere escribir una palabra</param>
        /// <param name="palabra">Palabra que se quiere escribir</param>
        public void Escribir(int direccionMemoria, int palabra) {
            //this.Bloquear();
            int numeroDeBloque = direccionMemoria / 16;
            int numeroDePalabraEnBloque = (direccionMemoria % 16) / 4;
            int indiceEnCache = GetIndiceBloqueEnCache(numeroDeBloque);
            //directorio.InvalidarBloque(numeroDeBloque);
            bloquesDeCache[indiceEnCache].SetPalabra(numeroDePalabraEnBloque, palabra);
            estadosDeBloque[indiceEnCache] = 'M';
            Modificado = true; // Indica que hubo un cambio en un bloque de la cache
            //directorio.SetEstadoBloque(numeroDeBloque, 'M', true);
            this.Desbloquear();
        }

        /// <summary>
        /// Metodo para que el procesador lea una palabra en una direccion de memoria
        /// </summary>
        /// <param name="direccionMemoria">Direccion de memoria donde se quiere leer una palabra</param>
        /// <returns>Devuelve la palabra que se encuentra en la direccion de memoria</returns>
        public int Leer(int direccionMemoria) {
            //this.Bloquear();
            int numeroDeBloque = direccionMemoria / 16;
            int numeroDePalabraEnBloque = (direccionMemoria % 16) / 4;
            int indiceEnCache = GetIndiceBloqueEnCache(numeroDeBloque);
            int resultado = bloquesDeCache[indiceEnCache].GetPalabra(numeroDePalabraEnBloque);
            this.Desbloquear();
            return resultado;
        }

        /// <summary>
        /// Devuelve el indice en el cual se encuentra en la cacheDatos el bloque se quiere de memoria.
        /// Si el bloque de memoria no está en la cacheDatos, se manda a traer de memoria con el metodo ReemplazarBloque
        /// </summary>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque en el que se necesita escribir</param>
        /// <returns>Devuelve el indice en el cual se encuentra el bloque buscado en la cacheDatos</returns>
        private int GetIndiceBloqueEnCache(int numeroDeBloqueEnMemoria) {
            int i = MapeoDirecto(numeroDeBloqueEnMemoria);
            // El numero del bloque es -1 en la primera corrida cuando la cache está vacía
            if (numerosDeBloque[i] != numeroDeBloqueEnMemoria || numerosDeBloque[i] == -1) {
                ReemplazarBloque(i, numeroDeBloqueEnMemoria);
            }
            return i;
        }

        /// <summary>
        /// Se encarga de traer un bloque de la memoria, pero verificando si el bloque esta modificado.
        /// Si el bloque se encuentra modificado, entonces lo manda a escribir en memoria antes de reemplazarlo.
        /// </summary>
        /// <param name="indiceEnCache">indice en cacheDatos donde se quiere traer un bloque</param>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque en memoria que se quiere poner en indiceEnCache</param>
        private void ReemplazarBloque(int indiceEnCache, int numeroDeBloqueEnMemoria) {
            if (estadosDeBloque[indiceEnCache] == 'M') {
                EnviarBloqueAMemoria(indiceEnCache);
                //directorio.SetEstadoBloque(numeroDeBloqueEnMemoria, 'U', true);
            }
            TraerBloqueDeMemoria(indiceEnCache, numeroDeBloqueEnMemoria);
        }

        /// <summary>
        /// Escribe un bloque de memoria de la cacheDatos en su posicion respectiva en la memoria principal
        /// </summary>
        /// <param name="indiceDeCache">Indice del bloque que se quiere enviar a memoria principal</param>
        private void EnviarBloqueAMemoria(int indiceDeCache) {
            // Se esperan 16 ticks de reloj del controlador
            controlador.Esperar(16);
            memoriaPrincipal.SetBloque(numerosDeBloque[indiceDeCache], bloquesDeCache[indiceDeCache]);
            estadosDeBloque[indiceDeCache] = 'C';
            //directorio.SetEstadoBloque(numerosDeBloque[indiceDeCache], 'C', true);
        }

        /// <summary>
        /// Trae un bloque de memoria. 
        /// Dura 16 ticks de reloj.
        /// </summary>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque en memoria que se quiere traer</param>
        private void TraerBloqueDeMemoria(int indiceEnCache, int numeroDeBloqueEnMemoria) {
            // Se esperan 16 ticks de reloj del controlador
            controlador.Esperar(16);
            bloquesDeCache[indiceEnCache] = memoriaPrincipal.GetBloque(numeroDeBloqueEnMemoria);
            this.numerosDeBloque[indiceEnCache] = numeroDeBloqueEnMemoria;
            estadosDeBloque[indiceEnCache] = 'C';
            // Se trajo un bloque de memoria, las vistas deben ser notificadas
            Modificado = true;
            //directorio.SetEstadoBloque(numeroDeBloqueEnMemoria, 'C', true);
        }

        /// <summary>
        /// Indica donde deberia ubicarse determinado bloque de memoria principal en la cacheDatos
        /// </summary>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque que se necesita saber su posible indice en cacheDatos</param>
        /// <returns>Devuelve el indice que el bloque deberia tener en la cacheDatos</returns>
        private int MapeoDirecto(int numeroDeBloqueEnMemoria) {
            // 4 es el numero de bloques de la cacheDatos
            return numeroDeBloqueEnMemoria % 4;
        }

        public void Invalidar(int numeroBloqueMemoria) {
            int numeroBloqueCache = MapeoDirecto(numeroBloqueMemoria);
            estadosDeBloque[numeroBloqueCache] = 'I';
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
