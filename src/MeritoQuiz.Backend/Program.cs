using Microsoft.EntityFrameworkCore;
using MeritoQuiz.Backend.Data;
using MeritoQuiz.Backend.Repositories;
using System.Text.Json.Serialization;
using MeritoQuiz.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MeritoQuizDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapPost("/questions", async (QuestionsSyncRequest req, IQuestionRepository repo, CancellationToken ct) =>
{
    var categories = await repo.GetCategoriesWithModifiedQuestionsAsync(req.LastSync, ct);
    
    if (categories.Count == 0)
        return Results.StatusCode(StatusCodes.Status304NotModified);
    
    var dto = categories.Select(MeritoQuiz.Backend.Mapping.CategoryMapper.ToDTO).ToList();
    return Results.Ok(dto);
});

app.Run();