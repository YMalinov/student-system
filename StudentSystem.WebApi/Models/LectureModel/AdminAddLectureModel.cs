using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    public class AdminAddLectureModel
    {
        [DataMember(Name = "lecturename")]
        public string Name { get; set; }

        [DataMember(Name = "homeworkdeadline")]
        public DateTime? HomeworkDeadline { get; set; }
    }
}