using System;
using System.Runtime.Serialization;
using StudentSystem.Model;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class RegisterUserModel
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "authcode")]
        public string AuthCode { get; set; }

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
        
        //public RegisterUserModel()
        //{
        //    this.Gender = Model.Gender.Unknown;
        //}
    }
}