using StudentSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name = "coursemodel")]
    public class DetailedCourseModelWithHomeworks : MinimizedCourseModel
    {
        [DataMember(Name = "lectures")]
        public IEnumerable<LectureModel> Lectures { get; set; }
    }
}