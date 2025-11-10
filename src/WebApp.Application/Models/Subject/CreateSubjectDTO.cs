namespace WebApp.Application.Models.Subject;

public class CreateSubjectDTO
{
    public string Name { get; set; }

   public List<SubjectTranslateModel> SubjectTranslates { get; set; } 
}
