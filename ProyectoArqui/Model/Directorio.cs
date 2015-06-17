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
    /// Contiene quien está utilizando cada uno de los bloques de memoriaPrincipal local.
    /// </summary>
    class Directorio : Bloqueable {

        private int id;

        // Contiene el estado de cada bloqueMemoria
        private EstadosD[] estados = new EstadosD[BloquesPorMemoria];

        //Matriz dinámica de control para saber cuál o cuáles cachés tienen cada uno de los bloques
        private List<int>[] usuarios = new List<int>[BloquesPorMemoria];

        /// <summary>
        /// Se inicializa directorio 
        /// </summary>
        /// <param name="controlador">Se asigna controlador</param>
        /// <param name="id">ID del directorio</param>
        public Directorio(Controlador controlador, int id)
            : base(controlador, "Directorio " + id) {
            this.id = id;
            for (int i = 0; i < BloquesPorMemoria; i++) {
                estados[i] = EstadosD.Uncached;
                usuarios[i] = new List<int>();
            }
        }

        /// <summary>
        /// Devuelve los estados del directorio
        /// </summary>
        public EstadosD[] Estados {
            get {
                return estados;
            }
        }

        /// <summary>
        /// Agrega una cache como usuaria de un bloque. Siempre se agregan los bloques en C.
        /// </summary>
        /// <param name="solicitante">Cache de datos usuaria del bloqueMemoria</param>
        /// <param name="bloqueMemoria">Bloque siendo utilizado</param>
        public void AgregarUsuarioBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            Esperar(solicitante);

            // Debido a la implementación los bloques siempre se agregan como compartidos al directorio
            // Si la cache los modifica, luego llama al método de ModificarBloque del directorio
            Debug.Assert(EstaBloqueado());
            Debug.Assert(estados[bloque.IndiceMemoriaPrincipal] != EstadosD.Modificado);
            Debug.Assert(bloque.Estado != EstadosB.Modificado);
            Debug.Assert(!usuarios[bloque.IndiceMemoriaPrincipal].Contains(solicitante.ID));
            Debug.Assert(bloque.ID == this.id);
            Debug.Assert(solicitante[bloque.IndiceCache].Direccion == bloque.Direccion);

            usuarios[bloque.IndiceMemoriaPrincipal].Add(solicitante.ID);
            estados[bloque.IndiceMemoriaPrincipal] = EstadosD.Compartido;
        }

        public void ModificarBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            Esperar(solicitante);

            Debug.Assert(EstaBloqueado());
            Debug.Assert(usuarios[bloque.IndiceMemoriaPrincipal].Count == 1);

            Estados[bloque.IndiceMemoriaPrincipal] = EstadosD.Modificado;
        }

        /// <summary>
        /// Método que devuelve la cache que modificó un bloqueMemoria.
        /// Devuelve null si nadie ha modificado el bloqueMemoria.
        /// </summary>
        /// <param name="solicitante">Solicitante del cambio</param>
        /// <param name="bloqueMemoria">Bloque para obtener el id</param>
        /// <returns>Cache que modificó una palabra del bloqueMemoria</returns>
        public CacheDatos GetUsuarioQueModifica(CacheDatos solicitante, Bloque bloque) {
            Esperar(solicitante);

            Debug.Assert(EstaBloqueado());

            CacheDatos cache = null;
            int indice = bloque.IndiceMemoriaPrincipal;
            if (estados[indice] == EstadosD.Modificado && usuarios[indice].Count == 1) {
                cache = controlador.CachesDatos[usuarios[indice][0]];
            }
            return cache;
        }

        /// <summary>
        /// Método que devuelve cuantos usuarios comparten un bloque menos yo
        /// </summary>
        /// <param name="bloqueMemoria">Bloque para obtener el índice de memoria principal</param>
        /// <returns>Lista de Caches que comparten el bloqueMemoria</returns>
        public int CountUsuariosQueComparten(CacheDatos solicitante, Bloque bloque) {
            Esperar(solicitante);

            Debug.Assert(EstaBloqueado());

            List<int> tmp = new List<int>(usuarios[bloque.IndiceMemoriaPrincipal]);
            tmp.Remove(solicitante.ID);

            return tmp.Count;
        }

        /// <summary>
        /// Se elimina cache de la lista respectiva
        /// </summary>
        /// <param name="solicitante">Cache de datos que ya no es usuaria del bloqueMemoria</param>
        /// <param name="bloqueMemoria">Bloque ya no utilizado</param>
        public void EliminarUsuarioBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            Esperar(solicitante);

            Debug.Assert(EstaBloqueado());
            Debug.Assert(usuarios[bloque.IndiceMemoriaPrincipal].Contains(solicitante.ID));
            Debug.Assert(bloque.ID == this.id);

            usuarios[bloque.IndiceMemoriaPrincipal].Remove(solicitante.ID);
            if (usuarios[bloque.IndiceMemoriaPrincipal].Count == 0) {
                estados[bloque.IndiceMemoriaPrincipal] = EstadosD.Uncached;
            }
        }

        /// <summary>
        /// Se invalida el bloqueMemoria en todas las cachés que lo usen
        /// </summary>
        /// <param name="solicitante">Cache de Datos solicitante</param>
        /// <param name="bloqueMemoria">Bloque para obtener la dirección</param>
        public void InvalidarBloque(CacheDatos solicitante, Bloque bloque) {
            Esperar(solicitante);

            // Solo se puede invalidar un bloqueMemoria si alguien lo está compartiendo
            Debug.Assert(EstaBloqueado());
            Debug.Assert(estados[bloque.IndiceMemoriaPrincipal] == EstadosD.Compartido);
            Debug.WriteLine("Cache " + solicitante.ID + ": Invalidando a quienes comparten el bloque " + bloque.Direccion);

            //Obtengo quienes son las caches que tienen bloqueN compartido
            List<int> lectores = usuarios[bloque.IndiceMemoriaPrincipal];

            // Creo una copia de la lista de lectores sin la cache solicitante
            // Si es que se encuentra
            List<int> tmp = new List<int>(lectores);
            tmp.Remove(solicitante.ID);

            // TODO Mejorar este código para que reintente sobre las caches
            // Mientras no falle consecutivamente en todas
            foreach (int id in tmp) {

                CacheDatos cache = controlador.CachesDatos[id];

                // Bloqueo la cache
                cache.Bloquear(this.Nombre);

                BloqueCacheDatos bloqueCache = cache[bloque.IndiceCache];

                Debug.Assert(bloqueCache.Direccion == bloque.Direccion);

                // Invalido el bloque
                bloqueCache.Invalidar();

                // Debloqueo la cache
                cache.Desbloquear(this.Nombre);
            }

            // O no hay nadie compartiendo el bloque o la solicitante es la unica que lo comparte
            Debug.Assert(lectores.Count == 0 || lectores[0] == solicitante.ID && lectores.Count == 1);
        }

        /// <summary>
        /// Cantidad de ciclos a esperar en caso de que se trate de una operación local o remota
        /// </summary>
        /// <param name="solicitante">Cache de Datos solicitante</param>
        private void Esperar(CacheDatos solicitante) {
            if (id == solicitante.ID) {
                controlador.Esperar(EsperaOperacionDirectorioLocal);
            } else {
                controlador.Esperar(EsperaOperacionDirectorioRemoto);
            }
        }
    }
}
