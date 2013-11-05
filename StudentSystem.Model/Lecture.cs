using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? HomeworkDeadline { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual Course Course { get; set; }
    }
}
