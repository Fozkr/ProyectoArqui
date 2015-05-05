using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class CacheDatos
    {

        MemoriaPrincipal memoriaPrincipal;

        // Contiene los 4 bloques actuales en cache
        Bloque[] cache = new Bloque[4];

        // Contiene el estado de cada bloque
        // 'I' Invalido
        // 'C' Compartido
        // 'M' Modificado
        char[] estados = new char[4];

        // Indica el numero de bloque en memoria principal
        int [] numBloque = new int[4];

        // NumBloque = dirMem / 16
        // NumPalabra = (dirMem % 16) / 4

        public CacheDatos(MemoriaPrincipal memoriaPrincipal)
        {
            this.memoriaPrincipal = memoriaPrincipal;
            for (int i = 0; i < estados.Length; ++i)
            {
                estados[i] = 'I';
                numBloque[i] = -1;
            }
        }

        public void escribir(int dirMem, int elemento)
        {
            int numBloque = dirMem / 16;
            int numPalabra = (dirMem % 16) / 4;
            int indiceEnCache = getIndiceDeBloqueEnCache(numBloque);
            if (indiceEnCache == -1)
            {
                indiceEnCache = traerDeMemoria(numBloque);
            }
            estados[indiceEnCache] = 'M';
            cache[indiceEnCache].set(numPalabra, elemento);
            // Aqui se deberia invalidar en las otras caches el bloque "numBloque" a traves del bus
        }

        private int getIndiceDeBloqueEnCache(int bloqueBuscado) 
        {
            int indice = mapeoDirecto(bloqueBuscado);
            if (numBloque[indice] != bloqueBuscado)
            {
                indice = -1;
            }
            return indice;
        }

        public int leer(int dirMem)
        {
            int numBloque = dirMem / 16;
            int numPalabra = (dirMem % 16) / 4;
            int indiceEnCache = getIndiceDeBloqueEnCache(numBloque);
            if (indiceEnCache == -1)
            {
                indiceEnCache = traerDeMemoria(numBloque);
            }
            return cache[indiceEnCache].get(numPalabra);
        }

        private void enviarAMemoria(int indiceDeCache) 
        {
            memoriaPrincipal.set(numBloque[indiceDeCache], cache[indiceDeCache]);
        }

        private int traerDeMemoria(int numBloque)
        {
            int indiceEnCache = mapeoDirecto(numBloque);
            cache[indiceEnCache] = memoriaPrincipal.get(numBloque);
            this.numBloque[indiceEnCache] = numBloque;
            estados[indiceEnCache] = 'C';
            return indiceEnCache;
        }

        private int mapeoDirecto(int numBloque)
        {
            return numBloque % 4;
        }

    }
}
