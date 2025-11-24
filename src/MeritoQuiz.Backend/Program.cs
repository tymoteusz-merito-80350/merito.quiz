using Microsoft.EntityFrameworkCore;
using MeritoQuiz.Backend.Data;
using MeritoQuiz.Backend.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// JSON options - avoid cycles
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var connString = builder.Configuration.GetConnectionString("Default")
                 ?? "Host=localhost;Username=postgres;Password=postgres;Database=MeritoQuiz";

builder.Services.AddDbContext<MeritoQuizDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MeritoQuizDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapPost("/questions", async (MeritoQuiz.Backend.Models.QuestionsSyncRequest req, IQuestionRepository repo, CancellationToken ct) =>
{
    var categories = await repo.GetCategoriesWithModifiedQuestionsAsync(req.LastSync, ct);
    
    if (categories.Count == 0)
        return Results.StatusCode(StatusCodes.Status304NotModified);
    
    var dto = categories.Select(MeritoQuiz.Backend.Mapping.CategoryMapper.ToDTO).ToList();
    return Results.Ok(dto);
});

app.Run();