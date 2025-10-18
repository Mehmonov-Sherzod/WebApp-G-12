using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Models.Answer;
using WebApp.Domain.Enums;

namespace WebApp.Application.Models.Question
{
    public class UpdateQuestionDTO
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }
        public List<UpdateAnswers> Answers { get; set; }
    }
}
