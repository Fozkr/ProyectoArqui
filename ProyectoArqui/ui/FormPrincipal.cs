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
using ProyectoArqui.simulador;

namespace ProyectoArqui.ui
{
    /*
     * Clase interfaz, obtiene los parámetros del usuario y crea la instancia del hilo maestro para iniciar el programa.
     */
    public partial class FormPrincipal : Form, Listener
    {
        //Atributos
        short[] cantidadProgramasPorGrid;

        /*
         * Constructor, inicializa la instancia del hilo maestro.
         */
        public FormPrincipal()
        {
            InitializeComponent();
            cantidadProgramasPorGrid = new short[3];
        }

        /*
         * Limpia y habilita los campos para comenzar una nueva simulación.
         */
        private void BotonNuevaSimulacion_Click(object sender, EventArgs e)
        {
            TextBoxCantidadProgramas.Text = "";
            GridPaths.Rows.Clear();
            gridProcesador0.Rows.Clear();
            gridProcesador1.Rows.Clear();
            gridProcesador2.Rows.Clear();
            gridMemoriaCompartida.Rows.Clear();
            TextBoxCantidadProgramas.Enabled = true;
            BotonAgregarArchivo.Enabled = true;
            cantidadProgramasPorGrid[0] = cantidadProgramasPorGrid[1] = cantidadProgramasPorGrid[2] = 0;
        }

        /*
         * Abre el FileChooser para poder escoger un archivo que será agregado al grid.
         */
        private void BotonAgregarArchivo_Click(object sender, EventArgs e)
        {
            FileChooser.Reset();
            FileChooser.ShowDialog();
        }

        /*
         * Al escogerse un archivo, se agrega el path del mismo al grid. También, cuando haya escogido la misma cantidad de archivos
         * que especificó en el textbox, se habilita el botón de iniciar la simulación.
         */
        private void FileChooser_FileOk(object sender, CancelEventArgs e)
        {
            String pathNuevoArchivo = FileChooser.FileName;
            GridPaths.Rows.Add(pathNuevoArchivo);
            if (Convert.ToInt32(TextBoxCantidadProgramas.Text) == GridPaths.Rows.Count)
                BotonIniciarSimulacion.Enabled = true;
        }

        /*
         * Lee desde los archivos todas las instrucciones para luego enviarlas al simulador.
         */
        private void BotonIniciarSimulacion_Click(object sender, EventArgs e)
        {
            if ((Convert.ToInt32(TextBoxCantidadProgramas.Text) != GridPaths.Rows.Count) || (TextBoxCantidadProgramas.TextLength == 0))
                return; //TODO mostrar un mensaje de error

            //Deshabilitar botones para no entorpecer la interfaz durante la simulación (TODO agregar botón para detenerla)
            BotonNuevaSimulacion.Enabled = false;
            TextBoxCantidadProgramas.Enabled = false;
            BotonAgregarArchivo.Enabled = false;
            BotonIniciarSimulacion.Enabled = false;
            panelProcesadores.Visible = true; //también hace este panel visible para ver los programas correr

            //Lee todas las instrucciones y las guarda en una lista
            List<int> instrucciones = new List<int>();          //arreglo general que almacenará todas las instrucciones leídas
            List<int> iniciosProgramas = new List<int>();       //arreglo pequeño que almacena los índices en el anterior donde inicia cada programa
            List<string> nombresProgramas = new List<string>();
            String pathArchivo = "";            //usada para iterar por los paths
            String instruccionIndividual = "";  //usada para iterar por las líneas de los archivos
            int parteInstruccion = 0;           //usada para iterar por los números enteros en cada línea
            foreach (DataGridViewRow fila in GridPaths.Rows)
            {
                pathArchivo = fila.Cells[0].Value.ToString();
                nombresProgramas.Add(pathArchivo.Substring(pathArchivo.LastIndexOf('\\') + 1));
                iniciosProgramas.Add(instrucciones.Count);
                System.IO.TextReader lector = System.IO.File.OpenText(pathArchivo); //abre el archivo para Leer sus líneas una por una
                while ((instruccionIndividual = lector.ReadLine()) != null)
                {
                    string[] partes = instruccionIndividual.Split(' '); //divide cada línea en partes, usando los espacios como token separador
                    if (partes.Length == 4)
                    {
                        for (short i = 0; i < 4; ++i)
                        {
                            instrucciones.Add(parteInstruccion = int.Parse(partes[i])); //agrega cada número entero al arreglo
                        }
                    }
                }
                lector.Close();
            }

            //Enviar parámetros al simulador e iniciar la simulación
            Simulador simulador = new Simulador(instrucciones, iniciosProgramas, nombresProgramas, this);
            simulador.CantidadProgramas = Convert.ToInt16(TextBoxCantidadProgramas.Text); //se asume que sólo escribirán números
            Thread hiloSimulacion = new Thread(simulador.ejecutarSimulacion);
            hiloSimulacion.Start();

            // NO hacer join al hiloSimulacion porque sino se detienen los eventos de la interfaz grafica
        }


