using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProyectoArqui.simulador
{
    class Simulador
    {

        public void ejecutarSimulacion(List<int> instrucciones, List<int> iniciosProgramas)
        {

            Debug.WriteLine("Simulador: Iniciando...");
            Debug.Flush();

            CacheInstrucciones cacheInstrucciones = new CacheInstrucciones(instrucciones, iniciosProgramas);

            Procesador procesador = new Procesador(cacheInstrucciones, 0);

            Controlador controlador = new Controlador(1, new Procesador[]{procesador}, cacheInstrucciones);

            procesador.SetControlador(controlador);

            // CrearHilo
            Thread procesadorHilo = new Thread(procesador.procesar);
            procesadorHilo.Start();

            // Esperar a que termine
            procesadorHilo.Join();
        }

    }
}
