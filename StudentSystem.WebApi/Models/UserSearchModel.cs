using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class UserSearchModel
    {
        [DataMember(Name = "userId")]
        public int Id { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
        [DataMember(Name = "pictureFileName")]   
        public string PictureFileName { get; set; }
    }
}