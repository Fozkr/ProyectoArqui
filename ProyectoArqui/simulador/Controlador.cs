using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class Controlador
    {

        Barrier barrier;
        Procesador[] procesadores;

        public Controlador(int numeroProcesadores, Procesador[] procesadores)
        {
            this.barrier = new Barrier(numeroProcesadores, entreCiclosDeReloj);
            this.procesadores = procesadores;
        }

        public void esperar(int ticksDeReloj)
        {
            for (int i = 0; i < ticksDeReloj; ++i)
            {
                barrier.SignalAndWait();
            }
        }

        public void entreCiclosDeReloj(Barrier b)
        {
            // Este metodo se ejecuta entre ciclos de reloj
            // Aqui se pueden procesar mensajes de caches, etc
        }


    }
}
