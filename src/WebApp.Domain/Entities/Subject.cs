using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
