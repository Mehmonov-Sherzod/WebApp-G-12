using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Enums;

namespace WebApp.Application.Models.Subject
{
    public class SubjectTranslateModel
    {

        public Language LanguageId { get; set; }

        public string ColumnName { get; set; }

        public string TranslateText { get; set; }
    }
}
