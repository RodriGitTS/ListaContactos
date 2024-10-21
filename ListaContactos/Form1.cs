using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListaContactos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ArrayList contactos = new ArrayList();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            ErrorProvider frm = new ErrorProvider();

            String nombre=txtNombre.Text;
            String apellidos=txtApellidos.Text;
            String tlf=txtTlf.Text;
            String correo=txtCorreo.Text;
            DateTime fecha = datePkFechaNac.Value;
           
        }
    }
}