        public void onTickChanged(int newTick)
        {
            // TODO La simulacion llama a este metodo cada vez que termina un tick
            // De forma que en este metodo se puede actualizar la interfaz

        }


        /*
         * Simplemente actualiza la label que indica el nombre del programa (archivo de programa) que está corriendo
         * en uno de los procesadores.
         */
        public void onProgramNameChanged(String nombrePrograma, int idProcesador)
        {
            switch (idProcesador)
            {
                case 0:
                    labelProcesador0Corriendo.Text = nombrePrograma;
                    break;
                case 1:
                    labelProcesador1Corriendo.Text = nombrePrograma;
                    break;
                case 2:
                    labelProcesador2Corriendo.Text = nombrePrograma;
                    break;
            }
        }

        
        public void onProgramCounterChanged(int newPc, int idProcesador) 
        {
            // IMPLEMENTAR ESTO!

        }

        public void onRegistersChanged(int[] nuevosRegistros, int idProcesador) 
        {
            // IMPLEMENTAR ESTO!

        }

        public void onCacheChanged(int[] palabrasCache, int idProcesador) 
        {
            // IMPLEMENTAR ESTO!

        }

        public void onMemoryChanged(int[] palabrasMemoria, int idProcesador) 
        {
            // IMPLEMENTAR ESTO!

        }

        public void onSimulationFinished()
        {
            // IMPLEMENTAR ESTO!

            // Habilitar el boton hasta que termine la simulacion
            BotonNuevaSimulacion.Enabled = true;
        }

        /*
         * Cuando un procesador empieza a simular un nuevo programa, se deben crear las tuplas que mostrarán los resultados
         * de esa simulación, con valores iniciales.
         */
        public void crearTuplasResultado(int idProcesador, String nombrePrograma, int ticsRelojInicio, int[] registros, int[] cache)
        {
            DataGridView grid = null;
            switch (idProcesador)
            {
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
            grid.Rows.Add(nombrePrograma);
            grid.Rows.Add("Reloj al inicio", ticsRelojInicio);
            grid.Rows.Add("Tics totales", 0);
            for (short i = 0; i < 8; ++i)
                grid.Rows.Add("Registros", "R" + i + ": " + registros[i],
                                            "R" + (i + 8) + ": " + registros[(i + 8)],
                                            "R" + (i + 16) + ": " + registros[(i + 16)],
                                            "R" + (i + 24) + ": " + registros[(i + 24)]);
            for (short i = 0; i < 4; ++i)
                grid.Rows.Add("Caché datos", cache[i],
                                            cache[i + 4],
                                            cache[i + 8],
                                            cache[i + 12]);
            grid.Rows.Add("-", "-", "-", "-", "-");
            cantidadProgramasPorGrid[idProcesador]++;
        }

        /*
         * Conforme los procesadores simulan los programas, la interfaz va actualizando visualmente el estado
         * de los datos de interés de cada simulación.
         */
        public void actualizarTuplasResultado(int idProcesador, int ticsReloj, int[] registros, int[] cache)
        {
            DataGridView grid = null;
            switch (idProcesador)
            {
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
            int tuplaInicial = (cantidadProgramasPorGrid[idProcesador] - 1) * 16; //28 tuplas por programa (3 titulo, 8 registros, 4 cache, 1 final)
            grid.Rows[tuplaInicial + 2].Cells[1].Value = (ticsReloj - (int)grid.Rows[tuplaInicial + 1].Cells[1].Value);
            for (short i = 0; i < 8; ++i)
            {
                grid.Rows[i + 3 + tuplaInicial].Cells[1].Value = "R" + i + ": " + registros[i];
                grid.Rows[i + 3 + tuplaInicial].Cells[2].Value = "R" + (i + 8) + ": " + registros[(i + 8)];
                grid.Rows[i + 3 + tuplaInicial].Cells[3].Value = "R" + (i + 16) + ": " + registros[(i + 16)];
                grid.Rows[i + 3 + tuplaInicial].Cells[4].Value = "R" + (i + 24) + ": " + registros[(i + 24)];
            }
            for (short i = 0; i < 4; ++i)
            {
                grid.Rows[i + 11 + tuplaInicial].Cells[1].Value = cache[i];
                grid.Rows[i + 11 + tuplaInicial].Cells[2].Value = cache[i + 4];
                grid.Rows[i + 11 + tuplaInicial].Cells[3].Value = cache[i + 8];
                grid.Rows[i + 11 + tuplaInicial].Cells[4].Value = cache[i + 12];
            }
        }

    }

}
