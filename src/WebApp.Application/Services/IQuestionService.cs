using WebApp.Application.Models;
using WebApp.Application.Models.Question;

namespace WebApp.Application.Services
{
    public interface IQuestionService
    {
        Task<Result<int>> Create(QuestionCreateDTO questionModel);
        Task<Result<int>> Update(UpdateQuestionDTO updateQuestionDTO);

    }
}
