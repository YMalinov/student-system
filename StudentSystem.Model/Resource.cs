using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class Resource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public ResourceType Type { get; set; }
    }
}
