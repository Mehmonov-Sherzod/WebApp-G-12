using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models;
using WebApp.Application.Models.Subject;
using WebApp.Application.Services;

namespace WebApp.API.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpPost]
    public IActionResult PostSubject(CreateSubjectDTO subjectDTO)
    {
        Result<int> response = _subjectService.Create(subjectDTO);

        if (response.IsSuccess)
        {
           return  Ok(response);
        }
        else
        {
            return BadRequest(response.Errors);
        }

            
    }

    [Authorize]
    [HttpPost("get-all")]
    public IActionResult GetAll([FromBody] PaginationOption model)
    {
        Result<PaginationResult<SubjectListResponseModel>> response =
            _subjectService.GetAll(model);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response.Errors);
        }
        //var result = Result<PaginationResult<SubjectListResponseModel>>.Succuss(response);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        Result<SubjectResponseModel> response = _subjectService.GetSubject(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response.Errors);
        }
    }
}
