using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    /// <summary>
    /// Representa un procesador simulado. Ejecuta instrucciones hasta que llega a una instruccion fin.
    /// </summary>
    class Procesador
    {

        private bool finalizado = false;

        private Controlador controlador;

        private int programCounter;
        private int[] registros =  new int[32];
        private CacheDatos cache;
        private CacheInstrucciones cacheInst;

        // FIXME Usar un mapa <int, inst>
        private delegate void inst(Instruccion i);
        private int[] codigos = {8, 32, 34, 35, 43, 4, 5, 63};
        private inst[] metodos = new inst[8];

        public Procesador(CacheInstrucciones cacheInst)
        {
            this.metodos[0] = daddi;
            this.metodos[1] = dadd;
            this.metodos[2] = dsub;
            this.metodos[3] = lw;
            this.metodos[4] = sw;
            this.metodos[5] = beqz;
            this.metodos[6] = bnez;
            this.metodos[7] = fin;

            this.programCounter = 0;
            this.cache = new CacheDatos(new MemoriaPrincipal(), controlador);
            this.cacheInst = cacheInst;

        }

        public void setProgramCounter(int programCounter)
        {
            this.programCounter = programCounter;
        }

        public void setControlador(Controlador controlador)
        {
            this.controlador = controlador;
        }

        public void procesar()
        {
            while (!finalizado)
            {
                Instruccion i = cacheInst.obtenerInstruccion(programCounter);
                procesarInstruccion(i);
            }
        }


        /// <summary>
        /// Metodo que procesa una instruccion.
        /// Se encarga de hacer la decodificacion de la instruccion y llamar al metodo de procesamiento correcto.
        /// </summary>
        /// <param name="i">Instruccion cuyo codigo se decodifica</param>
        private void procesarInstruccion(Instruccion inst)
        {
            // 
            bool ejecutado = false;
            for (int i = 0; i < codigos.Length && !ejecutado; ++i)
            {
                if (codigos[i] == inst.GetCodigo())
                {
                    metodos[i](inst);
                    ejecutado = true;
                }
            }
            programCounter += 4;
            controlador.esperar(1);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de daddi
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void daddi(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            registros[x] = registros[y] + n;
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de dadd
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void dadd(Instruccion i)
        {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            registros[x] = registros[y] + registros[z];
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de dsub
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void dsub(Instruccion i)
        {
            int y = i.GetParametro(1), z = i.GetParametro(2), x = i.GetParametro(3);
            registros[x] = registros[y] - registros[z];
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de lw
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param
        private void lw(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            registros[x] = cache.Leer(n + registros[y]);
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de sw
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void sw(Instruccion i)
        {
            int y = i.GetParametro(1), x = i.GetParametro(2), n = i.GetParametro(3);
            cache.Escribir(n + registros[y], registros[x]);
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
                // Despues el metodo que llama a este aumenta en 4 el pc
                programCounter = n - 4;
            }
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
                // Despues el metodo que llama a este aumenta en 4 el pc
                programCounter = n - 4;
            }
        }

        /// <summary>
        /// Metodo que ejecuta la instruccion de fin
        /// </summary>
        /// <param name="i">Instruccion de la cual se extraen los parametros necesarios</param>
        private void fin(Instruccion i)
        {
            finalizado = true;
        }

    }
}
