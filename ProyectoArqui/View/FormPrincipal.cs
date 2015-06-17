using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoArqui.Controller;
using ProyectoArqui.Model;

namespace ProyectoArqui.View {

    /// <summary>
    /// Clase interfaz, obtiene los parámetros del usuario y crea la instancia del hilo maestro para iniciar el programa.
    /// </summary>
    public partial class FormPrincipal : Form, Listener {

        //Atributos
        private int[] programasCorridos;
        private int cantidadProcesadores;           //Usado para saber cuándo actualizar cuáles delos 3 grids
        private List<string> pathsArchivos;

        // Estos delegates se ocupan para poder hacer ThreadSafe llamados a la interfaz
        private delegate void onTickChangedCallback(int newTick);
        private delegate void onProgramChangedCallback(int idProcesador, String nombrePrograma, int ticksReloj, int[] registros, int[] cache);
        private delegate void onProgramCounterChangedCallback(int newPc, int idProcesador);
        private delegate void onRegistersChangedCallback(int[] nuevosRegistros, int idProcesador);
        private delegate void onCacheChangedCallback(int[] palabrasCache, int[] numerosBloquesCache, char[] estadosBloquesCache, int idProcesador);
        private delegate void onMemoryChangedCallback(int[] palabrasMemoria, int idProcesador);
        private delegate void onProgramEndedCallback(string nombrePrograma, int[] registrosFinales, int idProcesador);
        private delegate void onSimulationFinishedCallback();

        /*
         * Constructor, inicializa la instancia del hilo maestro.
         */
        public FormPrincipal() {
            InitializeComponent();

            // Iniciar ventana maximizada
            this.WindowState = FormWindowState.Maximized;

            // Se inicializan atributos
            this.programasCorridos = new int[3];
            this.pathsArchivos = new List<string>();

            for (int i = 0; i < programasCorridos.Length; i++) {
                programasCorridos[i] = -1;
            }

#if DEBUG

            // Esto solo se ejecuta en modo Debug, En Release no se agrega

            String dir = Application.ExecutablePath + "/../../..";
            String[] paths = { "/Hilos/1.txt", "/Hilos/2.txt", "/Hilos/3.txt", "/Hilos/4.txt", "/Hilos/5.txt" };

            foreach (String path in paths) {
                GridPaths.Rows.Add(dir + path);
                pathsArchivos.Add(dir + path);
            }

            BotonNuevaSimulacion.Enabled = false;
            BotonIniciarSimulacion.Enabled = true;

            BotonIniciarSimulacion_Click(null, null);
#endif
        }

        /*
         * Limpia y habilita los campos para comenzar una nueva simulación.
         */
        private void BotonNuevaSimulacion_Click(object sender, EventArgs e) {
            GridPaths.Rows.Clear();
            gridProcesador0.Rows.Clear();
            gridProcesador1.Rows.Clear();
            gridProcesador2.Rows.Clear();
            gridMemoriaCompartida.Rows.Clear();
            BotonAgregarArchivo.Enabled = true;
        }

        /*
         * Abre el FileChooser para poder escoger un archivo que será agregado al grid.
         */
        private void BotonAgregarArchivo_Click(object sender, EventArgs e) {
            FileChooser.Reset();
            FileChooser.ShowDialog();
        }

        /*
         * Al escogerse un archivo, se agrega el path del mismo al grid. También, cuando haya escogido la misma cantidad de archivos
         * que especificó en el textbox, se habilita el botón de iniciar la simulación.
         */
        private void FileChooser_FileOk(object sender, CancelEventArgs e) {
            String pathNuevoArchivo = FileChooser.FileName;
            GridPaths.Rows.Add(pathNuevoArchivo);
            pathsArchivos.Add(pathNuevoArchivo);
            if (GridPaths.Rows.Count > 0) {
                BotonIniciarSimulacion.Enabled = true;
            }
        }

