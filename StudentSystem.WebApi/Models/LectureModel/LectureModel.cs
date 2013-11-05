using StudentSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name = "lecture")]
    public class LectureModel : MinimizedLectureModel
    {
        [DataMember(Name = "homeworkdeadline")]
        public DateTime? HomeworkDeadline { get; set; }

        [DataMember(Name = "homeworks")]
        public IEnumerable<HomeworkModel> Homeworks { get; set; }
    }
}