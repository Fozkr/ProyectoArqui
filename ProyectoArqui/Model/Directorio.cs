using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Contiene quien está utilizando cada uno de los bloques de memoria local.
    /// </summary>
    class Directorio : Bloqueable {

        private int id;

        // Contiene el estado de cada bloque
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
        /// Modifica el estado de un bloque en específico.
        /// </summary>
        /// <param name="indiceBloqueMemoria">Número de bloque al que le voy a modificar el estado</param>
        /// <param name="estado">Nuevo estado.</param>
        /// <param name="idSolicitante">ID del solicitante</param>
        public void SetEstadoBloque(int indiceBloque, EstadosD estado, int idSolicitante) {
            this.Esperar(idSolicitante);
            estados[indiceBloque] = estado;
        }

        /// <summary>
        /// Obtengo el estado de un bloque en específico.
        /// </summary>
        /// <param name="indiceBloqueMemoria">Número de bloque del que voy a revisar el estado</param>
        /// <param name="idSolicitante">ID del solicitante</param>
        /// <returns>Retorna el estado del bloque correspondiente</returns>
        public EstadosD GetEstadoBloque(int indiceBloque, int idSolicitante) {
            this.Esperar(idSolicitante);
            return estados[indiceBloque];
        }

        /// <summary>
        /// Se agrega directorio a la lista respectiva
        /// </summary>
        /// <param name="indiceBloque">Indice del bloque a utilizar</param>
        /// <param name="cache">Cache que utilizará el bloque</param>
        public void AgregarUsuarioBloque(int indiceBloque, CacheDatos cache) {
            usuarios[indiceBloque].Add(cache);
        }

        /// <summary>
        /// Devuelve los usuarios de un bloque en específico.
        /// </summary>
        /// <param name="indiceBloque">Número del bloque del cual se consultan los usuarios</param>
        /// <returns>Lista de usuarios</returns>
        public List<CacheDatos> GetUsuariosBloque(int indiceBloque) {
            return usuarios[indiceBloque];
        }

        /// <summary>
        /// Se elimina directorio de la lista respectiva
        /// </summary>
        /// <param name="indiceBloqueMemoria">Número de bloque que dejará de usar el directorio respectivo</param>
        /// <param name="directorio">Directorio que dejará de utilizar uno de los bloques</param>
        public void EliminarUsuarioBloque(int indiceBloque, CacheDatos cache) {
            usuarios[indiceBloque].Remove(cache);
        }

        /// <summary>
        /// Cantidad de ciclos a esperar en caso de que se trate de una operación local o remota
        /// </summary>
        /// <param name="idSolicitante">Para saber si es un directorio local o remoto</param>
        private void Esperar(int idSolicitante) {
            if (id == idSolicitante) {
                controlador.Esperar(EsperaOperacionDirectorioLocal);
            } else {
                controlador.Esperar(EsperaOperacionDirectorioRemoto);
            }
        }

        /// <summary>
        /// Se invalida el bloque en todas las cachés que lo usen
        /// </summary>
        /// <param name="indiceBloqueMemoria">Número de bloque a invalidar</param>
        public void InvalidarBloque(int numeroBloque) {
            throw new NotImplementedException();
         }

    }
}
