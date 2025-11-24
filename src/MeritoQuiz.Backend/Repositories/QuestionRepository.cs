using Microsoft.EntityFrameworkCore;
using MeritoQuiz.Backend.Data;
using MeritoQuiz.Backend.Entities;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Repositories;

public class QuestionRepository(MeritoQuiz.Backend.Data.MeritoQuizDbContext db) : IQuestionRepository
{
    public async Task<List<Category>> GetCategoriesWithModifiedQuestionsAsync(DateTime lastSync, CancellationToken ct = default)
    {
        // Fetch categories that have at least one question modified after lastSync
        var query = db.Categories
            .AsNoTracking()
            .Where(c => c.Questions.Any(q => q.ModifiedAt > lastSync))
            .Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Icon = c.Icon,
                Questions = c.Questions
                    .Where(q => q.ModifiedAt > lastSync)
                    .OrderBy(q => q.Text)
                    .Select(q => new Question
                    {
                        Id = q.Id,
                        CategoryId = q.CategoryId,
                        Text = q.Text,
                        ModifiedAt = q.ModifiedAt,
                        Answers = q.Answers
                            .OrderBy(a => a.Order)
                            .Select(a => new Answer
                            {
                                Id = a.Id,
                                QuestionId = a.QuestionId,
                                Order = a.Order,
                                Text = a.Text,
                                IsCorrect = a.IsCorrect,
                            }).ToList(),
                    }).ToList(),
            });

        return await query.ToListAsync(ct);
    }
}