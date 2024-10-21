using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaContactos
{
    internal class Contacto
    {
        String nombre;
        String apellido;
        String telefono;
        String correo;
        DateTime fechaNac;
        public Contacto(String nombre, String apellido, String telefono, String correo, DateTime fechaNac){ 
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.correo = correo;
            this.fechaNac = fechaNac;
        }

        override public String ToString()
        {
            return nombre+ " "+ apellido +"-"+correo+"-"+telefono;
        }

        public String getNombre()
        {
            return this.nombre;
        }
        public String getApellido()
        {
            return this.apellido;
        }
        public String getCorreo()
        {
            return this.correo;   
        }
        public String getTlf()
        {
            return this.telefono;
        }
        public DateTime getFechaNac()
        {
            return fechaNac;
        }
    }
}
