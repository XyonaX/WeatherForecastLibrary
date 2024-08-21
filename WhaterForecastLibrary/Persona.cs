using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastLibrary
{
    public abstract class Persona
    {
        private String Nombre { get; set; }

        protected Persona(String nombre)
        {
            Nombre = nombre;
        }

        public string ObtenerNombre()
        {
            return Nombre;
        }

    }
}
