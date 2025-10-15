using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models.Subject;
using WebApp.Application.Services;

namespace WebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
