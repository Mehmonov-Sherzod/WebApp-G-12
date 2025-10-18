using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models.Question;
using WebApp.Application.Services;

namespace WebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController :ControllerBase
    {
        private readonly QuestionService _service;
        public QuestionController(QuestionService questionService)
        {
            _service = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionCreateDTO newQuestion)
        {
            bool checker = await _service.Create(newQuestion);
            return Ok();    
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateQuestionDTO question)
        {
            bool checker = await _service.Update(question);
            return Ok();
        }
    }
}