        /*
         * Lee desde los archivos todas las instrucciones para luego enviarlas al simulador.
         */
        private void BotonIniciarSimulacion_Click(object sender, EventArgs e) {

            //Deshabilitar botones para no entorpecer la interfaz durante la simulación (TODO agregar botón para detenerla)
            BotonNuevaSimulacion.Enabled = false;
            BotonAgregarArchivo.Enabled = false;
            BotonIniciarSimulacion.Enabled = false;
            panelProcesadores.Visible = true; //también hace este panel visible para ver los programas correr
            panelMemoria.Visible = true; //también hace este panel visible para ver los programas correr

            //Lee todas las instrucciones y las guarda en una lista
            List<int> instrucciones = new List<int>();          //arreglo general que almacenará todas las instrucciones leídas
            List<int> iniciosProgramas = new List<int>();       //arreglo pequeño que almacena los índices en el anterior donde inicia cada programa
            List<string> nombresProgramas = new List<string>();
            String instruccionIndividual = "";  //usada para iterar por las líneas de los archivos
            foreach (String path in pathsArchivos) {
                nombresProgramas.Add(path.Substring(path.LastIndexOf('/') + 1));
                iniciosProgramas.Add(instrucciones.Count);
                System.IO.TextReader lector = System.IO.File.OpenText(path); //abre el archivo para Leer sus líneas una por una
                while ((instruccionIndividual = lector.ReadLine()) != null) {
                    string[] partes = System.Text.RegularExpressions.Regex.Split(instruccionIndividual.Trim(), @"\s+");
                    if (partes.Length == 4) {
                        for (short i = 0; i < 4; ++i) {
                            instrucciones.Add(int.Parse(partes[i])); //agrega cada número entero al arreglo
                        }
                    }
                }
                lector.Close();
                cantidadProcesadores = (iniciosProgramas.Count > 2 ? 3 : iniciosProgramas.Count); //para no accesar grids inexistentes
            }

            //Enviar parámetros al simulador e iniciar la simulación
            Simulador simulador = new Simulador(instrucciones, iniciosProgramas, nombresProgramas, this);
            Thread hiloSimulacion = new Thread(simulador.EjecutarSimulacion);
            hiloSimulacion.Start();

            crearTuplasMemoriaCompartida(); //de una vez iniciarlizar las tuplas de la memoria compartida

            // NO hacer join al hiloSimulacion porque sino se detienen los eventos de la interfaz grafica
        }

        public void onTickChanged(int newTick) {
            if (this.InvokeRequired) {
                onTickChangedCallback callback = new onTickChangedCallback(onTickChanged);
                this.Invoke(callback, new object[] { newTick });
            } else {
                // TODO La simulacion llama a este metodo cada vez que termina un tick de forma que en este metodo se puede actualizar la interfaz
                // Por ahora actualiza los ticks de duración en cada grid (se podría quitar después si es demasiado lento)
                for (short i = 0; i < cantidadProcesadores; ++i) { //por cada grid existente
                    actualizarTuplasResultado(i, newTick, null, null, null, null);
                }
            }
        }

        /*
         * Simplemente actualiza la label que indica el nombre del programa (archivo de programa) que está corriendo
         * en uno de los procesadores. Ahora, como el programa cambió, también deben agregarse tuplas al grid para éste.
         */
        public void onProgramChanged(int idProcesador, String nombrePrograma, int ticksReloj, int[] registros, int[] cache) {
            if (this.InvokeRequired) {
                onProgramChangedCallback callback = new onProgramChangedCallback(onProgramChanged);
                this.Invoke(callback, new object[] { idProcesador, nombrePrograma, ticksReloj, registros, cache });
            } else {
                // Actualizar label y crear tuplas
                switch (idProcesador) {
                case 0:
                labelProcesador0Corriendo.Text = "corriendo " + nombrePrograma + "...";
                break;
                case 1:
                labelProcesador1Corriendo.Text = "corriendo " + nombrePrograma + "...";
                break;
                case 2:
                labelProcesador2Corriendo.Text = "corriendo " + nombrePrograma + "...";
                break;
                }
                crearTuplasResultado(idProcesador, nombrePrograma, ticksReloj, registros, cache);
            }
        }

