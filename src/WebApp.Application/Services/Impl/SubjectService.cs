using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Models;
using WebApp.Application.Models.Subject;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services.Impl;

public class SubjectService : ISubjectService
{
    private readonly AppDbContext _context;
    public SubjectService(AppDbContext appContext)
    {
        _context = appContext;
    }

    public Result<int> Create(CreateSubjectDTO createSubjectDTO)
    {
        var result = new Subject
        {
            Name = createSubjectDTO.Name,
            subjectTranslates = createSubjectDTO.SubjectTranslates
            .Select(x => new SubjectTranslate
            {
                LanguageId = x.LanguageId,
                ColumnName = x.ColumnName,
                TranslateText = x.TranslateText,

            }).ToList()
        };

        _context.Subjects.Add(result);
        _context.SaveChanges();

        var id = result.Id;

        return Result<int>.Succuss(id);

    }

    public Result<PaginationResult<SubjectListResponseModel>> GetAll(PaginationOption model)
    {

        ///validator
        ///if(!validator.IsValid)
        ///{
        //if (true)
        //{
        //    return Result<PaginationResult<SubjectListResponseModel>>.Failure(validator.Errors);
        //}
        ///}
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

        var result = new PaginationResult<SubjectListResponseModel>
        {
            Values = subjects,
            PageSize = model.PageSize,
            PageNumber = model.PageNumber,
            TotalCount = count
        };

        return Result<PaginationResult<SubjectListResponseModel>>.Succuss(result);
    }

    public Result<SubjectResponseModel> GetSubject(int id)
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
            return Result<SubjectResponseModel>.Failure(new List<string> { "Bunday id li fan yo‘q" });
        }


        var result = subject;

        return Result<SubjectResponseModel>.Succuss(result);
    }
}
