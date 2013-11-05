using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class Homework
    {
        public int Id { get; set; }

        public virtual Lecture Lecture { get; set; }

        public virtual User Author { get; set; }

        public string FileName { get; set; }
    }
}
