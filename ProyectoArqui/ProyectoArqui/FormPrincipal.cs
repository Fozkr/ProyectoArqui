using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoArqui
{
    /*
     * 
     */
    public partial class FormPrincipal : Form
    {
        //Atributos
        Controladora hiloMaestro;

        public FormPrincipal()
        {
            InitializeComponent();
            hiloMaestro = new Controladora();
        }

        /*
         * Cuando escriba la cantidad de programas que serán ejecutados en la simulación, se guardan en el atributo de la clase,
         * más adelante debe bloquearse este textbox, cuando la simulación sea iniciada.
         */
        private void TextBoxCantidadProgramas_TextChanged(object sender, EventArgs e)
        {
            hiloMaestro.CantidadProgramas = Convert.ToInt32(TextBoxCantidadProgramas.Text);
        }

        private void BotonAgregarArchivo_Click(object sender, EventArgs e)
        {
            FileChooser.ShowDialog();
        }

        private void FileChooser_FileOk(object sender, CancelEventArgs e)
        {

        }

       
    }
}
