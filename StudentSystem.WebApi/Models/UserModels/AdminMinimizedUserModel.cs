using StudentSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class AdminMinimizedUserModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
        
        [DataMember(Name = "usertype")]
        public UserType UserType { get; set; }

        [DataMember(Name = "isactive")]
        public bool IsActive { get; set; }
    }
}