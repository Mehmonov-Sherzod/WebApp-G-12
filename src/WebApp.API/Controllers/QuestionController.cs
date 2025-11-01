using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Models;
using WebApp.Application.Models.Question;
using WebApp.Application.Services.Impl;

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
            Result<int> response = await _service.Create(newQuestion);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateQuestionDTO question)
        {
            Result<int> response = await _service.Update(question);

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
}
