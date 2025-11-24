using MeritoQuiz.Backend.DTOs;
using MeritoQuiz.Backend.Entities;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Mapping;

public static class AnswerMapper
{
    public static AnswerDto ToDTO(Answer entity)
    {
        return new AnswerDto
        {
            Id = entity.Id,
            QuestionId = entity.QuestionId,
            Order = entity.Order,
            Text = entity.Text,
            IsCorrect = entity.IsCorrect,
        };
    }

    public static Answer ToEntity(AnswerDto dto)
    {
        return new Answer
        {
            Id = dto.Id,
            QuestionId = dto.QuestionId,
            Order = dto.Order,
            Text = dto.Text,
            IsCorrect = dto.IsCorrect,
        };
    }
}