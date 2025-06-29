using macmod.controllers.dto;
using macmod.services.entities;
using macmod.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace macmod.controllers;

[Route("api/[controller]")]
public class ProjectController(IProjectService projectService)
    : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<ProjectDto[]>> FindAll()
    {
        return Ok(await projectService.FindAllAsync());
    }

    [HttpGet("title/{title}")]
    public async Task<ActionResult<ProjectDto>> FindByName(string title)
    {
        var result = await projectService.FindByTitleAsync(title);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("download/{title}/{projectId}")]
    public async Task<ActionResult<Stream?>> Download(string title, long projectId)
    {
        try
        {
            DownloadResult? downloadResult = await projectService.DownloadAsync(projectId);

            return 
                downloadResult == null ? 
                NotFound() : 
                File(downloadResult.FileStream, downloadResult.ContentType, downloadResult.FileName, true);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error while downloading: {ex.Message}");
        }
    }

    [HttpGet("projectType/{type}")]
    public async Task<ActionResult<ProjectDto[]>> FindAll(string type)
    {
        return Ok(await projectService.FindByProjectTypeAsync(type));
    }
}