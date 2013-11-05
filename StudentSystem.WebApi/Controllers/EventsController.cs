using StudentSystem.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentSystem.Model;

namespace StudentSystem.WebApi.Controllers
{
    public class EventsController : BaseController
    {
        // api/courses
        [HttpGet]
        public IQueryable<EventModel> GetAll()
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
            {
                var models =
                    (from ev in this.context.Events
                     select new EventModel()
                     {
                         Name = ev.Name,
                         StartDate = ev.StartDate,
                         Description = ev.Description,
                         DateCreated = ev.DateCreated,
                         Author = new EventUserModel()
                         {
                             Id = ev.Author.Id,
                             Nickname = ev.Author.Nickname
                         },
                         Attendants =
                         from attend in ev.Attendants
                         select new EventUserModel() 
                         { 
                            Id = attend.Id,
                            Nickname = attend.Nickname
                         }
                     });

                return models;
            });

            return response;
        }

        // api/courses
        [HttpPost]
        public HttpResponseMessage PostEvent([FromBody]EventModel model, string sessionKey)
        {
            var response = this.PerformOperationAndHandleExceptions(() => 
            {
                var user = this.GetUser(sessionKey);

                Event newEvent = new Event()
                {
                    Name = model.Name,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    DateCreated = DateTime.Now,
                    Author = user,
                    Attendants = new HashSet<User>()
                };

                this.context.Events.Add(newEvent);
                this.context.SaveChanges();

                return this.Request.CreateResponse(HttpStatusCode.Created);
            });

            return response;
        }
    }
}
