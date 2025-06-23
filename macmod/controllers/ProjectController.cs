using macmod.controllers.dto;
using macmod.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace macmod.controllers;

[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
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
    
    [HttpGet("projectType/{type}")]
    public async Task<ActionResult<ProjectDto[]>> FindAll(string type)
    {
        return Ok(await projectService.FindByProjectTypeAsync(type));
    }
}