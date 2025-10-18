using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Models.Answer;
using WebApp.Domain.Enums;

namespace WebApp.Application.Models.Question
{
    public class QuestionGetDTO
    {
        public string Title { get; set; }
        public QuestionType Type { get; set; }
        public ICollection<AnswerCreateDTO> Answers { get; set; }
    }
}
