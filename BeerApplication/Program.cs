using BeerApplication;
using Microsoft.EntityFrameworkCore;



{
    DisplayHeader();

    var context = new DataContext();
    context.Database.EnsureCreated();

    bool running = true;



    while (running)
    {
        Console.WriteLine();
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1. Registrar cerveza");
        Console.WriteLine("2. Mostrar cervezas");
        Console.WriteLine("3. Mostrar promedio de calificaciones");
        Console.WriteLine("4. Salir");
        Console.Write("Opción: ");

        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("Debe ingresar un número válido.");
            continue;
        }

        switch (option)
        {
            case 1:
                AddBeer(context);
                break;

            case 2:
                ShowBeers(context);
                break;

            case 3:
                ShowAverage(context);
                break;

            case 4:
                running = false;
                break;

            default:
                Console.WriteLine("La opción seleccionada no es válida.");
                break;
        }
    }

    Console.WriteLine("Gracias por utilizar la aplicación.");
    Console.ReadKey();
}
static void DisplayHeader()
{
    Console.WriteLine("APLICACIÓN DE CERVEZAS");
    Console.WriteLine("----------------------");
}

static void AddBeer(DataContext context)
{
    Console.WriteLine();
    Console.WriteLine("REGISTRAR CERVEZA");

    Console.Write("Ingrese el nombre: ");
    string? name = Console.ReadLine();

    Console.Write("Ingrese el tipo: ");
    string? type = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type))
    {
        Console.WriteLine("El nombre y el tipo son obligatorios.");
        return;
    }

    Console.Write("Ingrese el porcentaje de alcohol: ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal alcoholPercentage))
    {
        Console.WriteLine("El porcentaje de alcohol debe ser numérico.");
        return;
    }

    Console.Write("Ingrese la calificación del 1 al 10: ");

    if (!int.TryParse(Console.ReadLine(), out int rating))
    {
        Console.WriteLine("La calificación debe ser numérica.");
        return;
    }

    if (alcoholPercentage < 0 || alcoholPercentage > 100)
    {
        Console.WriteLine("El porcentaje de alcohol debe estar entre 0 y 100.");
        return;
    }

    if (rating < 1 || rating > 10)
    {
        Console.WriteLine("La calificación debe estar entre 1 y 10.");
        return;
    }

    var beer = new Beer
    {
        Name = name,
        Type = type,
        AlcoholPercentage = alcoholPercentage,
        Rating = rating
    };

    context.Beers.Add(beer);
    context.SaveChanges();

    Console.WriteLine("La cerveza fue registrada correctamente.");
}

static void ShowBeers(DataContext context)
{
    Console.WriteLine();
    Console.WriteLine("CERVEZAS REGISTRADAS");

    var beers = context.Beers.ToList();

    if (beers.Count == 0)
    {
        Console.WriteLine("No existen cervezas registradas.");
        return;
    }

    foreach (var beer in beers)
    {
        Console.WriteLine(
            $"Id: {beer.Id} | Nombre: {beer.Name} | Tipo: {beer.Type} | " +
            $"Alcohol: {beer.AlcoholPercentage}% | Calificación: {beer.Rating}/10");
    }
}

static void ShowAverage(DataContext context)
{
    Console.WriteLine();
    Console.WriteLine("PROMEDIO DE CALIFICACIONES");

    int beerAmount = context.Beers.Count();

    if (beerAmount == 0)
    {
        Console.WriteLine("No existen cervezas para calcular el promedio.");
        return;
    }

    double average = context.Beers.Average(beer => beer.Rating);

    Console.WriteLine($"El promedio de las calificaciones es: {average:F2}");
}
