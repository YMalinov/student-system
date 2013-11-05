using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class SearchResultModel
    {
        [DataMember(Name="users")]
        public IEnumerable<UserSearchModel> Users { get; set; }
        [DataMember(Name = "courses")]
        public IEnumerable<CourseSearchModel> Courses { get; set; }
    }
}