        public void onProgramCounterChanged(int newPc, int idProcesador) {
            if (this.InvokeRequired) {
                onProgramCounterChangedCallback callback = new onProgramCounterChangedCallback(onProgramCounterChanged);
                this.Invoke(callback, new object[] { newPc, idProcesador });
            } else {
                // Por ahora nada
            }
        }

        public void onRegistersChanged(int[] nuevosRegistros, int idProcesador) {
            if (this.InvokeRequired) {
                onRegistersChangedCallback callback = new onRegistersChangedCallback(onRegistersChanged);
                this.Invoke(callback, new object[] { nuevosRegistros, idProcesador });
            } else {
                // Actualizar los registros
                actualizarTuplasResultado(idProcesador, -1, nuevosRegistros, null, null, null);
            }
        }

        public void onCacheChanged(int[] palabrasCache, int[] numerosBloquesCache, char[] estadosBloquesCaches, int idProcesador) {
            if (this.InvokeRequired) {
                onCacheChangedCallback callback = new onCacheChangedCallback(onCacheChanged);
                this.Invoke(callback, new object[] { palabrasCache, numerosBloquesCache, estadosBloquesCaches, idProcesador });
            } else {
                // Actualizar el cache en la interfaz
                actualizarTuplasResultado(idProcesador, -1, null, palabrasCache, numerosBloquesCache, estadosBloquesCaches);
            }
        }

        public void onMemoryChanged(int[] palabrasMemoria, int idProcesador) {
            if (this.InvokeRequired) {
                onMemoryChangedCallback callback = new onMemoryChangedCallback(onMemoryChanged);
                this.Invoke(callback, new object[] { palabrasMemoria, idProcesador });
            } else {
                // Actualiza la parte de la memoria compartida correspondiente a ese procesador
                actualizarTuplasMemoriaCompartida(idProcesador, palabrasMemoria);
            }
        }

        public void onProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador) {
            if (this.InvokeRequired) {
                onProgramEndedCallback callback = new onProgramEndedCallback(onProgramEnded);
                this.Invoke(callback, new object[] { nombrePrograma, registrosFinales, idProcesador });
            } else {
                programasCorridos[idProcesador]++;
            }
        }

        public void onSimulationFinished() {
            if (this.InvokeRequired) {
                onSimulationFinishedCallback callback = new onSimulationFinishedCallback(onSimulationFinished);
                this.Invoke(callback);
            } else {
                // Habilitar el botón para una nueva simulación y actualizar los labels
                labelProcesador0Corriendo.Text = "terminado";
                labelProcesador1Corriendo.Text = "terminado";
                labelProcesador2Corriendo.Text = "terminado";
                BotonNuevaSimulacion.Enabled = true;
            }
        }

        /*
         * Cada vez que se actualiza la interfaz es un grid a la vez, dado un ID de procesador, se debe identificar cuál es el grid.
         */
        public DataGridView identificarGridProcesador(int idProcesador) {
            DataGridView grid = null;
            switch (idProcesador) {
            case 0:
            grid = gridProcesador0;
            break;
            case 1:
            grid = gridProcesador1;
            break;
            case 2:
            grid = gridProcesador2;
            break;
            }
            return grid;
        }

        /*
         * Cuando un procesador empieza a simular un nuevo programa, se deben crear las tuplas que mostrarán los resultados
         * de esa simulación, con valores iniciales.
         */
        public void crearTuplasResultado(int idProcesador, String nombrePrograma, int ticsRelojInicio, int[] registros, int[] cache) {
            DataGridView grid = identificarGridProcesador(idProcesador);
            grid.Rows.Add(nombrePrograma);
            grid.Rows.Add("Reloj al inicio", ticsRelojInicio);
            grid.Rows.Add("Tics totales", 0);
            for (short i = 0; i < 8; ++i)
                grid.Rows.Add("Registros", "R" + i + ": " + registros[i],
                                            "R" + (i + 8) + ": " + registros[(i + 8)],
                                            "R" + (i + 16) + ": " + registros[(i + 16)],
                                            "R" + (i + 24) + ": " + registros[(i + 24)]);
            if (cache != null) {
                for (short i = 0; i < 4; ++i)
                    grid.Rows.Add("Caché datos", cache[i],
                                                cache[i + 4],
                                                cache[i + 8],
                                                cache[i + 12]);
            } else {
                for (short i = 0; i < 4; ++i)
                    grid.Rows.Add("Caché datos", 0, 0, 0, 0);
            }
            grid.Rows.Add("Números de bloque", -1, -1, -1, -1);     //caché
            grid.Rows.Add("Estados de bloques", "", "", "", "");    //caché
            grid.Rows.Add("-", "-", "-", "-", "-");
        }

