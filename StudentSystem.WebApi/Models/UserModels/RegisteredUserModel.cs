using StudentSystem.Model;
using System.Runtime.Serialization;

namespace StudentSystem.WebApi.Models
{
    [DataContract]
    public class RegisteredUserModel
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }

        [DataMember(Name = "userType")]
        public UserType UserType { get; set; }
    }
}