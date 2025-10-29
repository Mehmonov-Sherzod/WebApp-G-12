using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Models.Answer;
using WebApp.Application.Models.Question;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services.Impl
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;
        public QuestionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(QuestionCreateDTO questionModel)
        {
            Question newQuestion = new Question
            {
                Type = questionModel.Type,
                Title = questionModel.Title,
                SubjectId = questionModel.SubjectId,
                Answers = questionModel.Answers
                                        .Select(a => new Answer
                                        {
                                            IsCorrect = a.IsCorrect,
                                            Text = a.Text
                                        })
                                        .ToList()
            };
            await _context.Questions.AddAsync(newQuestion);
            await _context.SaveChangesAsync();
            return true;
            //Question newQuestion = new Question
            //{
            //    Type = questionModel.Type,
            //    Title = questionModel.Title,
            //    Answers = new List<Answer>()
            //};

            //// Map answers with a for loop
            //for (int i = 0; i < questionModel.Answers.Count; i++)
            //{
            //    var a = questionModel.Answers[i];
            //    var answer = new Answer
            //    {
            //        IsCorrect = a.IsCorrect,
            //        Text = a.Text
            //    };

            //    newQuestion.Answers.Add(answer);
            //}
        }
        public async Task<bool> Update(UpdateQuestionDTO updateQuestionDTO) 
        {
            var storage = await _context.Questions
                                .Where(x=>x.Id==updateQuestionDTO.Id)
                                .Include(x=>x.Answers)
                                .FirstAsync();
            //TODO
            //Storage should be mapped with updateQustionDTO
            storage.SubjectId = updateQuestionDTO.SubjectId;
            storage.Title = updateQuestionDTO.Title;
            storage.Answers.Clear();
            foreach(var item in updateQuestionDTO.Answers)
            {
                Answer newAnswer = new Answer
                {
                    Id = item.Id,
                
                    Text = item.Text,
                    IsCorrect = item.IsCorrect
                };
                storage.Answers.Add(newAnswer);
            }
            _context.Questions.Update(storage);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
