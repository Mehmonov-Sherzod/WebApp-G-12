using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models;
using WebApp.Application.Models.Subject;
using WebApp.Application.Services;

namespace WebApp.API.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectController : ControllerBase
{
    private readonly SubjectService _subjectService;
    public SubjectController(SubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpPost]
    public IActionResult PostSubject(CreateSubjectDTO subjectDTO)
    {
        var id = _subjectService.Create(subjectDTO);

        return Ok(id);
    }

    [HttpPost("get-all")]
    public IActionResult GetAll([FromBody] PaginationOption model)
    {
        var result = _subjectService.GetAll(model);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var result = _subjectService.GetSubject(id);

        return Ok(result);
    }
}
