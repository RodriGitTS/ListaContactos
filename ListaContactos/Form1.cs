﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (lstContactos.SelectedItem == null) { 
            MessageBox.Show("No has seleccionado ningun contacto");
            }
            else
            {
             DialogResult dia= MessageBox.Show("¿Borrar contacto?", "Confirmación borrado", MessageBoxButtons.YesNoCancel);
                if (dia==DialogResult.Yes)
                {
                    contactos.Remove(lstContactos.SelectedItem);
                    toolStripStatusLabel1.Text = "Contacto borrado";
                } 
            }
        }

        private void ToolStripSalir_Click(object sender, EventArgs e)
        {
            DialogResult dia = MessageBox.Show("¿Salir de la app?", "", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
            if (dia == DialogResult.Yes)
            {
                Close();
            }
           
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "contactos.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach(Contacto contacto in contactos)
                {
                    writer.WriteLine(contacto.getNombre() + ";" + contacto.getApellido() + ";" + 
                        contacto.getCorreo() + ";" + contacto.getTlf()+";"+datePkFechaNac.Value.ToString("dd/MM/yyyy")+"\n");
                }
            }
            toolStripStatusLabel1.Text = "Contactos guardado en contacto en "+ path;
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
    }
}
