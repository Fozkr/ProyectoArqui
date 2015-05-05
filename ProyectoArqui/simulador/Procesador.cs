using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
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

        public void procesarInstruccion(Instruccion inst)
        {
            bool ejecutado = false;
            for (int i = 0; i < codigos.Length && !ejecutado; ++i)
            {
                if (codigos[i] == inst.getCodigo())
                {
                    metodos[i](inst);
                    ejecutado = true;
                }
            }
            programCounter += 4;
            controlador.esperar(1);
        }

        public void daddi(Instruccion i)
        {
            int y = i.getParametro(1), x = i.getParametro(2), n = i.getParametro(3);
            registros[x] = registros[y] + n;
        }

        public void dadd(Instruccion i)
        {
            int y = i.getParametro(1), z = i.getParametro(2), x = i.getParametro(3);
            registros[x] = registros[y] + registros[z];
        }

        public void dsub(Instruccion i)
        {
            int y = i.getParametro(1), z = i.getParametro(2), x = i.getParametro(3);
            registros[x] = registros[y] - registros[z];
        }

        public void lw(Instruccion i)
        {
            int y = i.getParametro(1), x = i.getParametro(2), n = i.getParametro(3);
            registros[x] = cache.leer(n + registros[y]);
        }

        public void sw(Instruccion i)
        {
            int y = i.getParametro(1), x = i.getParametro(2), n = i.getParametro(3);
            cache.escribir(n + registros[y], registros[x]);
        }

        public void beqz(Instruccion i)
        {
            int x = i.getParametro(1), n = i.getParametro(3);
            if (registros[x] == 0)
            {
                // Despues el metodo que llama a este aumenta en 4 el pc
                programCounter = n - 4;
            }
        }

        public void bnez(Instruccion i)
        {
            int x = i.getParametro(1), n = i.getParametro(3);
            if (registros[x] != 0)
            {
                // Despues el metodo que llama a este aumenta en 4 el pc
                programCounter = n - 4;
            }
        }

        public void fin(Instruccion i)
        {
            finalizado = true;
        }

    }
}
