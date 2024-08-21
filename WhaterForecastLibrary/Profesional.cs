using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastLibrary
{
    public class Profesional : Persona
    {
        private int NumeroMatricula {  get; set; }

        public Profesional (String nombre, int numeroMatricula) : base (nombre) {
            NumeroMatricula = numeroMatricula;
        }

        public int ObtenerNumeroMatricula()
        {
            return NumeroMatricula;
        }
    }
}
