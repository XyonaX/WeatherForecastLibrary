# WeatherForecastLibrary

La `WeatherForecastLibrary` es una biblioteca que simula el funcionamiento de una estación meteorológica, permitiendo registrar y consultar múltiples temperaturas en distintos días y semanas. La biblioteca también gestiona un equipo de personal que toma las mediciones, alternando entre ellos de forma automática.

## Clases Principales

### `EstacionMeteorologica`

Esta es la clase principal que maneja el registro de las temperaturas.

#### Propiedades

- **`registros`**: Matriz de listas de `RegistroTemperatura`, que almacena las temperaturas registradas para cada día de la semana en un período de tiempo.
- **`semanas`**: Número de semanas en la estación meteorológica.
- **`dias`**: Número de días por semana en la estación meteorológica.
- **`personal`**: Lista de `Persona` que representa el personal a cargo de registrar las temperaturas.
- **`turnoIndex`**: Índice que rastrea a quién le toca registrar la temperatura.

#### Métodos

- **`EstacionMeteorologica(int semanas = 5, int dias = 7)`**: Constructor que inicializa la estación con el número especificado de semanas y días.
- **`RegistrarTemperatura(RegistroTemperatura registro, int semana, int dia)`**: Registra una temperatura en una semana y día específicos.
- **`VerTemperaturas(string tipo = "todas")`**: Devuelve una lista de listas de temperaturas registradas. Puede filtrar por "todas" las temperaturas o "porfecha" solicitando una fecha específica.
- **`CargarTemperaturasAutomatica()`**: Carga automáticamente temperaturas aleatorias en la matriz `registros` para cada día.
- **`VerTempPorFecha(DateTime fecha)`**: Devuelve una lista de `RegistroTemperatura` para una fecha específica.
- **`ObtenerPersonaDeTurno()`**: Devuelve la persona que está de turno en el último registro.
- **`VerTemperaturaDiaEspecifico(int semana, int dia)`**: Devuelve la lista de temperaturas registradas en una semana y día específicos.

### `RegistroTemperatura`

Representa un registro de temperatura en la estación meteorológica.

#### Propiedades

- **`Temperatura`**: La temperatura registrada.
- **`PersonaDeTurno`**: La persona que registró la temperatura.
- **`FechaRegistro`**: La fecha en la que se registró la temperatura.

#### Métodos

- **`RegistroTemperatura(double temperatura, Persona personaDeTurno, DateTime fechaRegistro)`**: Constructor que inicializa un registro con la temperatura, persona de turno y fecha.
- **`ObtenerTemperatura()`**: Devuelve la temperatura registrada.
- **`ObtenerPersonaDeTurno()`**: Devuelve la persona que registró la temperatura.
- **`ObtenerFechaRegistro()`**: Devuelve la fecha del registro.

### `Persona` (Clase abstracta)

Clase base para representar a una persona en la estación meteorológica.

#### Propiedades

- **`Nombre`**: El nombre de la persona.

#### Métodos

- **`Persona(string nombre)`**: Constructor que inicializa una persona con un nombre.
- **`ObtenerNombre()`**: Devuelve el nombre de la persona.

### `Pasante` y `Profesional`

Clases derivadas de `Persona` que representan tipos específicos de personal.

#### Propiedades (Pasante)

- **`NumeroLegajo`**: Número de legajo del pasante.

#### Métodos (Pasante)

- **`Pasante(string nombre, int numeroLegajo)`**: Constructor que inicializa un pasante con su nombre y número de legajo.
- **`ObtenerNumeroLegajo()`**: Devuelve el número de legajo del pasante.

#### Propiedades (Profesional)

- **`NumeroMatricula`**: Número de matrícula del profesional.

#### Métodos (Profesional)

- **`Profesional(string nombre, int numeroMatricula)`**: Constructor que inicializa un profesional con su nombre y número de matrícula.
- **`ObtenerNumeroMatricula()`**: Devuelve el número de matrícula del profesional.

## Ejemplo de Uso

```csharp
using System;
using WeatherForecastLibrary;

class Program
{
    static void Main(string[] args)
    {
        // Crear una instancia de EstacionMeteorologica
        EstacionMeteorologica estacion = new EstacionMeteorologica();

        // Cargar temperaturas automáticamente
        estacion.CargarTemperaturasAutomatica();

        // Ver todas las temperaturas registradas
        var todasLasTemperaturas = estacion.VerTemperaturas("todas");

        Console.WriteLine("Todas las temperaturas registradas:");
        foreach (var diaTemperaturas in todasLasTemperaturas)
        {
            Console.WriteLine("Temperaturas del día:");
            foreach (var temp in diaTemperaturas)
            {
                Console.WriteLine($"  - {temp}ºC");
            }
        }

        // Ver temperatura promedio
        double promedio = CalculoTemperaturas.CalcularPromedio(todasLasTemperaturas);
        Console.WriteLine($"\nTemperatura Promedio: {promedio:F2}ºC");

        // Ver temperatura máxima y mínima
        double maxTemp = CalculoTemperaturas.EncontrarTemperaturaMaxima(todasLasTemperaturas);
        double minTemp = CalculoTemperaturas.EncontrarTemperaturaMinima(todasLasTemperaturas);
        Console.WriteLine($"Temperatura Máxima: {maxTemp}ºC");
        Console.WriteLine($"Temperatura Mínima: {minTemp}ºC");

        // Consultar temperaturas por fecha específica
        Console.Write("Ingrese una fecha (YYYY-MM-DD): ");
        DateTime fechaIngresada;
        if (DateTime.TryParse(Console.ReadLine(), out fechaIngresada))
        {
            var registrosEnFecha = estacion.VerTempPorFecha(fechaIngresada);
            Console.WriteLine($"\nTemperaturas registradas el {fechaIngresada.ToShortDateString()}:");
            foreach (var registro in registrosEnFecha)
            {
                Console.WriteLine($"{registro.ObtenerTemperatura()}°C");
            }
        }
    }
}
