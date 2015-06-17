using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model {

    /// <summary>
    /// Representa un procesador simulado. Ejecuta instrucciones hasta que llega a una instruccion Fin.
    /// Esta clase se convierte en un hilo de la simulacion!
    /// </summary>
    class Procesador : Constantes, IModificable {

        private Controlador controlador;
        private int programCounter;
        private int[] registros = new int[32];
        private int id;
        private bool finalizado;
        private bool modificado = true;

        // Para la interfaz
        private int[] registrosArray = new int[32];

        // Diccionario que almacena un entero con el metodo que tiene que ejecutar
        private delegate void inst(Instruccion i);
        private Dictionary<int, inst> mapa = new Dictionary<int, inst>();

        /// <summary>
        /// Crea un procesador y se le indica el controlador donde debe consultar la informacion
        /// </summary>
        /// <param name="controlador">Controlador de la simulación</param>
        public Procesador(Controlador controlador, int id) {
            mapa.Add(8, Daddi);
            mapa.Add(32, Dadd);
            mapa.Add(34, Dsub);
            mapa.Add(35, Lw);
            mapa.Add(43, Sw);
            mapa.Add(4, Beqz);
            mapa.Add(5, Bnez);
            mapa.Add(63, Fin);

            this.id = id;
            this.programCounter = 0;
            this.controlador = controlador;

            // finalizado debe ser true para que el inicializador
            // del controlador le asigne un programa que ejecutar!
            this.finalizado = true;
        }

        /// <summary>
        /// Implementación de la interfaz IModificable.
        /// </summary>
        public bool Modificado {
            get {
                return modificado;
            }
            set {
                modificado = value;
            }
        }

        /// <summary>
        /// Para acceder al PC
        /// </summary>
        public int ProgramCounter {
            get {
                return programCounter;
            }
            set {
                programCounter = value;
            }
        }

        /// <summary>
        /// Id del procesador
        /// </summary>
        public int ID {
            get {
                return id;
            }
        }

        /// <summary>
        /// Indica si este procesador ya terminó o no
        /// </summary>
        public bool Finalizado {
            set {
                finalizado = value;
            }
            get {
                return finalizado;
            }
        }

        /// <summary>
        /// Devuelve una copia de los registros del procesador
        /// </summary>
        /// <returns>Una copia de los registros</returns>
        public int[] GetRegistros() {
            for (int i = 0; i < 32; ++i) {
                registrosArray[i] = registros[i];
            }
            return registrosArray;
        }

        /// <summary>
        /// Cuando un programa termina y se carga otro
        /// Los registros deben ponerse en 0
        /// </summary>
        public void ResetearRegistros() {
            for (int i = 0; i < 32; ++i) {
                registros[i] = 0;
            }
        }

        /// <summary>
        /// Este es el metodo principal del procesador.
        /// 
        /// Cuando el procesador se envuelva en un hilo, este es el metodo que se ejecuta
        /// 
        /// Basicamente mientras hayan instrucciones que ejecutar el hilo continua ejecutandose
        /// </summary>
        public void Procesar() {
            try {

                while (!Finalizado) {
                    Debug.WriteLine("Procesador " + id + ": PC = " + programCounter);
                    Instruccion i = controlador.CacheInstrucciones.ObtenerInstruccion(programCounter);
                    procesarInstruccion(i);
                    Debug.Flush();
                }
                controlador.OnProcesadorTerminado();

            } catch (Exception) {
#if !DEBUG
                MessageBox.Show("Ocurrió un error :/\nAsí quedó la simulación.");
#endif
            }
        }

        /// <summary>
        /// Metodo que procesa una instruccion.
        /// Se encarga de hacer la decodificacion de la instruccion y llamar al metodo de procesamiento correcto.
        /// </summary>
        /// <param name="palabra">Instruccion cuyo codigo se decodifica</param>
        private void procesarInstruccion(Instruccion inst) {
            mapa[inst.GetCodigo()](inst);
            programCounter += 4; // Se modifica el pc para que la proxima instruccion arranque donde debe
            Debug.WriteLine("Procesador " + id + ": Esperando 1 tick luego de ejecutar una instruccion");
            controlador.Esperar(1);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Daddi
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Daddi(Instruccion i) {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            Debug.WriteLine("Procesador " + id + ": DADDI R" + x + " = R" + y + " + " + n);
            Debug.WriteLine("Procesador " + id + ": DADDI R" + x + " = " + registros[y] + " + " + n);
            registros[x] = registros[y] + n;
            this.Modificado = true; // Indica que hubo un cambio en un registro
            Debug.WriteLine("Procesador " + id + ": DADDI R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Dadd
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Dadd(Instruccion i) {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            Debug.WriteLine("Procesador " + id + ": DADD R" + x + " = R" + y + " + R" + z);
            Debug.WriteLine("Procesador " + id + ": DADD R" + x + " = " + registros[y] + " + " + registros[z]);
            registros[x] = registros[y] + registros[z];
            this.Modificado = true; // Indica que hubo un cambio en un registro
            Debug.WriteLine("Procesador " + id + ": DADD R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Dsub
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Dsub(Instruccion i) {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            Debug.WriteLine("Procesador " + id + ": DSUB R" + x + " = R" + y + " - R" + z);
            Debug.WriteLine("Procesador " + id + ": DSUB R" + x + " = " + registros[y] + " - " + registros[z]);
            registros[x] = registros[y] - registros[z];
            this.Modificado = true; // Indica que hubo un cambio en un registro
            Debug.WriteLine("Procesador " + id + ": DSUB R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Lw
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param
        private void Lw(Instruccion i) {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            registros[x] = controlador.CachesDatos[id].Leer(n + registros[y]);
            this.Modificado = true; // Indica que hubo un cambio en un registro
            Debug.WriteLine("Procesador " + id + ": LW R" + x + " = MEM(" + n + " + R" + y + ")");
            Debug.WriteLine("Procesador " + id + ": LW R" + x + " = MEM(" + (n + registros[y]) + ")");
            Debug.WriteLine("Procesador " + id + ": LW R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Sw
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Sw(Instruccion i) {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            controlador.CachesDatos[id].Escribir(n + registros[y], registros[x]);
            Debug.WriteLine("Procesador " + id + ": SW MEM(" + n + " + R" + y + ") = R" + x);
            Debug.WriteLine("Procesador " + id + ": SW MEM(" + (n + registros[y]) + ") = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Beqz
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Beqz(Instruccion i) {
            int x = i.GetParametro(1), n = i.GetParametro(3);
            if (registros[x] == 0) {
                programCounter = (programCounter + 4) + n * 4;
                Debug.WriteLine("Procesador " + id + ": BEQZ Salto a " + programCounter);
                programCounter -= 4;
            } else {
                Debug.WriteLine("Procesador " + id + ": BEQZ No salto");
            }
            // Despues el metodo que llama a este aumenta en 4 el pc
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Bnez
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Bnez(Instruccion i) {
            int x = i.GetParametro(1), n = i.GetParametro(3);
            if (registros[x] != 0) {
                programCounter = (programCounter + 4) + n * 4;
                Debug.WriteLine("Procesador " + id + ": BNEZ Salto a " + programCounter);
                programCounter -= 4;
            } else {
                Debug.WriteLine("Procesador " + id + ": BNEZ No salto");
            }
            // Despues el metodo que llama a este aumenta en 4 el pc
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Fin
        /// </summary>
        /// <param name="palabra">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Fin(Instruccion i) {
            // A pesar de que se asigna true a finalizado cada vez que llega a un final de programa
            // El controlador revisa entre ciclos de reloj si hay un siguiente programa
            // Si lo hay entonces modifica Finalizado a false para que el procesador no deje de hacer su procesamiento
            Finalizado = true;
            Debug.WriteLine("Procesador " + id + ": Un programa ha finalizado");
        }

    }
}
