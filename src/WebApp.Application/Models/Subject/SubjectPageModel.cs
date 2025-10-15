using System.Diagnostics.CodeAnalysis;

namespace WebApp.Application.Models.Subject;

public class SubjectPageModel
{
    [NotNull]
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string Search { get; set; }
}

