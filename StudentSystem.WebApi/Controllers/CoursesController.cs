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
    public class CoursesController : BaseController
    {
        //api/courses
        [HttpGet]
        public HttpResponseMessage GetAllCourses()
        {
            var responseMsg = PerformOperation(() =>
            {
                var courses = from course in context.Courses
                              where course.IsVisible
                              select new MinimizedCourseModel()
                              {
                                  Id = course.Id,
                                  Title = course.Name,
                                  Description = course.Description,
                                  StartDate = course.StartDate,
                                  EndDate = course.EndDate,
                                  LecturesPerWeek = course.LecturesPerWeek,
                                  SignUpDeadline = course.SignUpDeadline
                              };

                var response = this.Request.CreateResponse(HttpStatusCode.OK, courses);

                return response;

            });

            return responseMsg;
        }

        //api/courses/{id}
        [HttpGet]
        public HttpResponseMessage GetByCourseId(int id)
        {
            var responseMsg = PerformOperation(() =>
            {
                var detailedCourse = (from course in context.Courses
                                      where course.Id == id && course.IsVisible
                                      select new DetailedCourseModel()
                                      {
                                          Id = course.Id,
                                          Title = course.Name,
                                          Description = course.Description,
                                          StartDate = course.StartDate,
                                          EndDate = course.EndDate,
                                          LecturesPerWeek = course.LecturesPerWeek,
                                          SignUpDeadline = course.SignUpDeadline,
                                          Lectures = (from lecture in course.Lectures
                                                      select new MinimizedLectureModel()
                                                      {
                                                          Id = lecture.Id,
                                                          Name = lecture.Name
                                                      })
                                      }).FirstOrDefault();

                if (detailedCourse == null)
                {
                    throw new ArgumentNullException("Course not found!");
                }

                var response = this.Request.CreateResponse(HttpStatusCode.OK, detailedCourse);

                return response;

            });

            return responseMsg;
        }

        //api/courses/{id}?sessionKey={sessionKey}
        [HttpGet]
        public HttpResponseMessage GetByCourseIdAndSessionKey(int id, string sessionKey)
        {
            var responseMsg = PerformOperation(() =>
            {
                var user = this.GetUser(sessionKey);

                var detailedCourse = (from course in context.Courses
                                      where course.Id == id && course.IsVisible
                                      select new DetailedCourseModelWithHomeworks()
                                      {
                                          Id = course.Id,
                                          Title = course.Name,
                                          Description = course.Description,
                                          StartDate = course.StartDate,
                                          EndDate = course.EndDate,
                                          LecturesPerWeek = course.LecturesPerWeek,
                                          SignUpDeadline = course.SignUpDeadline,
                                          Lectures = (from lecture in course.Lectures
                                                      select new LectureModel()
                                                      {
                                                          Id = lecture.Id,
                                                          Name = lecture.Name,
                                                          Homeworks = (from homework in lecture.Homeworks
                                                                       where homework.Author.Id == user.Id
                                                                       select new HomeworkModel()
                                                                       {
                                                                           Id = homework.Id,
                                                                           FileName = homework.FileName
                                                                       }),
                                                          HomeworkDeadline = lecture.HomeworkDeadline
                                                      })
                                      }).FirstOrDefault();

                if (detailedCourse == null)
                {
                    throw new ArgumentNullException("Course not found!");
                }

                var response = this.Request.CreateResponse(HttpStatusCode.OK, detailedCourse);

                return response;

            });

            return responseMsg;
        }
    }
}
