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

        /// <summary>
        /// Constructor de la simulacion
        /// </summary>
        /// <param name="instrucciones">Lista de instrucciones de todos los programas</param>
        /// <param name="iniciosProgramas">Indica el inicio de cada programa</param>
        /// <param name="nombresProgramasRecibidos">Indica el nombre de cada programa</param>
        /// <param name="interfaz">Vista que va a recibir los datos que el modelo emite</param>
        public Simulador(List<int> instrucciones, List<int> iniciosProgramas, List<string> nombresProgramasRecibidos, Listener interfaz)
        {
            this.cantidadProgramas = iniciosProgramas.Count;
            this.instrucciones = instrucciones;
            this.iniciosProgramas = iniciosProgramas;
            this.nombresProgramas = nombresProgramasRecibidos.ToArray();
            this.interfaz = interfaz;
        }

        /// <summary>
        /// Crea todas las condiciones necesarias para la simulacion y
        /// posteriormente la inicia simplemente diciendole a cada procesador
        /// que se ejecute
        /// 
        /// Este metodo esta pensado en que se va a envolver en un hilo cuando se
        /// cree desde la vista!
        /// 
        /// </summary>
        public void EjecutarSimulacion()
        {
            Debug.WriteLine("Simulador: Iniciando...");
            Debug.Flush();

            // Modificar aqui la cantidad de procesadores deseados!
            int numeroProcesadores = 1;

            // Se crean vectores para todos los objetos necesarios
            Controlador controlador = new Controlador();
            Procesador[] procesadores = new Procesador[numeroProcesadores];
            CacheInstrucciones cacheInstrucciones = new CacheInstrucciones(instrucciones, iniciosProgramas, cantidadProgramas, nombresProgramas);
            CacheDatos[] cachesDatos = new CacheDatos[numeroProcesadores];
            MemoriaPrincipal[] memoriasPrincipales = new MemoriaPrincipal[numeroProcesadores];

            // Se inicializan todos los objetos relacionados a los procesadores
            for (int i = 0; i < numeroProcesadores; ++i)
            {
                memoriasPrincipales[i] = new MemoriaPrincipal();
                cachesDatos[i] = new CacheDatos(memoriasPrincipales[i], controlador);
                procesadores[i] = new Procesador(i, cacheInstrucciones, cachesDatos[i], controlador);
            }

            // Se inicializa el controlador que hasta el momento no conocia a nadie
            controlador.Inicializar(procesadores, cachesDatos, cacheInstrucciones, memoriasPrincipales);
            
            // Se agrega la interfaz como listener del controlador/modelo
            controlador.AddListener(interfaz);

            // Se crean los hilos necesarios
            Thread[] hilosProcesadores = new Thread[numeroProcesadores];
            for (int i = 0; i < numeroProcesadores; ++i)
            {
                // Se crea un hilo para cada procesador y se manda a ejectuar instrucciones
                hilosProcesadores[i] = new Thread(procesadores[i].Procesar);
                hilosProcesadores[i].Start();
            }

            // Cuando todos los procesadores comienzan, se empiezan a sincronizar 
            // solos con ayuda del objeto controlador porque ahi esta la barrera

            // Se espera que cada procesador termine
            for (int i = 0; i < numeroProcesadores; ++i)
            {
                hilosProcesadores[i].Join();
            }

            // El hilo del simulador termina en el momento que todos los procesadores terminan!
        }
    }
}
