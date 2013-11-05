using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name="coursemodel")]
    public class DetailedCourseModel : MinimizedCourseModel
    {
        [DataMember(Name = "lectures")]
        public IEnumerable<MinimizedLectureModel> Lectures { get; set; }
    }
}