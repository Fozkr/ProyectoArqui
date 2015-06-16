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
        private List<CacheDatos>[] usuarios = new List<CacheDatos>[BloquesPorMemoria];

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
                usuarios[i] = new List<CacheDatos>();
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
        /// Agrega una solicitante como usuaria de un bloqueMemoria. Modifica el estado del 
        /// directorio de acuerdo al estado del bloqueMemoria. Si se va agregar un 
        /// bloqueMemoria modificado, debe estar modificado previamente a
        /// insertarlo en el directorio!!!
        /// </summary>
        /// <param name="solicitante">Cache de datos usuaria del bloqueMemoria</param>
        /// <param name="bloqueMemoria">Bloque siendo utilizado</param>
        public void AgregarUsuarioBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            // Si se va a agregar un bloqueMemoria cuyo estado es modificado al directorio, entonces la lista correspondiente debería estar vacía
            Debug.Assert(bloque.Estado == EstadosB.Modificado && usuarios[bloque.IndiceMemoriaPrincipal].Count == 0);
            Esperar(solicitante);
            usuarios[bloque.IndiceMemoriaPrincipal].Add(solicitante);
            if (bloque.Estado == EstadosB.Modificado) {
                estados[bloque.IndiceMemoriaPrincipal] = EstadosD.Modificado;
            } else {
                estados[bloque.IndiceMemoriaPrincipal] = EstadosD.Compartido;
            }
        }

        /// <summary>
        /// Se cambia el estado del directorio de Modificado a Compartido.
        /// Esto es cuando se envia un bloqueMemoria a memoria.
        /// </summary>
        /// <param name="solicitante">Solicitante del cambio</param>
        /// <param name="bloqueMemoria">Bloque para obtener el id</param>
        public void CompartirBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            Debug.Assert(usuarios[bloque.IndiceMemoriaPrincipal].Count == 1);
            Esperar(solicitante);
            Estados[bloque.IndiceMemoriaPrincipal] = EstadosD.Compartido;
        }

        public void ModificarBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            Debug.Assert(usuarios[bloque.IndiceMemoriaPrincipal].Count == 1);
            Esperar(solicitante);
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
            CacheDatos cache = null;
            int indice = bloque.IndiceMemoriaPrincipal;
            if (estados[indice] == EstadosD.Modificado && usuarios[indice].Count == 1) {
                cache = usuarios[indice][0];
            }
            return cache;
        }

        /// <summary>
        /// Método que devuelve los usuarios que comparten un bloqueMemoria
        /// </summary>
        /// <param name="bloqueMemoria">Bloque para obtener el índice de memoria principal</param>
        /// <returns>Lista de Caches que comparten el bloqueMemoria</returns>
        public List<CacheDatos> GetUsuariosQueComparten(CacheDatos solicitante, Bloque bloque) {
            Esperar(solicitante);
            return usuarios[bloque.IndiceMemoriaPrincipal];
        }

        /// <summary>
        /// Se elimina cache de la lista respectiva
        /// </summary>
        /// <param name="solicitante">Cache de datos que ya no es usuaria del bloqueMemoria</param>
        /// <param name="bloqueMemoria">Bloque ya no utilizado</param>
        public void EliminarUsuarioBloque(CacheDatos solicitante, BloqueCacheDatos bloque) {
            Esperar(solicitante);
            usuarios[bloque.IndiceMemoriaPrincipal].Remove(solicitante);
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
            Debug.Assert(estados[bloque.IndiceMemoriaPrincipal] == EstadosD.Compartido);

            //Obtengo quienes son las caches que tienen bloqueN compartido
            List<CacheDatos> lectores = usuarios[bloque.IndiceMemoriaPrincipal];

            // Cuenta la cantidad de veces que se falla con una cache
            int intentosFallidos = 0;

            // Mientras no se falle consecutivamente con todas las caches de la lista
            while (intentosFallidos != lectores.Count) {

                foreach (CacheDatos cache in new List<CacheDatos>(lectores)) {

                    if (cache.ID != solicitante.ID) {

                        // Se intenta invalidar cada una
                        try {

                            // Bloque la cache
                            cache.Bloquear();

                            BloqueCacheDatos bloqueCache = cache[bloque.IndiceCache];

                            // Invalido el bloque
                            bloqueCache.Estado = EstadosB.Invalido;

                            // Quito del directorio
                            EliminarUsuarioBloque(cache, bloqueCache);

                            // Debloqueo la cache
                            cache.Desbloquear();

                        } catch (RebootNeededException) {
                            intentosFallidos++;
                        }
                    }
                }
            }

            // Si se falló con todas las caches, se reinicia la instrucción.
            if (intentosFallidos == lectores.Count) {
                throw new RebootNeededException();
            }

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
