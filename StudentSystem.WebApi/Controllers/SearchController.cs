using StudentSystem.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentSystem.WebApi.Controllers
{
    public class SearchController : BaseController
    {
        // api/search?q={query}
        [HttpGet]
        public HttpResponseMessage Search(string q)
        {
            var responseMsg = PerformOperation(() =>
            {
                var result = new SearchResultModel
                {
                    Courses = from c in context.Courses
                              where c.Name.Contains(q)
                              select new CourseSearchModel
                              {
                                  Id = c.Id,
                                  Name = c.Name
                              },
                    Users = from u in context.Users
                            where u.Username.Contains(q) || u.Nickname.Contains(q)
                            select new UserSearchModel
                            {
                                 Id = u.Id,
                                 Username = u.Username,
                                 Nickname = u.Nickname,
                                 PictureFileName = u.PictureFileName
                            }

                };
                return result;
            });

            return responseMsg;
        }
    }
}
