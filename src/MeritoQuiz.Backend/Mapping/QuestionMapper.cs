using MeritoQuiz.Backend.DTOs;
using MeritoQuiz.Backend.Entities;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Mapping;

public static class QuestionMapper
{
    public static QuestionDto ToDTO(Question entity)
    {
        return new QuestionDto
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            Text = entity.Text,
            ModifiedAt = entity.ModifiedAt,
            Answers = (entity.Answers ?? []).Select(AnswerMapper.ToDTO).ToList(),
        };
    }

    public static Question ToEntity(QuestionDto dto)
    {
        return new Question
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            Text = dto.Text,
            ModifiedAt = dto.ModifiedAt,
            Answers = (dto.Answers ?? []).Select(AnswerMapper.ToEntity).ToList(),
        };
    }
}