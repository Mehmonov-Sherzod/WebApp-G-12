using System.Diagnostics.CodeAnalysis;

namespace WebApp.Application.Models;

public class PaginationOption
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
}