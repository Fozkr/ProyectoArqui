using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model {
    class InformacionPalabra {

        private int idDirectorioNecesitado;
        private int direccionMemoria;
        private int direccionMemoriaLocal;
        private int numeroDeBloque;
        private int numeroDePalabraEnBloque;
        private CacheDatos cachePeticion;
        private int indiceBloqueCacheDatos;
        private Directorio[] directorios;

        public int NumeroDelBloque {
            get {
                return numeroDeBloque;
            }
            set {
                this.numeroDeBloque = value;
            }
        }

        public int NumeroDePalabraEnBloque {
            get {
                return numeroDePalabraEnBloque;
            }
            set {
                this.numeroDePalabraEnBloque = value;
            }
        }

        public int IndiceBloqueCacheDatos {
            get {
                return indiceBloqueCacheDatos;
            }
            set {
                this.indiceBloqueCacheDatos = value;
            }
        }

        ///  Tiene que ser hit para que esto funcione
        public int Palabra {
            get {
                return cachePeticion.Bloques[indiceBloqueCacheDatos].GetPalabra(numeroDePalabraEnBloque);
            }
            set {
                cachePeticion.Bloques[indiceBloqueCacheDatos].SetPalabra(numeroDePalabraEnBloque, value);
                cachePeticion.EstadosDeBloque[indiceBloqueCacheDatos] = 'M';
            }
        }

        public Bloque Bloque {
            get {
                return cachePeticion.Bloques[indiceBloqueCacheDatos];
            }
        }

        public int NumeroBloque {
            get {
                return cachePeticion.NumerosDeBloque[indiceBloqueCacheDatos];
            }
            set {
                cachePeticion.NumerosDeBloque[indiceBloqueCacheDatos] = value;
            }
        }

        public char EstadoBloque {
            get {
                return cachePeticion.EstadosDeBloque[indiceBloqueCacheDatos];
            }
            set {
                cachePeticion.EstadosDeBloque[indiceBloqueCacheDatos] = value;
            }
        }

        public Directorio DirectorioNecesitado {
            get {
                return directorios[idDirectorioNecesitado];
            }
        }

        public InformacionPalabra(int direccionMemoria, CacheDatos cachePeticion, Directorio[] directorios) {
            this.idDirectorioNecesitado = direccionMemoria / 128;
            this.direccionMemoria = direccionMemoria;
            this.direccionMemoriaLocal = direccionMemoria % 128;
            this.numeroDeBloque = direccionMemoriaLocal / 16;
            this.numeroDePalabraEnBloque = (direccionMemoriaLocal % 16) / 4;
            this.cachePeticion = cachePeticion;
            this.indiceBloqueCacheDatos = MapeoDirecto(numeroDeBloque);
            this.directorios = directorios;
        }

        public bool EsHit() {
            return cachePeticion.NumerosDeBloque[indiceBloqueCacheDatos] == direccionMemoria;
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

    }
}
