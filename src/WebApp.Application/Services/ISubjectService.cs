using WebApp.Application.Models;
using WebApp.Application.Models.Subject;

namespace WebApp.Application.Services;

public interface ISubjectService
{
    Result<int> Create(CreateSubjectDTO createSubjectDTO);
    Result<PaginationResult<SubjectListResponseModel>> GetAll(PaginationOption model);
    Result<SubjectResponseModel> GetSubject(int id);
}
