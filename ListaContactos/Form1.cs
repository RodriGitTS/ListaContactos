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
        ArrayList contactos = new ArrayList();
        public Form1()
        {
            InitializeComponent();

        }




        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            String nombre = txtNombre.Text;
            String apellidos = txtApellidos.Text;
            String tlf = msktxtTlf.Text;
            String correo = txtCorreo.Text;
            DateTime fecha = datePkFechaNac.Value;

            contactos.Add(new Contacto(nombre, apellidos, tlf, correo, fecha));
            toolStripStatusLabel1.Text = "Contacto añadido";
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            lstContactos.Items.Clear();
            foreach (Contacto contacto in contactos)
            {
                lstContactos.Items.Add(contacto);
            }
        }
    }
}
