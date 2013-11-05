using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentSystem.Data;
using StudentSystem.Model;

namespace StudentSystem.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        protected StudentSystemContext context;
        private const string InvalidSessionKeyErroMessage = "Invalid sessionKey. Login again.";

        public BaseController()
        {
            this.context = new StudentSystemContext();
        }

        protected User GetUser(string sessionKey)
        {
            var user = this.context.Users.Where(u => u.SessionKey == sessionKey).FirstOrDefault();
            if (user == null)
            {
                throw new InvalidOperationException(InvalidSessionKeyErroMessage);
            }

            user.LastVisit = DateTime.Now;
            context.SaveChanges();

            return user;
        }

        protected HttpResponseMessage PerformOperation(Action action)
        {
            try
            {
                action();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (InvalidOperationException ex)
            {
                // a simple and ugly way to return slightly more meaningfull error codes
                if (ex is InvalidOperationException && ex.Message == InvalidSessionKeyErroMessage)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
                }

                throw ex;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        protected HttpResponseMessage PerformOperation<T>(Func<T> action)
        {
            try
            {
                var result = action();
                if (result is HttpResponseMessage)
                {
                    return result as HttpResponseMessage;
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (InvalidOperationException ex)
            {
                // a simple and ugly way to return slightly more meaningfull error codes
                if (ex is InvalidOperationException && ex.Message == InvalidSessionKeyErroMessage)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
                }

                throw ex;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        protected T PerformOperationAndHandleExceptions<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }
    }
}
