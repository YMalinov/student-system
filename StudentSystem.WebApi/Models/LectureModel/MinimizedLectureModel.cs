using StudentSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name = "lecture")]
    public class MinimizedLectureModel
    {
        [DataMember(Name = "lectureid")]
        public int Id { get; set; }

        [DataMember(Name = "lecturename")]
        public string Name { get; set; }
    }
}