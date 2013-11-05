using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name = "homework")]
    public class HomeworkModel
    {
        [DataMember(Name = "homeworkid")]
        public int Id { get; set; }

        [DataMember(Name = "filename")]
        public string FileName { get; set; }
    }
}