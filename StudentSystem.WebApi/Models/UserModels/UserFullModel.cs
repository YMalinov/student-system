
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using StudentSystem.Model;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class UserFullModel
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "userType")]
        public UserType UserType { get; set; }

        [DataMember(Name = "website")]
        public string WebSite { get; set; }

        [DataMember(Name = "gender")]
        public Gender Gender { get; set; }

        [DataMember(Name = "hometown")]
        public string Hometown { get; set; }

        [DataMember(Name = "birthday")]
        public DateTime? Birthday { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "occupation")]
        public string Occupation { get; set; }

        [DataMember(Name = "aboutme")]
        public string AboutMe { get; set; }

        [DataMember(Name = "studentNumber")]
        public int StudentNumber { get; set; }

        [DataMember(Name = "registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [DataMember(Name = "lastVisit")]
        public DateTime LastVisit { get; set; }

        [DataMember(Name = "courses")]
        public IEnumerable<CourseUserModel> Courses { get; set; }
    }
}