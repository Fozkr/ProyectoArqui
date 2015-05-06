using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    /// <summary>
    /// Representa una cache de datos para un procesador.
    /// Se compone de 4 bloques.
    /// </summary>
    class CacheDatos
    {

        Controlador controlador;

        MemoriaPrincipal memoriaPrincipal;

        Bloque[] bloquesDeCache = new Bloque[4];

        // Contiene el estado de cada bloque
        //      'I' Invalido
        //      'C' Compartido
        //      'M' Modificado
        char[] estadosDeBloque = new char[4];

        // Indica el numero de bloque en memoria principal
        int [] numerosDeBloque = new int[4];

        // Numero de Bloque = direccionMemoria / 16
        // Numeros de Palabra = (direccionMemoria % 16) / 4

        /// <summary>
        /// Crea una nueva cache de datos.
        /// Recibe una memoria principal para la cual sirve de cache y
        /// un controlador que utiliza para esperar cierta cantidad de ticks 
        /// cuando ocurren fallos de cache
        /// </summary>
        /// <param name="memoriaPrincipal">Memoria principal de la que se reciben y escriben bloques</param>
        /// <param name="controlador">controlador que controla el reloj de la simulacion</param>
        public CacheDatos(MemoriaPrincipal memoriaPrincipal, Controlador controlador)
        {
            this.memoriaPrincipal = memoriaPrincipal;
            this.controlador = controlador;
            for (int i = 0; i < estadosDeBloque.Length; ++i)
            {
                this.estadosDeBloque[i] = 'I';
                this.numerosDeBloque[i] = -1; // Da error si se intenta acceder a la posicion -1
            }
        }

        /// <summary>
        /// Metodo para que el procesador escriba en una direccion de memoria una palabra
        /// </summary>
        /// <param name="direccionMemoria">Direccion de memoria donde se quiere escribir una palabra</param>
        /// <param name="palabra">Palabra que se quiere escribir</param>
        public void Escribir(int direccionMemoria, int palabra)
        {
            int numeroDeBloque = direccionMemoria / 16;
            int numeroDePalabraEnBloque = (direccionMemoria % 16) / 4;
            int indiceEnCache = GetIndiceBloqueEnCache(numeroDeBloque);
            bloquesDeCache[indiceEnCache].SetPalabra(numeroDePalabraEnBloque, palabra);
            estadosDeBloque[indiceEnCache] = 'M';
            // TODO Aqui se deberia invalidar en las otras caches el bloque "numerosDeBloque" a traves del bus y del directorio
        }

        /// <summary>
        /// Metodo para que el procesador lea una palabra en una direccion de memoria
        /// </summary>
        /// <param name="direccionMemoria">Direccion de memoria donde se quiere leer una palabra</param>
        /// <returns>Devuelve la palabra que se encuentra en la direccion de memoria</returns>
        public int Leer(int direccionMemoria)
        {
            int numeroDeBloque = direccionMemoria / 16;
            int numeroDePalabraEnBloque = (direccionMemoria % 16) / 4;
            int indiceEnCache = GetIndiceBloqueEnCache(numeroDeBloque);
            return bloquesDeCache[indiceEnCache].GetPalabra(numeroDePalabraEnBloque);
        }

        /// <summary>
        /// Devuelve el indice en el cual se encuentra en la cache el bloque se quiere de memoria.
        /// Si el bloque de memoria no está en la cache, se manda a traer de memoria con el metodo ReemplazarBloque
        /// </summary>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque en el que se necesita escribir</param>
        /// <returns>Devuelve el indice en el cual se encuentra el bloque buscado en la cache</returns>
        private int GetIndiceBloqueEnCache(int numeroDeBloqueEnMemoria)
        {
            int i = MapeoDirecto(numeroDeBloqueEnMemoria);
            ReemplazarBloque(i, numeroDeBloqueEnMemoria);
            return i;
        }

        /// <summary>
        /// Se encarga de traer un bloque de la memoria, pero verificando si el bloque esta modificado.
        /// Si el bloque se encuentra modificado, entonces lo manda a escribir en memoria antes de reemplazarlo.
        /// </summary>
        /// <param name="indiceEnCache">indice en cache donde se quiere traer un bloque</param>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque en memoria que se quiere poner en indiceEnCache</param>
        private void ReemplazarBloque(int indiceEnCache, int numeroDeBloqueEnMemoria)
        {
            if (estadosDeBloque[indiceEnCache] == 'M')
            {
                EnviarBloqueAMemoria(indiceEnCache);
            }
            TraerBloqueDeMemoria(indiceEnCache, numeroDeBloqueEnMemoria);
        }

        /// <summary>
        /// Escribe un bloque de memoria de la cache en su posicion respectiva en la memoria principal
        /// </summary>
        /// <param name="indiceDeCache">Indice del bloque que se quiere enviar a memoria principal</param>
        private void EnviarBloqueAMemoria(int indiceDeCache) 
        {
            // Se esperan 16 ticks de reloj del controlador
            controlador.esperar(16);
            memoriaPrincipal.SetBloque(numerosDeBloque[indiceDeCache], bloquesDeCache[indiceDeCache]);
            estadosDeBloque[indiceDeCache] = 'I';
            numerosDeBloque[indiceDeCache] = -1; // El bloque ya no está en la cache
        }

        /// <summary>
        /// Trae un bloque de memoria. 
        /// Dura 16 ticks de reloj.
        /// </summary>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque en memoria que se quiere traer</param>
        private void TraerBloqueDeMemoria(int indiceEnCache, int numeroDeBloqueEnMemoria)
        {
            // Se esperan 16 ticks de reloj del controlador
            controlador.esperar(16);
            bloquesDeCache[indiceEnCache] = memoriaPrincipal.GetBloque(numeroDeBloqueEnMemoria);
            this.numerosDeBloque[indiceEnCache] = numeroDeBloqueEnMemoria;
            estadosDeBloque[indiceEnCache] = 'C';
        }

        /// <summary>
        /// Indica donde deberia ubicarse determinado bloque de memoria principal en la cache
        /// </summary>
        /// <param name="numeroDeBloqueEnMemoria">Numero de bloque que se necesita saber su posible indice en cache</param>
        /// <returns>Devuelve el indice que el bloque deberia tener en la cache</returns>
        private int MapeoDirecto(int numeroDeBloqueEnMemoria)
        {
            // 4 es el numero de bloques de la cache
            return numeroDeBloqueEnMemoria % 4;
        }

    }
}
