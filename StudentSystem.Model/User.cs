using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public UserType UserType { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        public int StudentNumber { get; set; }

        public string WebSite { get; set; }

        public string PictureFileName { get; set; }

        public Gender Gender { get; set; }

        public string Hometown { get; set; }

        public DateTime? Birthday { get; set; }

        public string Email { get; set; }

        public string Occupation { get; set; }

        public string AboutMe { get; set; }

        public DateTime LastVisit { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }

        public User()
        {
            this.Courses = new HashSet<Course>();
            this.Marks = new HashSet<Mark>();
            this.Homeworks = new HashSet<Homework>();
        }
    }
}
