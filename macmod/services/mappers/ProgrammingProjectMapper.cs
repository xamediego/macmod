using macmod.controllers.dto;
using macmod.services.entities;

namespace macmod.services.mappers;

public static class ProgrammingProjectMapper
{
    public static ProgrammingProject MapGameMapFromDto(ProgrammingProjectDto dto, Project project)
    {
        return new ProgrammingProject
        {
            Purpose = dto.Purpose,
            Project = project
        };
    }
}