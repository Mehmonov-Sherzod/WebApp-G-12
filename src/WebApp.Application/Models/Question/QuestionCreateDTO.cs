using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Models.Answer;
using WebApp.Domain.Enums;

namespace WebApp.Application.Models.Question
{
    public class QuestionCreateDTO
    {
        public string Title { get; set; }
        public QuestionType Type { get; set; }
        public int SubjectId { get; set; }
        public List<AnswerCreateDTO> Answers { get; set; }
    }
}
