using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastLibrary
{
    public class RegistroTemperatura
    {
        private double Temperatura {  get; set; }
        private Persona PersonaDeTurno { get; set; }
        private DateTime FechaRegistro { get; set; }

        public RegistroTemperatura(double temperatura, Persona personaDeTurno, DateTime fechaRegistro)
        {
            Temperatura = temperatura;
            PersonaDeTurno = personaDeTurno;
            FechaRegistro = fechaRegistro;
        }

        public double obtenerTemperatura()
        {
            return Temperatura; 
        }

        public Persona ObtenerPersonaDeTurno()
        {
            return PersonaDeTurno;
        }

        public DateTime ObtenerFechaRegistro()
        {
            return FechaRegistro;
        }
    }
}
