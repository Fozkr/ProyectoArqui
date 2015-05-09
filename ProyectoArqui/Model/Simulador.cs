using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using ProyectoArqui.Controller;

namespace ProyectoArqui.Model
{
    /*
     * 
     */
    class Simulador
    {
        //Atributos
        private int cantidadProgramas;
        private List<int> instrucciones;
        private List<int> iniciosProgramas; 
        private Listener interfaz;
        private String[] nombresProgramas;

        /*
         * Constructor, inicializa el atributo.
         */
        public Simulador(List<int> instrucciones, List<int> iniciosProgramas, List<string> nombresProgramasRecibidos, Listener interfaz)
        {
            this.cantidadProgramas = iniciosProgramas.Count;
            this.instrucciones = instrucciones;
            this.iniciosProgramas = iniciosProgramas;
            this.nombresProgramas = nombresProgramasRecibidos.ToArray();
            this.interfaz = interfaz;
        }

        /*
         * 
         */
        public void ejecutarSimulacion()
        {
            Debug.WriteLine("Simulador: Iniciando...");
            Debug.Flush();

            // Modificar aqui la cantidad de procesadores deseados!
            int numeroProcesadores = 1;

            CacheInstrucciones cacheInstrucciones = new CacheInstrucciones(instrucciones, iniciosProgramas, cantidadProgramas, nombresProgramas);

            Procesador[] procesadores = new Procesador[numeroProcesadores];

            for (int i = 0; i < numeroProcesadores; ++i)
            {
                procesadores[i] = new Procesador(cacheInstrucciones, i);
            }

            Controlador controlador = new Controlador(numeroProcesadores, procesadores, cacheInstrucciones);
            controlador.AddListener(interfaz);

            for (int i = 0; i < numeroProcesadores; ++i)
            {
                procesadores[i].SetControlador(controlador);
            }

            Thread[] hilosProcesadores = new Thread[3];
            for (int i = 0; i < numeroProcesadores; ++i)
            {
                hilosProcesadores[i] = new Thread(procesadores[i].procesar);
                hilosProcesadores[i].Start();
            }

            // Cuando todos los procesadores comienzan se empiezan a sincronizar solos con ayuda del objeto controlador
            // porque ahi esta la barrera

            for (int i = 0; i < numeroProcesadores; ++i)
            {
                hilosProcesadores[i].Join();
            }
        }

        private int[] descomponerCache(CacheDatos cache)
        {
            int[] descomposicion = new int[4 * 4]; //4 bloques, 4 palabras, 4 bytes
            Bloque[] bloques = cache.BloquesDeCache;
            short byteActual = 0;
            for(short i=0; i<4; ++i) //4 bloques
            {
                int[] palabras = bloques[i].PalabrasDelBloque;
                for (short k = 0; k < 4; ++k) //4  palabras
                    descomposicion[byteActual++] = palabras[k];
            }
            return descomposicion;
        }

        /*
         * Setter y getter para el atributo cantidadProgramas.
         */
        public int CantidadProgramas
        {
            get { return this.cantidadProgramas; }
            set { this.cantidadProgramas = value; }
        }

    }
}
