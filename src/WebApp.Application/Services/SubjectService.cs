using Microsoft.EntityFrameworkCore;
using WebApp.Application.Models;
using WebApp.Application.Models.Subject;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services
{
    public class SubjectService
    {
        private readonly AppDbContext _context;
        private const int a = 5;
        public SubjectService(AppDbContext appContext)
        {
            _context = appContext;
        }

        public int Create(CreateSubjectDTO createSubjectDTO)
        {
            var result = new Subject
            {
                Name = createSubjectDTO.Name,
            };

            _context.Subjects.Add(result);
            _context.SaveChanges();

            return result.Id;
        }

        public PaginationResult<SubjectListResponseModel> GetAll(SubjectPageModel model)
        {
            var query = _context.Subjects.AsQueryable();

            if (!string.IsNullOrEmpty(model.Search))
            {
                query = query.Where(s => s.Name.Contains(model.Search));
            }
            Console.WriteLine(query.ToQueryString());
            List<SubjectListResponseModel> subjects = query 
                .Skip(model.PageSize * (model.PageNumber - 1))
                .Take(model.PageSize)
                .Select(s => new SubjectListResponseModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();

            int count = _context.Subjects.Count();

            return new PaginationResult<SubjectListResponseModel>
            {
                Values = subjects,
                PageSize = model.PageSize,
                PageNumber = model.PageNumber,
                TotalCount = count
            };
        }

        public SubjectResponseModel GetSubject(int id)
        {
            SubjectResponseModel? subject = _context.Subjects
                .Where(s => s.Id == id)
                .Select(s => new SubjectResponseModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .FirstOrDefault();

            if (subject == null)
            {
                throw new NotImplementedException();
            }

            return subject;
        }
    }
}
