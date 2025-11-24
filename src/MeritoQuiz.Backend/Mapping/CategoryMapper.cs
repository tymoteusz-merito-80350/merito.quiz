using MeritoQuiz.Backend.DTOs;
using MeritoQuiz.Backend.Entities;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Mapping;

public static class CategoryMapper
{
    public static CategoryDto ToDTO(Category entity)
    {
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Icon = entity.Icon,
            Questions = (entity.Questions ?? []).Select(QuestionMapper.ToDTO).ToList(),
        };
    }

    public static Category ToEntity(CategoryDto dto)
    {
        return new Category
        {
            Id = dto.Id,
            Name = dto.Name,
            Icon = dto.Icon,
            Questions = (dto.Questions ?? []).Select(QuestionMapper.ToEntity).ToList(),
        };
    }
}