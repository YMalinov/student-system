using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class CourseSearchModel
    {
        [DataMember(Name="courseId")]
        public int Id { get; set; }
        [DataMember(Name = "courseName")]
        public string Name { get; set; }
    }
}