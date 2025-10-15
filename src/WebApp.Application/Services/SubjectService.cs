using WebApp.Application.Models.Subject;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services
{
    public class SubjectService
    {
        private readonly AppDbContext appContext;
        private const int a = 5;
        public SubjectService(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public int Create(CreateSubjectDTO createSubjectDTO)
        {
            var result = new Subject
            {
                Name = createSubjectDTO.Name,
            };

            appContext.Subjects.Add(result);
            appContext.SaveChanges();

            return result.Id;
        }
       
    }
}
