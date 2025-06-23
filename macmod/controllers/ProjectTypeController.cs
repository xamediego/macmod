using macmod.controllers.dto;
using macmod.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace macmod.controllers;

[Route("api/[controller]")]
public class ProjectTypeController(IProjectTypeService projectTypeService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<ProjectTypeDto[]>> FindAll()
    {
        return Ok(await projectTypeService.FindAllAsync());
    }
    
    [HttpGet("all/complete")]
    public async Task<ActionResult<ProjectTypeDto[]>> FindAllComplete()
    {
        return Ok(await projectTypeService.FindAllCompleteAsync());
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult<ProjectTypeDto>> FindByType(string type)
    {
        var result = await projectTypeService.FindByTypeAsync(type);

        return result != null ? Ok(result) : NotFound();
    }
}