using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Cálculo de límites aproximado ===\n");

        // Menú de funciones
        Console.WriteLine("Seleccione una función:");
        Console.WriteLine("1) f(x) = x^2 - 1");
        Console.WriteLine("2) f(x) = (x^2 - 1)/(x - 1)");
        Console.WriteLine("3) f(x) = 1/(x - 2)");
        Console.Write("Opción: ");

        if (!int.TryParse(Console.ReadLine(), out int opcion) || opcion < 1 || opcion > 3)
        {
            Console.WriteLine("Opción inválida. Saliendo...");
            return;
        }

        // Ingreso de x0
        Console.Write("Ingrese el punto x0 al cual x se aproxima: ");
        if (!double.TryParse(Console.ReadLine(), out double x0))
        {
            Console.WriteLine("Valor inválido. Saliendo...");
            return;
        }

        Console.WriteLine($"\nAnalizando función seleccionada en x0 = {x0}\n");

        double delta = 0.0001;

        // Calcular límites laterales
        double limiteIzq = AnalizarComportamiento(opcion, x0 - delta);
        double limiteDer = AnalizarComportamiento(opcion, x0 + delta);

        Console.WriteLine("Valores cercanos a x0:");
        for (double dx = -5; dx <= 5; dx++)
        {
            if (dx == 0)    // Saltar x0
            {
                Console.WriteLine("---");
                continue;
            }

            double x = x0 + dx * delta;
            double fx = AnalizarComportamiento(opcion, x);
            Console.WriteLine($"x = {x:F5} -> f(x) = {fx:F5}");
        }

        Console.WriteLine("\nResultados del límite:");

        // Caso de discontinuidad infinita
        if (double.IsInfinity(limiteIzq) || double.IsInfinity(limiteDer))
        {
            if (limiteIzq > 0 && limiteDer > 0)
                Console.WriteLine("Límite en x0 = +∞");
            else if (limiteIzq < 0 && limiteDer < 0)
                Console.WriteLine("Límite en x0 = -∞");
            else
                Console.WriteLine("Límite no existe (discontinuidad infinita, valores laterales de distinto signo)");
        }

        // Caso de límites laterales prácticamente iguales (función continua)
        else if (Math.Abs(limiteIzq - limiteDer) < 0.01)
        {
            // Calcula el valor del limite como el promedio de los valores laterales
            double limite = (limiteIzq + limiteDer) / 2;
            Console.WriteLine($"Límite en x0 ≈ {Math.Round(limite, 4)}");
        }

        // Caso de límites laterales diferentes (discontinuidad)
        else
        {
            Console.WriteLine("Límite no existe (valores laterales diferentes, discontinuidad)");
        }

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }

    static double AnalizarComportamiento(int opcion, double x)
    {
        try
        {
            switch (opcion)
            {
                case 1: // f(x) = x^2 - 1
                    return x * x - 1;
                case 2: // f(x) = (x^2 - 1)/(x - 1)
                    return (x * x - 1) / (x - 1);
                case 3: // f(x) = 1/(x - 2)
                    return 1 / (x - 2);
                default:
                    return 0;
            }
        }
        catch (DivideByZeroException)         // Division por cero
        {
            return double.PositiveInfinity;
        }
        catch
        {
            return double.NaN;
        }
    }
}
