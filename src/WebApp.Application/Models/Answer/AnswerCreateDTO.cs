using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Models.Answer
{
    public class AnswerCreateDTO
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
