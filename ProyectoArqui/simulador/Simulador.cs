using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoArqui.simulador
{
    class Simulador
    {

        public void ejecutarSimulacion()
        {
            
            CacheInstrucciones cacheInst = new CacheInstrucciones(); // Debe venir de afuera
            Procesador procesador = new Procesador(cacheInst);
            Controlador controlador = new Controlador(1, new Procesador[]{procesador});

            procesador.setControlador(controlador);

            // CrearHilo
            Thread procesadorHilo = new Thread(procesador.procesar);
            procesadorHilo.Start();

            // Esperar a que termine
            procesadorHilo.Join();
        }

    }
}
