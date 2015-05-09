using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProyectoArqui.simulador
{
    /// <summary>
    /// Representa un procesador simulado. Ejecuta instrucciones hasta que llega a una instruccion fin.
    /// </summary>
    class Procesador
    {
        //Atributos
        private bool finalizado = false;

        private Controlador controlador;

        private int programCounter;
        private int[] registros = new int[32];
        private CacheDatos cache;
        private CacheInstrucciones cacheInstrucciones;
        private int id;

        // FIXME Usar un mapa <int, inst>
        private delegate void inst(Instruccion i);
        Dictionary<int, inst> mapa = new Dictionary<int, inst>();

        public Procesador(CacheInstrucciones cacheInst, int id)
        {
            mapa.Add(8, daddi);
            mapa.Add(32, dadd);
            mapa.Add(34, dsub);
            mapa.Add(35, lw);
            mapa.Add(43, sw);
            mapa.Add(4, beqz);
            mapa.Add(5, bnez);
            mapa.Add(63, fin);

            this.programCounter = 0;
            this.cacheInstrucciones = cacheInst;
            this.id = id;
        }

        public void SetProgramCounter(int programCounter)
        {
            this.programCounter = programCounter;
        }

        public void SetControlador(Controlador controlador)
        {
            this.controlador = controlador;
            this.cache = new CacheDatos(new MemoriaPrincipal(), controlador);
        }

        public void procesar()
        {
            while (!finalizado)
            {
                Debug.WriteLine("Procesador: PC = " + programCounter);
                Instruccion i = cacheInstrucciones.obtenerInstruccion(programCounter);
                procesarInstruccion(i);
                Debug.Flush();
            }
        }

        public bool Finalizado
        {
            get { return this.finalizado; }
            set { this.finalizado = value; }
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int[] Registros
        {
            get { return this.registros; }
            set { this.registros = value; }
        }

        public CacheDatos Cache
        {
            get { return this.cache; }
            set { this.cache = value; }
        }

        /// <summary>
        /// Metodo que procesa una instruccion.
        /// Se encarga de hacer la decodificacion de la instruccion y llamar al metodo de procesamiento correcto.
        /// </summary>
        /// <param name="i">Instruccion cuyo codigo se decodifica</param>
        private void procesarInstruccion(Instruccion inst)
        {
            mapa[inst.GetCodigo()](inst);
            programCounter += 4;
            Debug.WriteLine("Procesador: Esperando 1 tick luego de ejecutar una instruccion");
            controlador.esperar(1);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de daddi
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void daddi(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            Debug.WriteLine("Procesador: DADDI R" + x + " = R" + y + " + " + n);
            Debug.WriteLine("Procesador: DADDI R" + x + " = " + registros[y] + " + " + n);
            registros[x] = registros[y] + n;
            Debug.WriteLine("Procesador: DADDI R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de dadd
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void dadd(Instruccion i)
        {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            Debug.WriteLine("Procesador: DADD R" + x + " = R" + y + " + R" + z);
            Debug.WriteLine("Procesador: DADD R" + x + " = " + registros[y] + " + " + registros[z]);
            registros[x] = registros[y] + registros[z];
            Debug.WriteLine("Procesador: DADD R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de dsub
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void dsub(Instruccion i)
        {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            Debug.WriteLine("Procesador: DSUB R" + x + " = R" + y + " - R" + z);
            Debug.WriteLine("Procesador: DSUB R" + x + " = " + registros[y] + " - " + registros[z]);
            registros[x] = registros[y] - registros[z];
            Debug.WriteLine("Procesador: DSUB R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de lw
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param
        private void lw(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            registros[x] = cache.Leer(n + registros[y]);
            Debug.WriteLine("Procesador: LW R" + x + " = MEM(" + n + " + R" + y +")");
            Debug.WriteLine("Procesador: LW R" + x + " = MEM(" + (n + registros[y]) + ")");
            Debug.WriteLine("Procesador: LW R" + x + " = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de sw
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void sw(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            cache.Escribir(n + registros[y], registros[x]);
            Debug.WriteLine("Procesador: SW MEM(" + n + " + R" + y + ") = R" + x);
            Debug.WriteLine("Procesador: SW MEM(" + (n + registros[y]) + ") = " + registros[x]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de beqz
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void beqz(Instruccion i)
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
        /// Metodo que ejecuta la instruccion de bnez
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void bnez(Instruccion i)
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
        /// Metodo que ejecuta la instruccion de fin
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void fin(Instruccion i)
        {
            controlador.programaTerminado(id);
            Debug.WriteLine("Procesador: Un programa ha finalizado");
        }
    }
}
