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
        List<int> memoriaInstrucciones;

        public FormPrincipal()
        {
            InitializeComponent();
            memoriaInstrucciones = new List<int>;
        }
    }
}
