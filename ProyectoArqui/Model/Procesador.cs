using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model
{
    /// <summary>
    /// Representa un procesador simulado. Ejecuta instrucciones hasta que llega a una instruccion Fin.
    /// 
    /// Esta clase se convierte en un hilo de la simulacion!
    /// 
    /// </summary>
    class Procesador
    {
        //Atributos
        private Controlador controlador;
        private int programCounter;
        private int[] registros = new int[32];
        private CacheDatos cacheDatos;
        private CacheInstrucciones cacheInstrucciones;
        private int idProcesador;
        private bool finalizado;

        // Diccionario que almacena un entero con el metodo que tiene que ejecutar
        private delegate void inst(Instruccion i);
        private Dictionary<int, inst> mapa = new Dictionary<int, inst>();

        public int ProgramCounter
        {
            get { return programCounter; }
            set { programCounter = value; }
        }

        public int ID
        {
            get { return idProcesador; }
        }

        public bool Finalizado
        {
            set { finalizado = value; }
            get { return finalizado; }
        }

        /// <summary>
        /// Crea un procesador con el id indicado y una cacheDatos de instrucciones de la cual extrae 
        /// las instrucciones que ejecutar
        /// </summary>
        /// <param name="idProcesador">Id del procesador</param>
        /// <param name="cacheInstrucciones">Cache de Instrucciones para todos los procesadores</param>
        public Procesador(int idProcesador, CacheInstrucciones cacheInstrucciones, CacheDatos cacheDatos, Controlador controlador)
        {
            mapa.Add(8, Daddi);
            mapa.Add(32, Dadd);
            mapa.Add(34, Dsub);
            mapa.Add(35, Lw);
            mapa.Add(43, Sw);
            mapa.Add(4, Beqz);
            mapa.Add(5, Bnez);
            mapa.Add(63, Fin);

            this.programCounter = 0;
            this.cacheInstrucciones = cacheInstrucciones;
            this.cacheDatos = cacheDatos;
            this.idProcesador = idProcesador;
            this.controlador = controlador;

            // finalizado debe ser true para que el inicializador
            // del controlador le asigne un programa que ejecutar!
            this.finalizado = true;
        }

        /// <summary>
        /// Este es el metodo principal del procesador.
        /// 
        /// Cuando el procesador se envuelva en un hilo, este es el metodo que se ejecuta
        /// 
        /// Basicamente mientras hayan instrucciones que ejecutar el hilo continua ejecutandose
        /// </summary>
        public void Procesar()
        {
            while (!Finalizado)
            {
                Debug.WriteLine("Procesador: PC = " + programCounter);
                Instruccion i = cacheInstrucciones.ObtenerInstruccion(programCounter);
                procesarInstruccion(i);
                Debug.Flush();
            }
        }

        /// <summary>
        /// Metodo que procesa una instruccion.
        /// Se encarga de hacer la decodificacion de la instruccion y llamar al metodo de procesamiento correcto.
        /// </summary>
        /// <param name="i">Instruccion cuyo codigo se decodifica</param>
        private void procesarInstruccion(Instruccion inst)
        {
            mapa[inst.GetCodigo()](inst);
            programCounter += 4; // Se modifica el pc para que la proxima instruccion arranque donde debe
            Debug.WriteLine("Procesador: Esperando 1 tick luego de ejecutar una instruccion");
            controlador.esperar(1);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Daddi
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Daddi(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            Debug.WriteLine("Procesador: DADDI R" + x + " = R" + y + " + " + n);
            Debug.WriteLine("Procesador: DADDI R" + x + " = " + registros[y] + " + " + n);
            registros[x] = registros[y] + n;
            Debug.WriteLine("Procesador: DADDI R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Dadd
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Dadd(Instruccion i)
        {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            Debug.WriteLine("Procesador: DADD R" + x + " = R" + y + " + R" + z);
            Debug.WriteLine("Procesador: DADD R" + x + " = " + registros[y] + " + " + registros[z]);
            registros[x] = registros[y] + registros[z];
            Debug.WriteLine("Procesador: DADD R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Dsub
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Dsub(Instruccion i)
        {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            Debug.WriteLine("Procesador: DSUB R" + x + " = R" + y + " - R" + z);
            Debug.WriteLine("Procesador: DSUB R" + x + " = " + registros[y] + " - " + registros[z]);
            registros[x] = registros[y] - registros[z];
            Debug.WriteLine("Procesador: DSUB R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Lw
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param
        private void Lw(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            registros[x] = cacheDatos.Leer(n + registros[y]);
            Debug.WriteLine("Procesador: LW R" + x + " = MEM(" + n + " + R" + y + ")");
            Debug.WriteLine("Procesador: LW R" + x + " = MEM(" + (n + registros[y]) + ")");
            Debug.WriteLine("Procesador: LW R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Sw
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Sw(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            cacheDatos.Escribir(n + registros[y], registros[x]);
            Debug.WriteLine("Procesador: SW MEM(" + n + " + R" + y + ") = R" + x);
            Debug.WriteLine("Procesador: SW MEM(" + (n + registros[y]) + ") = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Beqz
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Beqz(Instruccion i)
        {
            int x = i.GetParametro(1), n = i.GetParametro(3);
            if (registros[x] == 0)
            {
                programCounter = (programCounter + 4) + n * 4;
                Debug.WriteLine("Procesador: BEQZ Salto a " + programCounter);
                programCounter -= 4;
            }
            else
            {
                Debug.WriteLine("Procesador: BEQZ No salto");
            }
            // Despues el metodo que llama a este aumenta en 4 el pc
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Bnez
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Bnez(Instruccion i)
        {
            int x = i.GetParametro(1), n = i.GetParametro(3);
            if (registros[x] != 0)
            {
                programCounter = (programCounter + 4) + n * 4;
                Debug.WriteLine("Procesador: BNEZ Salto a " + programCounter);
                programCounter -= 4;
            }
            else
            {
                Debug.WriteLine("Procesador: BNEZ No salto");
            }
            // Despues el metodo que llama a este aumenta en 4 el pc
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de Fin
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void Fin(Instruccion i)
        {
            // A pesar de que se asigna true a finalizado cada vez que llega a un final de programa
            // El controlador revisa entre ciclos de reloj si hay un siguiente programa
            // Si lo hay entonces modifica Finalizado a false para que el procesador no deje de hacer su procesamiento
            Finalizado = true;
            Debug.WriteLine("Procesador: Un programa ha finalizado");
        }
    }
}
