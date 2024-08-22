using System;
using System.Collections.Generic;

namespace WeatherForecastLibrary
{
    public class EstacionMeteorologica
    {
        private List<RegistroTemperatura>[,] registros;
        private int semanas;
        private int dias;
        private List<Persona> personal;
        private int turnoIndex;

        public EstacionMeteorologica(int semanas = 5, int dias = 7)
        {
            this.semanas = semanas;
            this.dias = dias;
            this.registros = new List<RegistroTemperatura>[semanas, dias];
            this.turnoIndex = 0;

            // inicializar cada celda de la matriz de registros como una lista vacia
            for (int i = 0; i < semanas; i++)
            {
                for (int j = 0; j < dias; j++)
                {
                    registros[i, j] = new List<RegistroTemperatura>();
                }
            }

            // inicializar la lista de personal
            this.personal = new List<Persona>
            {
                new Pasante("Mario", 101),
                new Profesional("Ana", 201),
                new Pasante("Luis", 102),
                new Profesional("Pedro", 202),
                new Pasante("Sofia", 103),
                new Profesional("Laura", 203)
            };

            // carga de temperaturas automatica
            //CargarTemperaturasAutomatica();
        }

        public void RegistrarTemperatura(RegistroTemperatura registro, int semana, int dia)
        {
            if (semana < semanas && dia < dias)
            {
                // añadir el registro a la lista de ese día
                registros[semana, dia].Add(registro);
                // actualizar el indice de turno
                turnoIndex = (turnoIndex + 1) % personal.Count;
            }
            else
            {
                Console.WriteLine("La semana o el día están fuera de rango.");
            }
        }

        public List<List<double>> VerTemperaturas(string tipo = "todas")
        {
            List<List<double>> temperaturasPorDia = new List<List<double>>();

            switch (tipo.ToLower())
            {
                case "todas":
                    for (int i = 0; i < semanas; i++)
                    {
                        for (int j = 0; j < dias; j++)
                        {
                            List<double> temperaturas = new List<double>();
                            foreach (var registro in registros[i, j])
                            {
                                temperaturas.Add(registro.ObtenerTemperatura());
                            }
                            temperaturasPorDia.Add(temperaturas);
                        }
                    }
                    break;

                case "porfecha":
                    Console.Write("Ingrese una fecha (YYYY-MM-DD): ");
                    DateTime fechaIngresada;
                    if (DateTime.TryParse(Console.ReadLine(), out fechaIngresada))
                    {
                        var tempFecha = VerTempPorFecha(fechaIngresada);
                        foreach (var temp in tempFecha)
                        {
                            temperaturasPorDia.Add(new List<double> { temp.ObtenerTemperatura() });
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fecha no válida.");
                    }
                    break;

                default:
                    Console.WriteLine("Tipo no reconocido.");
                    break;
            }

            return temperaturasPorDia;
        }

        public void CargarTemperaturasAutomatica()
        {
            DateTime diaInicio = new DateTime(2024, 8, 1);
            int diasCount = 0;

            for (int i = 0; i < semanas && diasCount < 31; i++)
            {
                for (int j = 0; j < dias && diasCount < 31; j++)
                {
                    for (int t = 0; t < 3; t++)  // registrar hasta 3 temperaturas por día
                    {
                        Persona persona = personal[turnoIndex];
                        Random rand = new Random();
                        double temp = Math.Round(-5 + rand.NextDouble() * 40, 1);

                        RegistroTemperatura registro = new RegistroTemperatura(
                            temp,
                            persona,
                            diaInicio.AddDays(diasCount)
                        );

                        RegistrarTemperatura(registro, i, j);
                    }
                    diasCount++;
                }
            }
        }

        public List<RegistroTemperatura> VerTempPorFecha(DateTime fecha)
        {
            List<RegistroTemperatura> registrosEnFecha = new List<RegistroTemperatura>();

            for (int i = 0; i < semanas; i++)
            {
                for (int j = 0; j < dias; j++)
                {
                    foreach (var registro in registros[i, j])
                    {
                        if (registro.ObtenerFechaRegistro().Date == fecha.Date)
                        {
                            registrosEnFecha.Add(registro);
                        }
                    }
                }
            }

            return registrosEnFecha;
        }

        public Persona ObtenerPersonaDeTurno()
        {
            Persona persona = personal[turnoIndex];
            return persona;
        }

        public List<double> VerTemperaturaDiaEspecifico(int semana, int dia)
        {
            if (semana < semanas && dia < dias)
            {
                List<double> temperaturas = new List<double>();
                foreach (var registro in registros[semana, dia])
                {
                    temperaturas.Add(registro.ObtenerTemperatura());
                }
                return temperaturas;
            }
            else
            {
                throw new Exception("Semana o día fuera de rango, o no hay temperatura registrada.");
            }
        }
    }
}
