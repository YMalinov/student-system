using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LecturesPerWeek { get; set; }

        public DateTime SignUpDeadline { get; set; }

        public string Description { get; set; }

        public bool IsVisible { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<User> Students { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }

        //public virtual ICollection<User> Lectors { get; set; }

        public Course()
        {
            this.Lectures = new HashSet<Lecture>();
            this.Students = new HashSet<User>();
            this.Marks = new HashSet<Mark>();
            //this.Lectors = new HashSet<User>();
        }
    }
}
