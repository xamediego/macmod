using macmod.controllers.dto;
using macmod.services.entities;

namespace macmod.services.mappers;

public static class ProjectMapper
{
    public static Project MapProjectFromDto(ProjectDto dto, ProjectType projectType)
    {
        return new Project
        {
            Title = dto.Title,
            Version = dto.Version,
            Filesize = dto.Filesize,
            FileName = dto.FileName,
            Description = dto.Description,
            PublishedDate = dto.PublishedDate,
            ProjectThumbnail = dto.ProjectThumbnail,
            ProjectTypeId = projectType.Id,

            Links = dto.Links.Select(link => new Link
            {
                Provider = link.Provider,
                Url = link.Url
            }).ToList(),

            Images = dto.Images.Select(img => new Image
            {
                ImageUrl = img
            }).ToList(),
            
            ProjectSubType = dto.ProjectSubType
        };
    }

    public static ProjectDto MapDtoFromProject(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Title = project.Title,
            Version = project.Version,
            Filesize = project.Filesize,
            Description = project.Description,
            PublishedDate = project.PublishedDate,
            ProjectThumbnail = project.ProjectThumbnail,
            FileName = project.FileName,

            Links = project.Links.Select(link => new LinkDto
            {
                Provider = link.Provider,
                Url = link.Url
            }).ToList(),

            Images = project.Images.Select(img => img.ImageUrl).ToList(),
            ProjectSubType = project.ProjectSubType
        };
    }
}