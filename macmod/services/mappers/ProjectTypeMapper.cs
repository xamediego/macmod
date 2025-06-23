using macmod.controllers.dto;
using macmod.services.entities;

namespace macmod.services.mappers;

public static class ProjectTypeMapper
{
    public static ProjectType MapProjectTypeFromDto(ProjectTypeDto dto)
    {
        return new ProjectType
        {
            Type = dto.Type,
            Title = dto.Title,
            Thumbnail = dto.Thumbnail,
            Icon = dto.Icon
        };
    }

    public static ProjectTypeDto MapDtoFromProjectType(ProjectType projectType)
    {
        var projects = projectType.Projects.Select(ProjectMapper.MapDtoFromProject).ToList();

        return new ProjectTypeDto
        {
            Type = projectType.Type,
            Title = projectType.Title,
            Thumbnail = projectType.Thumbnail,
            Icon = projectType.Icon,
            Projects = projects
        };
    }
}