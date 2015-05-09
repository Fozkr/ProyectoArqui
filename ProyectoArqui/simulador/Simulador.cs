using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProyectoArqui.simulador
{
    /*
     * 
     */
    class Simulador
    {
        //Atributos
        private short cantidadProgramas;
        private FormPrincipal interfaz;
        private String[] nombresProgramas;

        /*
         * Constructor, inicializa el atributo.
         */
        public Simulador(FormPrincipal interfaz)
        {
            cantidadProgramas = 0;
            this.interfaz = interfaz;
        }

        /*
         * 
         */
        public void ejecutarSimulacion(List<int> instrucciones, List<int> iniciosProgramas, List<string> nombresProgramasRecibidos)
        {
            Debug.WriteLine("Simulador: Iniciando...");
            Debug.Flush();

            nombresProgramas = new String[nombresProgramasRecibidos.Count];
            for (short i = 0; i < nombresProgramasRecibidos.Count; ++i)
                nombresProgramas[i] = nombresProgramasRecibidos[i];

            CacheInstrucciones cacheInstrucciones = new CacheInstrucciones(instrucciones, iniciosProgramas, cantidadProgramas, nombresProgramas);

            Procesador procesador0 = new Procesador(cacheInstrucciones, 0);
            //Procesador procesador1 = new Procesador(cacheInstrucciones, 1);
            //Procesador procesador2 = new Procesador(cacheInstrucciones, 2);

            Controlador controlador = new Controlador(1, new Procesador[] { procesador0 }, cacheInstrucciones);
            //Controlador controlador = new Controlador(3, new Procesador[] { procesador0, procesador1, procesador2 }, cacheInstrucciones);

            procesador0.SetControlador(controlador);
            //procesador1.SetControlador(controlador);
            //procesador2.SetControlador(controlador);

            // CrearHilo
            Thread procesador0Hilo = new Thread(new ParameterizedThreadStart(procesador0.procesar));
            //Thread procesador1Hilo = new Thread(procesador1.procesar);
            //Thread procesador2Hilo = new Thread(procesador2.procesar);
            procesador0Hilo.Start(interfaz);
            //procesador1Hilo.Start();
            //procesador2Hilo.Start();

            while (!procesador0.Finalizado)
            {
                interfaz.actualizarNombrePrograma("corriendo " + procesador0.NombrePrograma + "...", 0);
            }

            // Esperar a que termine
            procesador0Hilo.Join();
            //procesador1Hilo.Join();
            //procesador2Hilo.Join();
        }

        /*
         * Setter y getter para el atributo cantidadProgramas.
         */
        public short CantidadProgramas
        {
            get { return this.cantidadProgramas; }
            set { this.cantidadProgramas = value; }
        }

        /*
         * Setter y getter para el atributo interfaz, usado por la clase Procesador.
         */
        public FormPrincipal Interfaz
        {
            get { return this.interfaz; }
            set { this.interfaz = value; }
        }
    }
}
