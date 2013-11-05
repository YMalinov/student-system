using System.Collections.Generic;

namespace StudentSystem.Model
{
    public class Friend
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public Friend()
        {
            this.Friends = new HashSet<User>();
        }
    }
}
