using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class AdminMinimizedCourseModel : MinimizedCourseModel
    {
        [DataMember(Name="isVisible")]
        public bool IsVisible { get; set; }
    }
}