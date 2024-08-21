using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastLibrary
{
    public class Pasante : Persona
    {
        private int NumeroLegajo { get; set; }
        public Pasante(string nombre, int numeroLegajo) : base(nombre)
        {
            NumeroLegajo = numeroLegajo;
        }

        public int ObtenerNumeroLegajo()
        {
            return NumeroLegajo;
        }

    }
}
