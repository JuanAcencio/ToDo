using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ToDo.Api_.Data;
using ToDo.Api_.DTOs;
using ToDo.Api_.Endpoints;
using ToDo.Api_.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Agrega el servicio para la conexion a la base de datos SQL Server 
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(cfg => 
{
    cfg.CreateMap<CreateToDoItemDto, ToDoItem>();
    cfg.CreateMap<ToDoItem, ToDoItemDto>();
    cfg.CreateMap<UpdateToDoItemDto, ToDoItem>();

});



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapToDoItemsEndpoint();


//var toDoItemsEndpints = app.MapGroup("/pendientes").WithTags("Pendientes");


//toDoItemsEndpints.MapGet("/",
//    async (AppDbContext dbContext) =>
//    {
//        var toDoItems = await dbContext.ToDoItems.ToListAsync();
//        return Results.Ok(toDoItems);
//    });

//toDoItemsEndpints.MapGet("/{id:int}",
//    async (AppDbContext  dbContext, int id) => 
//    {
//        var toDoItem = await dbContext.ToDoItems.FirstOrDefaultAsync(tdi => tdi.Id == id);
//        if (toDoItem is null) 
//        {
//            return Results.NotFound();
//        }
//        return Results.Ok(toDoItem);
//    });
//toDoItemsEndpints.MapPost("/",
//    async(AppDbContext dbContext, ToDoItem toDoItem) =>
//    {
//        toDoItem.IsCompleted = false;
//        await dbContext.ToDoItems.AddAsync(toDoItem);
//        await dbContext.SaveChangesAsync();
//        return Results.Created($"/api/pendientes/{toDoItem.Id}",toDoItem);
//    }
//    );

//toDoItemsEndpints.MapPut("/{id:int}",
//      async (AppDbContext dbContext,int id, ToDoItem item) =>
//      {
//          var toDoItem = await dbContext.ToDoItems.FirstOrDefaultAsync(tdi => tdi.Id == item.Id);
//          if (toDoItem is null)
//          {
//              return Results.NotFound();
//          }
//          dbContext.Attach(item);
//          dbContext.ToDoItems.Entry(item).State = EntityState.Modified;
//          await dbContext.SaveChangesAsync();
//          return Results.NoContent();
//      });

//toDoItemsEndpints.MapDelete("/a{id}",
//      async (AppDbContext dbContext, int id) =>
//      {
//          var toDoItem = await dbContext.ToDoItems.FirstOrDefaultAsync(tdi => tdi.Id == id);
//          if (toDoItem is null)
//          {
//              return Results.NotFound();
//          }
//          dbContext.ToDoItems.Remove(toDoItem);
//          await dbContext.SaveChangesAsync();
//          return Results.NoContent();
//      });

app.Run();
