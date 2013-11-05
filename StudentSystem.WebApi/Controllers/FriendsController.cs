using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentSystem.Model;

namespace StudentSystem.WebApi.Controllers
{
    public class FriendsController : BaseController
    {
        [HttpGet]
        public ICollection<Friend> GetAll(string sessionKey)
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
                {
                    var user = this.GetUser(sessionKey);
                    var friendList = context.Friends.Where(f => f.UserId == user.Id);
                    return friendList.ToList();
                });
            return response;
        }
    }
}
