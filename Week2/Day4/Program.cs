var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


// --------------------
// Minimal API
// --------------------

List<string> items = new()
{
    "Laptop",
    "Mouse",
    "Keyboard"
};

app.MapGet("/items", () =>
{
    return items;
});

app.MapGet("/items/{id}", (int id) =>
{
    if (id < 0 || id >= items.Count)
    {
        return Results.NotFound();
    }

    return Results.Ok(items[id]);
});

app.Run();