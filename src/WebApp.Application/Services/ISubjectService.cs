using WebApp.Application.Models;
using WebApp.Application.Models.Subject;

namespace WebApp.Application.Services;

public interface ISubjectService
{
    int Create(CreateSubjectDTO createSubjectDTO);
    PaginationResult<SubjectListResponseModel> GetAll(PaginationOption model);
    SubjectResponseModel GetSubject(int id);
}
