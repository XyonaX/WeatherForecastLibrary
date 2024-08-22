using System;
using System.Collections.Generic;
using WeatherForecastLibrary;

class Program
{
    static void Main(string[] args)
    {
        // Crear una instancia de EstacionMeteorologica con 5 semanas y 7 días
        EstacionMeteorologica estacion = new EstacionMeteorologica();
        estacion.CargarTemperaturasAutomatica();

        // Ver todas las temperaturas registradas
        List<List<double>> todasLasTemperaturas = estacion.VerTemperaturas();
        int count = 1;
        Console.WriteLine("Todas las temperaturas registradas:");
        foreach (var diaTemperaturas in todasLasTemperaturas)
        {
            if (count == 32) break;
            Console.WriteLine($"Temperaturas del día: {count}");
            foreach (var temp in diaTemperaturas)
            {
                Console.WriteLine($"  - {temp}ºC");
            }

            count++;
        }

        // Ver temperatura promedio
        double promedio = CalculoTemperaturas.CalcularPromedio(todasLasTemperaturas);
        Console.WriteLine($"\nTemperatura Promedio: {promedio:F2}ºC");

        // Ver temperatura máxima
        double maxTemp = CalculoTemperaturas.EncontrarTemperaturaMaxima(todasLasTemperaturas);
        Console.WriteLine($"Temperatura Maxima: {maxTemp}ºC");

        // Ver temperatura mínima
        double minTemp = CalculoTemperaturas.EncontrarTemperaturaMinima(todasLasTemperaturas);
        Console.WriteLine($"Temperatura Minima: {minTemp}ºC");

        // Ver temperaturas por fecha
        Console.Write("\nIngrese una fecha (YYYY-MM-DD): ");
        DateTime fechaIngresada;
        if (DateTime.TryParse(Console.ReadLine(), out fechaIngresada))
        {
            var registrosEnFecha = estacion.VerTempPorFecha(fechaIngresada);

            if (registrosEnFecha.Count > 0)
            {
                Console.WriteLine($"\nTemperaturas registradas el {fechaIngresada.ToShortDateString()}:");
                foreach (var registro in registrosEnFecha)
                {
                    Console.WriteLine($"{registro.ObtenerTemperatura()}°C");
                }
            }
            else
            {
                Console.WriteLine($"No hay registros de temperatura para la fecha {fechaIngresada.ToShortDateString()}.");
            }
        }
        else
        {
            Console.WriteLine("Fecha no válida.");
        }

        // Ver la temperatura de un día específico
        Console.Write("\nIngrese la semana (1-5) para ver la temperatura: ");
        int semana = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Ingrese el día (1-7) para ver la temperatura: ");
        int dia = int.Parse(Console.ReadLine()) - 1;
        try
        {
            List<double> tempDiaEspecifico = estacion.VerTemperaturaDiaEspecifico(semana, dia);
            Console.WriteLine($"Temperaturas registradas en la semana {semana + 1}, día {dia + 1}:");
            foreach (var temp in tempDiaEspecifico)
            {
                Console.WriteLine($"  - {temp}ºC");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Obtener la persona que está de turno (último registro)
        Persona personaDeTurno = estacion.ObtenerPersonaDeTurno();
        if (personaDeTurno != null)
        {
            Console.WriteLine($"\nPersona de turno en el último registro: {personaDeTurno.ObtenerNombre()}");
        }
        else
        {
            Console.WriteLine("No hay registros de temperatura.");
        }
    }

    public static class CalculoTemperaturas
    {
        public static double CalcularPromedio(List<List<double>> temperaturasPorDia)
        {
            double suma = 0;
            int totalTemperaturas = 0;

            foreach (var diaTemperaturas in temperaturasPorDia)
            {
                foreach (var temp in diaTemperaturas)
                {
                    suma += temp;
                    totalTemperaturas++;
                }
            }

            return totalTemperaturas > 0 ? suma / totalTemperaturas : 0;
        }

        public static double EncontrarTemperaturaMaxima(List<List<double>> temperaturasPorDia)
        {
            double maxima = double.MinValue;

            foreach (var diaTemperaturas in temperaturasPorDia)
            {
                foreach (var temp in diaTemperaturas)
                {
                    if (temp > maxima)
                    {
                        maxima = temp;
                    }
                }
            }

            return maxima != double.MinValue ? maxima : 0;
        }

        public static double EncontrarTemperaturaMinima(List<List<double>> temperaturasPorDia)
        {
            double minima = double.MaxValue;

            foreach (var diaTemperaturas in temperaturasPorDia)
            {
                foreach (var temp in diaTemperaturas)
                {
                    if (temp < minima)
                    {
                        minima = temp;
                    }
                }
            }

            return minima != double.MaxValue ? minima : 0;
        }
    }
}
