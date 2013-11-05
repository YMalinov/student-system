using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StudentSystem.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "UserIDApi",
                routeTemplate: "api/users/getuser/{username}",
                defaults: new
                {
                    controller = "users",
                    action = "GetUser"
                });

            config.Routes.MapHttpRoute(
                name: "AdminApi",
                routeTemplate: "api/admin/{action}/{id}",
                defaults: new
                {
                    controller = "admin",
                    id = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/users/{action}/{id}",
                defaults: new
                {
                    controller = "users",
                    id = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: "CourseApi",
                routeTemplate: "api/courses/{id}",
                defaults: new 
                { 
                    controller = "courses", 
                    id = RouteParameter.Optional 
                });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
