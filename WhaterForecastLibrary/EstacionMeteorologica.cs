using System;
using System.Collections.Generic;

namespace WeatherForecastLibrary
{
    public class EstacionMeteorologica
    {
        private RegistroTemperatura[,] registros;
        private int semanas;
        private int dias;
        private List<Persona> personal;
        private int turnoIndex;

        public EstacionMeteorologica(int semanas = 5, int dias = 7)
        {
            this.semanas = semanas;
            this.dias = dias;
            this.registros = new RegistroTemperatura[semanas, dias];
            this.turnoIndex = 0;  // Índice de turno para alternar entre pasantes y profesionales

            // Inicializa la lista de personal
            this.personal = new List<Persona>
            {
                new Pasante("Mario", 101),
                new Profesional("Ana", 201),
                new Pasante("Luis", 102),
                new Profesional("Pedro", 202),
                new Pasante("Sofia", 103),
                new Profesional("Laura", 203)
            };

            // Carga de temperaturas automática con personas intercaladas
            CargarTemperaturasAutomatica();
        }

        public void RegistrarTemperatura(RegistroTemperatura registro, int semana, int dia)
        {
            if (semana < semanas && dia < dias)
            {
                registros[semana, dia] = registro;
                // Actualiza el índice para el próximo turno
                turnoIndex = (turnoIndex + 1) % personal.Count;
            }
            else
            {
                Console.WriteLine("La semana o el día están fuera de rango.");
            }
        }

        public List<double> VerTemperaturas(string tipo = "todas")
        {
            List<double> temperaturas = new List<double>();

            switch (tipo.ToLower())
            {
                case "todas":
                    for (int i = 0; i < semanas; i++)
                    {
                        for (int j = 0; j < dias; j++)
                        {
                            if (registros[i, j] != null)
                                temperaturas.Add(registros[i, j].obtenerTemperatura());
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
                            temperaturas.Add(temp.obtenerTemperatura());
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

            return temperaturas;
        }

        private void CargarTemperaturasAutomatica()
        {
            DateTime diaInicio = new DateTime(2024, 8, 1);
            int diasCount = 0; // Contador para asegurar que no se cargue más de 31 días

            for (int i = 0; i < semanas && diasCount < 31; i++)
            {
                for (int j = 0; j < dias && diasCount < 31; j++)
                {
                    // Alterna entre pasantes y profesionales
                    Persona persona = personal[turnoIndex];
                    Random rand = new Random();
                    double temp = Math.Round(-5 + rand.NextDouble() * 40, 1);

                    RegistroTemperatura registro = new RegistroTemperatura(
                        temp,  // Temperatura ficticia para demostración
                        persona,
                        diaInicio.AddDays(diasCount)
                    );

                    RegistrarTemperatura(registro, i, j);
                    diasCount++;
                }
            }
        }

        public void CargarTemperaturasManual()
        {
            for (int i = 0; i < semanas; i++)
            {
                for (int j = 0; j < dias; j++)
                {
                    Console.Write($"Ingrese la temperatura para la semana {i + 1}, día {j + 1}: ");
                    if (double.TryParse(Console.ReadLine(), out double temp))
                    {
                        Persona personaDeTurno = ObtenerPersonaDeTurno();
                        registros[i, j] = new RegistroTemperatura(temp, personaDeTurno, DateTime.Now);
                    }
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
                    if (registros[i, j] != null && registros[i, j].ObtenerFechaRegistro().Date == fecha.Date)
                    {
                        registrosEnFecha.Add(registros[i, j]);
                    }
                }
            }

            return registrosEnFecha;
        }

        public Persona ObtenerPersonaDeTurno()
        {
            // Devuelve la persona según el índice del turno
            Persona persona = personal[turnoIndex];
            return persona;
        }

        public double VerTemperaturaDiaEspecifico(int semana, int dia)
        {
            if (semana < semanas && dia < dias && registros[semana, dia] != null)
            {
                return registros[semana, dia].obtenerTemperatura();
            }
            else
            {
                throw new Exception("Semana o día fuera de rango, o no hay temperatura registrada.");
            }
        }
    }
}
