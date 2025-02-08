using Microsoft.EntityFrameworkCore;
using TodoApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// חיבור ל-DbContext

Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("ToDoListDB")}");

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ToDoListDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ToDoListDB"))
    ));

// הוספת שירותים
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


app.UseCors();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();




app.MapGet("/items", async (ToDoDbContext context) =>
{
    var items = await context.Items.ToListAsync();
    return Results.Ok(items);
});


app.MapPost("/items", async (ToDoDbContext context, Item newItem) =>
{
    await context.Items.AddAsync(newItem);
    await context.SaveChangesAsync();
    return Results.Ok(newItem);
});

app.MapPut("/items/{id}", async (ToDoDbContext context, int id, Item updatedItem) =>
{
    var item = await context.Items.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound($"Item with ID {id} not found.");
    }
    item.Name = updatedItem.Name;
    item.IsComplete = updatedItem.IsComplete;
    //saving the item
    await context.SaveChangesAsync();
    return Results.Ok(item);

});

app.MapDelete("/items/{id}", async (ToDoDbContext context, int id) =>
{
    var item = await context.Items.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound($"Item with ID {id} not found.");
    }
    context.Items.Remove(item);
    await context.SaveChangesAsync();
    return Results.Ok(item);
});
app.MapGet("/", () => "Todo List is running..");
app.Run();