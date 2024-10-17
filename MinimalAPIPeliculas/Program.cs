using MinimalAPIPeliculas.Entidades;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var peliculas = new List<Pelicula>
{
    new() { Id = 1, Titulo = "Inception", Director = "Christopher Nolan" },
    new() { Id = 2, Titulo = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski" }
};

app.MapGet("/peliculas", () => Results.Ok(peliculas));

app.MapGet("/peliculas/{id}", (int id) =>
{
    var pelicula = peliculas.FirstOrDefault(p => p.Id == id);
    return pelicula != null ? Results.Ok(pelicula) : Results.NotFound();
});

app.MapPost("/peliculas", (Pelicula nuevaPelicula) =>
{
    peliculas.Add(nuevaPelicula);
    return Results.Created($"/peliculas/{nuevaPelicula.Id}", nuevaPelicula);
});

app.MapPut("/peliculas/{id}", (int id, Pelicula peliculaActualizada) =>
{
    var index = peliculas.FindIndex(p => p.Id == id);
    if (index == -1)
    {
        return Results.NotFound();
    }

    peliculas[index] = peliculaActualizada;
    return Results.NoContent();
});

app.MapDelete("/peliculas/{id}", (int id) =>
{
    var index = peliculas.FindIndex(p => p.Id == id);
    if (index == -1)
    {
        return Results.NotFound();
    }

    peliculas.RemoveAt(index);
    return Results.NoContent();
});

app.MapGet("/generos", () =>
{
    var generos = new List<Genero>
    {
        new() { Id = 1, Nombre = "Acción" },
        new() { Id = 2, Nombre = "Ciencia Ficción" }
    };

    return Results.Ok(generos);
});





app.Run();


// Define la clase Pelicula
public class Pelicula
{
    public int Id { get; set; }
    public string? Titulo { get; set; } // Permitir valores nulos
    public string? Director { get; set; } // Permitir valores nulos
}


