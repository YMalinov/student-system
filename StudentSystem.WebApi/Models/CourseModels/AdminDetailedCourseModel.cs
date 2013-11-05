using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    public class AdminDetailedCourseModel : DetailedCourseModel
    {
        public bool IsVisible { get; set; }
    }
}