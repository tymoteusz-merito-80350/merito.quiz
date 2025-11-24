using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MeritoQuiz.Models;
using MeritoQuiz.Shared.DTOs;
using MeritoQuiz.Shared.Models;

namespace MeritoQuiz.Services;

public class QuestionService
{
    private readonly QuizApiOptions _options;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = false,
    };

    private const string CacheFileName = "questions.cache.json";
    private List<Category> _categories = [];

    public QuestionService(QuizApiOptions options)
    {
        _options = options;
    }

    public async ValueTask<IEnumerable<Category>> GetCategories()
    {
        if (_categories.Count == 0)
            await EnsureSynchronized();

        return _categories.AsEnumerable();
    }

    private async Task EnsureSynchronized()
    {
        var cache = await LoadCacheAsync();
        var lastSync = cache?.LastSync ?? DateTime.MinValue;

        List<CategoryDto>? incoming;
        try
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(_options.BaseUrl.TrimEnd('/') + "/");
            
            var request = new QuestionsSyncRequest(lastSync);
            using var resp = await client.PostAsJsonAsync("questions", request, _jsonOptions);

            if (resp.StatusCode == HttpStatusCode.NotModified)
            {
                incoming = null;
            }
            else
            {
                resp.EnsureSuccessStatusCode();
                incoming = await resp.Content.ReadFromJsonAsync<List<CategoryDto>>(_jsonOptions);
            }
        }
        catch
        {
            incoming = null;
        }

        if (incoming is { Count: > 0 })
        {
            var updated = Merge(cache?.Categories ?? [], incoming, lastSync);
            var newCache = new QuestionsCache
            {
                LastSync = DateTime.UtcNow,
                Categories = updated,
            };
            await SaveCacheAsync(newCache);
            cache = newCache;
        }
        else if (cache == null)
        {
            throw new NotImplementedException("No questions available: sync failed and no local cache present.");
        }

        _categories = MapToUi(cache!.Categories);
    }

    private static List<CategoryDto> Merge(List<CategoryDto> existing, List<CategoryDto> incoming, DateTime lastSync)
    {
        var catById = existing.ToDictionary(c => c.Id);

        foreach (var cat in incoming)
        {
            if (!catById.TryGetValue(cat.Id, out var targetCat))
            {
                catById[cat.Id] = new CategoryDto
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Icon = cat.Icon,
                    Questions = cat.Questions.ToList(),
                };
                continue;
            }

            targetCat.Name = cat.Name;
            targetCat.Icon = cat.Icon;

            var qById = targetCat.Questions.ToDictionary(q => q.Id);
            foreach (var q in cat.Questions)
            {
                if (qById.TryGetValue(q.Id, out var existingQuestion))
                {
                    existingQuestion.Text = q.Text;
                    existingQuestion.ModifiedAt = q.ModifiedAt;
                    existingQuestion.CategoryId = q.CategoryId;
                    existingQuestion.Answers = q.Answers
                        .OrderBy(a => a.Order)
                        .Select(a => new AnswerDto
                        {
                            Id = a.Id,
                            QuestionId = a.QuestionId,
                            Order = a.Order,
                            Text = a.Text,
                            IsCorrect = a.IsCorrect,
                        }).ToList();
                }
                else
                {
                    qById[q.Id] = new QuestionDto
                    {
                        Id = q.Id,
                        CategoryId = q.CategoryId,
                        Text = q.Text,
                        ModifiedAt = q.ModifiedAt,
                        Answers = q.Answers.OrderBy(a => a.Order).ToList(),
                    };
                }
            }
            targetCat.Questions = qById.Values.OrderBy(qq => qq.Text).ToList();
        }

        return catById.Values.OrderBy(c => c.Name).ToList();
    }

    private async Task<QuestionsCache?> LoadCacheAsync()
    {
        try
        {
            var path = Path.Combine(GetAppDataDirectory(), CacheFileName);
            if (!File.Exists(path))
                return null;

            await using var fs = File.OpenRead(path);
            var cache = await JsonSerializer.DeserializeAsync<QuestionsCache>(fs, _jsonOptions);
            return cache;
        }
        catch
        {
            return null;
        }
    }

    private async Task SaveCacheAsync(QuestionsCache cache)
    {
        var dir = GetAppDataDirectory();
        var path = Path.Combine(dir, CacheFileName);
        await using var fs = File.Create(path);
        await JsonSerializer.SerializeAsync(fs, cache, _jsonOptions);
    }

    private static List<Category> MapToUi(List<CategoryDto> categories)
    {
        return categories.Select(c => new Category
        {
            Name = c.Name,
            Icon = c.Icon,
            Questions = c.Questions.Select(q => new Question
            {
                Text = q.Text,
                Answers = q.Answers
                    .OrderBy(a => a.Order)
                    .Select(a => new Answer
                    {
                        Order = a.Order,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                    }).ToList(),
            }).ToList(),
        }).ToList();
    }

    private static string GetAppDataDirectory()
    {
#if CORE
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MeritoQuiz");
        Directory.CreateDirectory(dir);
        return dir;
#else
        return Microsoft.Maui.Storage.FileSystem.AppDataDirectory;
#endif
    }
}