        /*
         * Conforme los procesadores simulan los programas, la interfaz va actualizando visualmente el estado
         * de los datos de interés de cada simulación.
         */
        public void actualizarTuplasResultado(int idProcesador, int ticsReloj, int[] registros, int[] palabrasCache, int[] numerosBloquesCaches, char[] estadosBloquesCaches) {
            DataGridView grid = identificarGridProcesador(idProcesador);
            int tuplaInicial = programasCorridos[idProcesador] * 18; //18 tuplas por programa (3 titulo, 8 registros, 6 cache, 1 final)

            if (ticsReloj != -1) {//si llega un valor válido, se actualiza
                grid.Rows[tuplaInicial + 2].Cells[1].Value = ticsReloj;
            }

            if (registros != null) //si llega un valor válido, se actualiza
            {
                for (short i = 0; i < 8; ++i) {
                    grid.Rows[i + 3 + tuplaInicial].Cells[1].Value = "R" + i + ": " + registros[i];
                    grid.Rows[i + 3 + tuplaInicial].Cells[2].Value = "R" + (i + 8) + ": " + registros[(i + 8)];
                    grid.Rows[i + 3 + tuplaInicial].Cells[3].Value = "R" + (i + 16) + ": " + registros[(i + 16)];
                    grid.Rows[i + 3 + tuplaInicial].Cells[4].Value = "R" + (i + 24) + ": " + registros[(i + 24)];
                }
            }

            if (palabrasCache != null) //si llega un valor válido, se actualiza
            {
                for (short i = 0; i < 4; ++i) {
                    grid.Rows[i + 11 + tuplaInicial].Cells[1].Value = palabrasCache[i];
                    grid.Rows[i + 11 + tuplaInicial].Cells[2].Value = palabrasCache[i + 4];
                    grid.Rows[i + 11 + tuplaInicial].Cells[3].Value = palabrasCache[i + 8];
                    grid.Rows[i + 11 + tuplaInicial].Cells[4].Value = palabrasCache[i + 12];
                }
                //actualizar los números de bloques
                grid.Rows[15].Cells[1].Value = numerosBloquesCaches[0];
                grid.Rows[15].Cells[2].Value = numerosBloquesCaches[1];
                grid.Rows[15].Cells[3].Value = numerosBloquesCaches[2];
                grid.Rows[15].Cells[4].Value = numerosBloquesCaches[3];
                //actualizar los estados de los bloques
                grid.Rows[16].Cells[1].Value = estadosBloquesCaches[0];
                grid.Rows[16].Cells[2].Value = estadosBloquesCaches[1];
                grid.Rows[16].Cells[3].Value = estadosBloquesCaches[2];
                grid.Rows[16].Cells[4].Value = estadosBloquesCaches[3];
            }
        }

        /*
         * 
         */
        private void crearTuplasMemoriaCompartida() {
            for (short i = 0; i < 12; ++i) //2 bloques por tupla, son 24 bloques en total, 4 tuplas por procesador
                gridMemoriaCompartida.Rows.Add("Procesador " + (i / 4), 0, 0, 0, 0, 0, 0, 0, 0);
        }

        /*
         * 
         */
        private void actualizarTuplasMemoriaCompartida(int idProcesador, int[] palabrasMemoria) {
            int tuplaInicial = idProcesador * 4;
            int palabra = 0;
            for (short i = 0; i < 4; ++i) {
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[1].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[2].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[3].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[4].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[5].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[6].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[7].Value = palabrasMemoria[palabra++];
                gridMemoriaCompartida.Rows[i + tuplaInicial].Cells[8].Value = palabrasMemoria[palabra++];
            }
        }
    }

}
