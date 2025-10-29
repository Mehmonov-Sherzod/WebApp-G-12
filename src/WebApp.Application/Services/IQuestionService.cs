using WebApp.Application.Models.Question;

namespace WebApp.Application.Services
{
    public interface IQuestionService
    {
        Task<bool> Create(QuestionCreateDTO questionModel);
        Task<bool> Update(UpdateQuestionDTO updateQuestionDTO);
    }
}
