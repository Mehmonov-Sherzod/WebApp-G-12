using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WebApp.Application.Models.Subject;

namespace WebApp.Application.Validators.SubjectValidators
{
    public class SubjectCreateValidators : AbstractValidator<CreateSubjectDTO>
    {
        public SubjectCreateValidators()
        {
            RuleFor(s => s.Name)
              .MinimumLength(3).WithMessage("Subject Name should have minimum 3 characters")
              .MaximumLength(20).WithMessage("Subject Name should have maximum 20 characters");
        }

    }
}
