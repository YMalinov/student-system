using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name="coursemodel")]
    public class MinimizedCourseModel
    {
        [DataMember(Name="courseid")]
        public int? Id { get; set; }

        [DataMember(Name = "coursetitle")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "startdate")]
        public DateTime? StartDate { get; set; }

        [DataMember(Name = "enddate")]
        public DateTime? EndDate { get; set; }

        [DataMember(Name = "lecturesperweek")]
        public int LecturesPerWeek { get; set; }

        [DataMember(Name = "signupdeadline")]
        public DateTime? SignUpDeadline { get; set; }
    }
}