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
        private List<Bloqueable> bloqueados = new List<Bloqueable>();

        // Para la interfaz
        private int[] palabrasArray = new int[PalabrasPorCache];
        private int[] direccionesArray = new int[BloquesPorCache];
        private char[] estadosArray = new char[BloquesPorCache];

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
                Debug.Assert(EstaBloqueado());
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
                for (int i = 0; i < BloquesPorCache; i++) {
                    direccionesArray[i] = bloques[i].Direccion;
                }
                return direccionesArray;
            }
        }

        /// <summary>
        /// Se convierten los estados en un vector de chars para 
        /// mantener compatibilidad con la interfaz
        /// </summary>
        public char[] EstadosArray {
            get {
                for (int i = 0; i < BloquesPorCache; i++) {
                    switch (bloques[i].Estado) {
                    case EstadosB.Invalido:
                    estadosArray[i] = 'I';
                    break;
                    case EstadosB.Compartido:
                    estadosArray[i] = 'C';
                    break;
                    case EstadosB.Modificado:
                    estadosArray[i] = 'M';
                    break;
                    }
                }
                return estadosArray;
            }
        }

        /// <summary>
        /// Propiedad para acceder a la estructura interna de la solicitante de datos.
        /// Devuelve una copia.
        /// </summary>
        public int[] Array {
            get {
                int k = 0;
                foreach (Bloque bloque in bloques) {
                    foreach (int palabra in bloque.Array) {
                        palabrasArray[k++] = palabra;
                    }
                }
                return palabrasArray;
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
                    DesbloquearTodo();
                    Debug.WriteLine("Cache " + id + ": Reiniciando SW");
                    // Reintentando proximo ciclo
                    controlador.Esperar(1);
                }
            }
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
                    DesbloquearTodo();
                    Debug.WriteLine("Cache " + id + ": Reiniciando LW");
                    // Reintentando proximo ciclo
                    controlador.Esperar(1);
                }
            }
            return palabra;
        }

        /// <summary>
        /// Método que escribe una palabra en una dirección de memoria
        /// </summary>
        /// <param name="info">Guarda información de donde debe escribirse la palabra</param>
        /// <param name="palabra">Palabra que se quiere escribir</param>
        public void Escribir(InformacionPalabra info, int palabra) {

            //Se bloquea mi cache
            this.Bloquear(this.Nombre);
            bloqueados.Add(this);

            // Si es hit
            if (info.EsHit()) {

                BloqueCacheDatos bloqueN = this[info.IndiceCache];

                if (bloqueN.Estado == EstadosB.Modificado) {

                    // Si está en mi cache modificado nada más lo escribo
                    bloqueN[info.IndicePalabra] = palabra;
                    bloqueN.Estado = EstadosB.Modificado;

                } else {

                    // Si lo tengo compartido, bloqueo el directorio correspondiente
                    info.Directorio.Bloquear(this.Nombre);
                    bloqueados.Add(info.Directorio);

                    // Invalido a todos quienes compartan el bloque
                    info.Directorio.InvalidarBloque(this, info.Bloque);

                    // Escribo la palabra
                    bloqueN[info.IndicePalabra] = palabra;
                    bloqueN.Estado = EstadosB.Modificado;
                    info.Directorio.ModificarBloque(this, bloqueN);

                    // Desbloqueo el directorio
                    info.Directorio.Desbloquear(this.Nombre);
                    bloqueados.Remove(info.Directorio);
                }

            } else {

                // Se envia el bloqueV a memoria si está modificado
                EnviarAMemoriaBloqueVSiModificado(this[info.IndiceCache]);

                // Bloqueo el directorio que contiene la palabra que busco
                info.Directorio.Bloquear(this.Nombre);
                bloqueados.Add(info.Directorio);

                // Consulto el directorio del bloqueMemoria que contiene la palabra que busco
                // para ver si alguna cache lo tiene modificado
                CacheDatos modificante = info.Directorio.GetUsuarioQueModifica(this, info.Bloque);

                if (modificante == null) {  //Si no hay nadie que esté modificando en bloque

                    if (info.Directorio.CountUsuariosQueComparten(this, info.Bloque) > 0) {

                        //Se les invalida el BloqueN a todas las caches que lo tengan compartido
                        info.Directorio.InvalidarBloque(this, info.Bloque);

                    }

                    // Traigo el bloqueMemoria de memoria
                    // El constructor pone el bloque en la cache
                    BloqueCacheDatos.TraerBloqueCacheDatos(info.Bloque, controlador, this, true);

                } else {

                    // Bloqueo la cache que modificó el dato
                    modificante.Bloquear(this.Nombre);
                    bloqueados.Add(modificante);

                    // Se envía el bloque en la cache modificante a memoria
                    modificante[info.IndiceCache].EnviarAMemoria();

                    // Se actualiza la información del bloque
                    info.ActualizarBloque();
                    
                    // Traigo el dato desde la cache modificante
                    BloqueCacheDatos.TraerBloqueCacheDatos(info.Bloque, controlador, this, false);

                    // Desbloqueo la cache que modificó el dato
                    modificante.Desbloquear(this.Nombre);
                    bloqueados.Remove(modificante);

                }

                // Escribo la palabra
                this[info.IndiceCache][info.IndicePalabra] = palabra;
                this[info.IndiceCache].Estado = EstadosB.Modificado;
                this[info.IndiceCache].Directorio.ModificarBloque(this, this[info.IndiceCache]);

                // Desbloqueo el directorio
                info.Directorio.Desbloquear(this.Nombre);
                bloqueados.Remove(info.Directorio);

            }

            // Desbloqueo mi cache
            this.Desbloquear(this.Nombre);
            bloqueados.Remove(this);
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
            this.Bloquear(this.Nombre);
            bloqueados.Add(this);

            // Se pregunta si es Hit
            if (!info.EsHit()) {

                // Se envia el bloqueV a memoria si está modificado
                EnviarAMemoriaBloqueVSiModificado(this[info.IndiceCache]);

                // Bloqueo el directorio que contiene la palabra que busco
                info.Directorio.Bloquear(this.Nombre);
                bloqueados.Add(info.Directorio);

                // Consulto el directorio del bloqueMemoria que contiene la palabra que busco
                // para ver si alguna cache lo tiene modificado
                CacheDatos modificante = info.Directorio.GetUsuarioQueModifica(this, info.Bloque);

                if (modificante == null) {

                    // Traigo el bloqueMemoria de memoria
                    BloqueCacheDatos.TraerBloqueCacheDatos(info.Bloque, controlador, this, true);

                } else {

                    // Bloqueo la cache que modificó el dato
                    modificante.Bloquear(this.Nombre);
                    bloqueados.Add(modificante);

                    // Se envía el bloque en la cache modificante a memoria
                    modificante[info.IndiceCache].EnviarAMemoria();

                    // Se actualiza la información del bloque con lo último de memoria
                    info.ActualizarBloque();

                    // Traigo el dato de la cache modificante
                    BloqueCacheDatos.TraerBloqueCacheDatos(info.Bloque, controlador, this, false);

                    // Desbloqueo la cache que modificó el dato
                    modificante.Desbloquear(this.Nombre);
                    bloqueados.Remove(modificante);
                }

                // Desbloqueo el directorio
                info.Directorio.Desbloquear(this.Nombre);
                bloqueados.Remove(info.Directorio);
            }

            // Leo la palabra
            palabraLeida = this[info.IndiceCache][info.IndicePalabra];

            // Desbloqueo la cache
            this.Desbloquear(this.Nombre);
            bloqueados.Remove(this);

            // Devuelvo la palabra leída
            return palabraLeida;
        }

        private void EnviarAMemoriaBloqueVSiModificado(BloqueCacheDatos bloqueV) {

            // Se pregunta si el bloque a reemplazar en mi cache está modificado
            if (bloqueV.Estado == EstadosB.Modificado) {

                Directorio directorioBloqueV = bloqueV.Directorio;

                // Bloqueo directorio de BloqueV
                directorioBloqueV.Bloquear(this.Nombre);
                bloqueados.Add(directorioBloqueV);

                // Envio el bloque a memoria
                // Este método modifica tanto la cache como el directorio (U en directorio e I en Cache)
                bloqueV.EnviarAMemoria();

                // Desbloqueo el directorio del BloqueV
                directorioBloqueV.Desbloquear(this.Nombre);
                bloqueados.Remove(directorioBloqueV);
            }
        }

        public void DesbloquearTodo() {
            Debug.WriteLine("Cache " + id + ": Llamado a desbloquear todo");
            foreach (Bloqueable bloqueado in bloqueados) {
                bloqueado.Desbloquear(this.Nombre);
            }
            bloqueados.Clear();
        }

    }
}
