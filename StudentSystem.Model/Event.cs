using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime StartDate { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<User> Attendants { get; set; }

        public Event()
        {
            this.Attendants = new HashSet<User>();
        }
    }
}
