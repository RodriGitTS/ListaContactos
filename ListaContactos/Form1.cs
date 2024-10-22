using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListaContactos
{
    public partial class Form1 : Form
    {
        ArrayList contactos = new ArrayList();
        private const String EXPRESION_NOM_AP = @"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s'-]+$";
        private const String EXPRESION_CORREO = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text="";
        }




        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (comprobarDatos())
            {
                epRegistro.Clear();
                String nombre = txtNombre.Text;
                String apellidos = txtApellidos.Text;
                String tlf = msktxtTlf.Text;
                String correo = txtCorreo.Text;
                DateTime fecha = datePkFechaNac.Value;

                contactos.Add(new Contacto(nombre, apellidos, tlf, correo, fecha));
                toolStripStatusLabel1.Text = "Contacto añadido";

            }

            else MessageBox.Show("Error en los datos", "Alguno de los campos no son correctos",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            epRegistro.Clear();
            lstContactos.Items.Clear();
            foreach (Contacto contacto in contactos)
            {
                lstContactos.Items.Add(contacto);
            }
        }

        private void lstContactos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contacto contactoSeleccionado = lstContactos.SelectedItem as Contacto;

            txtNombre.Text = contactoSeleccionado.getNombre();
            txtApellidos.Text = contactoSeleccionado.getApellido();
            txtCorreo.Text = contactoSeleccionado.getCorreo();
            msktxtTlf.Text = contactoSeleccionado.getTlf();
            datePkFechaNac.Value=contactoSeleccionado.getFechaNac();


        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            epRegistro.Clear();
            if (lstContactos.SelectedItem == null) { 
            MessageBox.Show("No has seleccionado ningun contacto");
            }
            else
            {
             DialogResult dia= MessageBox.Show("¿Borrar contacto?", "Confirmación borrado", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dia==DialogResult.Yes)
                {
                    contactos.Remove(lstContactos.SelectedItem);
                    toolStripStatusLabel1.Text = "Contacto borrado";
                } 
            }
        }

        private void ToolStripSalir_Click(object sender, EventArgs e)
        {
            DialogResult dia = MessageBox.Show("¿Salir de la app?", "", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dia == DialogResult.Yes)
            {
                Close();
            }
           
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            epRegistro.Clear();
            string path = "contactos.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Contacto contacto in contactos)
                {
                    writer.WriteLine(contacto.getNombre() + ";" + contacto.getApellido() + ";" +
                        contacto.getCorreo() + ";" + contacto.getTlf() + ";" + datePkFechaNac.Value.ToString("dd/MM/yyyy") + "\n");
                }
            }
            toolStripStatusLabel1.Text = "Contactos guardado en contacto en " + path;
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime fechaNacimiento;
            epRegistro.Clear();
            contactos = new ArrayList();
            string path = "contactos.txt";
            string linea = "";
            using (StreamReader lecutra =new StreamReader(path))
            {
               
                while (linea != null)
                {
                    linea = lecutra.ReadLine();
                    if (linea == null) break;
                    String[] c = new String[5];
                    c = linea.Split(';');

                    if (c.Length == 5)
                    {
                        String nombre = c[0];
                        String apellidos = c[1];
                        String correo = c[2];
                        String tlf = c[3];
                        String fechaS = c[4];
                        DateTime fecha = DateTime.ParseExact(c[4], "dd/MM/yyyy", null);

                        contactos.Add(new Contacto(nombre, apellidos, tlf, correo, fecha));
                        
                    }
                    toolStripStatusLabel1.Text = "Lista de contactos cargada";
                }
            }
        }

        private void formatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowDialog();

            if (DialogResult == DialogResult.OK) {
                Font = fontDialog.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cuadro=new ColorDialog();
            cuadro.ShowDialog();
            if (DialogResult == DialogResult.OK) {
                BackColor = cuadro.Color;

            }

        }
        private Boolean comprobarDatos()
        {
            Regex regexNomAp = new Regex(EXPRESION_NOM_AP);
            Regex regexCorreo = new Regex(EXPRESION_CORREO);

            epRegistro.Clear();
            Boolean datosCorrectos=true;
            DateTime fechaNacimiento;


            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                datosCorrectos = false;
                epRegistro.SetError(txtNombre,"Campo vacio");
            }else if (!regexNomAp.IsMatch(txtNombre.Text))
            {
                datosCorrectos = false;
                epRegistro.SetError(txtNombre, "Datos incorrectos");
            }

            if (String.IsNullOrEmpty(txtApellidos.Text))
            {

                datosCorrectos = false;
                epRegistro.SetError(txtApellidos, "Campo vacio");
            }
            else if (!regexNomAp.IsMatch(txtApellidos.Text))
            {
                datosCorrectos = false;
                epRegistro.SetError(txtApellidos, "Datos incorrectos");
            }

            if (String.IsNullOrEmpty(msktxtTlf.Text)||msktxtTlf.Text== "   -   -")
            {

                datosCorrectos = false;
                epRegistro.SetError(msktxtTlf, "Campo vacio");
            }else if(msktxtTlf.Text.Length < 1){
                datosCorrectos = false;
                epRegistro.SetError(msktxtTlf,"Campo incompleto");
            }
            if (String.IsNullOrEmpty(txtCorreo.Text))
            {

                datosCorrectos = false;
                epRegistro.SetError(txtCorreo, "Campo vacio");
            }else if (!regexCorreo.IsMatch(txtCorreo.Text))
            {
                datosCorrectos = false;
                epRegistro.SetError(txtCorreo, "Campo incorrecto");
            }
            if (!DateTime.TryParse(datePkFechaNac.Text, out fechaNacimiento)||fechaNacimiento==DateTime.Today)
            {
                datosCorrectos = false;
                epRegistro.SetError(datePkFechaNac, "Campo vacio");
            }else if (fechaNacimiento > DateTime.Today)
            {

                datosCorrectos = false;
                epRegistro.SetError(datePkFechaNac, "Fecha incorrecta");
            }
            foreach (Contacto contacto in contactos) { 
            
               if(contacto.getNombre()==txtNombre.Text  && contacto.getApellido() == txtApellidos.Text)
                {
                    datosCorrectos = false;
                    MessageBox.Show("Este contacto ya existe","Error al guardar contacto",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
          

            return datosCorrectos;
        }
    }
}
