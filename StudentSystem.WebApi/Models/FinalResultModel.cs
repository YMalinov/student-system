using StudentSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class FinalResultModel
    {
        [DataMember(Name="status")]
        public CourseFinalResultType Status { get; set; }

        [DataMember(Name = "score")]
        public decimal Score { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }
    }
}