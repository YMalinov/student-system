using StudentSystem.Model;
using StudentSystem.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentSystem.WebApi.Controllers
{
    public class AdminController : BaseController
    {
        #region Courses

        //api/admin/course?sessionKey={sessionKey}
        [HttpPost, ActionName("course")]
        public HttpResponseMessage CreateCourse([FromBody]MinimizedCourseModel course, string sessionKey)
        {
            var responseMsg = PerformOperation(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var courseToAdd = new Course()
                {
                    Name = course.Title,
                    Description = course.Description,
                    StartDate = course.StartDate ?? DateTime.Now,
                    EndDate = course.EndDate ?? DateTime.Now,
                    SignUpDeadline = course.SignUpDeadline ?? (course.StartDate ?? DateTime.Now),
                    LecturesPerWeek = course.LecturesPerWeek,
                    IsVisible = true
                };

                context.Courses.Add(courseToAdd);
                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);

                return response;
            });

            return responseMsg;
        }

        //api/admin/courses?sessionKey={sessionKey}
        [HttpGet, ActionName("courses")]
        public HttpResponseMessage GetAllCourses(string sessionKey)
        {
            var responseMsg = PerformOperation(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var courses = from course in context.Courses
                              select new AdminMinimizedCourseModel()
                              {
                                  Id = course.Id,
                                  Title = course.Name,
                                  Description = course.Description,
                                  StartDate = course.StartDate,
                                  EndDate = course.EndDate,
                                  LecturesPerWeek = course.LecturesPerWeek,
                                  SignUpDeadline = course.SignUpDeadline,
                                  IsVisible = course.IsVisible
                              };

                var response = this.Request.CreateResponse(HttpStatusCode.OK, courses);

                return response;

            });

            return responseMsg;
        }

        ////api/admin/courses?sessionKey={sessionKey}&page={page}&count={count}
        //[HttpGet]
        //public HttpResponseMessage GetAllCoursesWithPaging(string sessionKey, int page, int count)
        //{
        //    var responseMsg = PerformOperation(() =>
        //    {
        //        this.CheckIfUserIsAdmin(sessionKey);

        //        var courses = (from course in context.Courses
        //                       select new AdminMinimizedCourseModel()
        //                       {
        //                           Id = course.Id,
        //                           Title = course.Name,
        //                           Description = course.Description,
        //                           StartDate = course.StartDate,
        //                           EndDate = course.EndDate,
        //                           LecturesPerWeek = course.LecturesPerWeek,
        //                           SignUpDeadline = course.SignUpDeadline,
        //                           IsVisible = course.IsVisible
        //                       }).Skip(page * count).Take(count);

        //        var response = this.Request.CreateResponse(HttpStatusCode.OK, courses);

        //        return response;

        //    });

        //    return responseMsg;
        //}

        //api/admin/course/{id}?sessionKey={sessionKey}

        [HttpGet, ActionName("course")]
        public HttpResponseMessage GetByCourseId(int id, string sessionKey)
        {
            var responseMsg = PerformOperation(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var detailedCourse = (from course in context.Courses.Where(c=>c.Id ==id)
                                      select new AdminDetailedCourseModel()
                                      {
                                          Id = course.Id,
                                          Title = course.Name,
                                          Description = course.Description,
                                          StartDate = course.StartDate,
                                          EndDate = course.EndDate,
                                          LecturesPerWeek = course.LecturesPerWeek,
                                          SignUpDeadline = course.SignUpDeadline,
                                          IsVisible = course.IsVisible,
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

        //api/admin/updatecourse/{id}?sessionKey={sessionKey}
        [HttpPut, ActionName("updatecourse")]
        public HttpResponseMessage UpdateCourse([FromBody]MinimizedCourseModel courseToUpdate, int id, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var course = (from currentCourse in context.Courses
                              where currentCourse.Id == id
                              select currentCourse).FirstOrDefault();

                if (course == null)
                {
                    throw new ArgumentException("Course not found!");
                }

                course.Name = courseToUpdate.Title ?? course.Name;
                course.Description = courseToUpdate.Description ?? course.Description;
                course.StartDate = courseToUpdate.StartDate ?? course.StartDate;
                course.EndDate = courseToUpdate.EndDate ?? course.EndDate;
                course.LecturesPerWeek = courseToUpdate.LecturesPerWeek;
                course.SignUpDeadline = courseToUpdate.SignUpDeadline ?? course.SignUpDeadline;

                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });

            return responseMsg;
        }

        //api/admin/deletecourse/{id}?sessionKey={sessionKey}
        [HttpDelete, ActionName("course")]
        public HttpResponseMessage DeactivateCourse(string sessionKey, int id)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var course = context.Courses.FirstOrDefault(c=>c.Id==id);

                if (course == null)
                {
                    throw new ArgumentException("Course not found!");
                }

                course.IsVisible = false;

                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });

            return responseMsg;
        }

        //api/admin/addlecture/{id}?sessionKey={sessionKey}
        [HttpPut, ActionName("addlecture")]
        public HttpResponseMessage AddLectureToCourse([FromBody]AdminAddLectureModel lectureToAdd, string sessionKey, int id)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var searchedCourse = (from course in context.Courses
                                      where course.Id == id
                                      select course).FirstOrDefault();

                if (searchedCourse == null)
                {
                    throw new ArgumentException("Course not found!");
                }

                var newLecture = new Lecture()
                {
                    Course = searchedCourse,
                    HomeworkDeadline = lectureToAdd.HomeworkDeadline,
                    Name = lectureToAdd.Name
                };

                context.Lectures.Add(newLecture);
                searchedCourse.Lectures.Add(newLecture);

                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);

                return response;
            });

            return responseMsg;
        }

        #endregion

        #region Users

        [HttpGet, ActionName("allusers")]
        // api/admin/allusers?sessionKey={sessionKey}
        public HttpResponseMessage GetAllUsers(string sessionKey)
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var users = from user in context.Users
                            select new AdminMinimizedUserModel()
                            {
                                Id = user.Id,
                                Nickname = user.Nickname,
                                Username = user.Username,
                                UserType = user.UserType,
                                IsActive = user.IsActive
                            };

                var responseMsg = this.Request.CreateResponse(HttpStatusCode.OK, users);

                return responseMsg;
            });

            return response;
        }

        //[HttpGet, ActionName("allusers")]
        //// api/admin/allusers?page=0&count=5&sessionKey={sessionKey}
        //public HttpResponseMessage GetAllUsersWithPaging(string sessionKey, int page, int count)
        //{
        //    var response = this.PerformOperationAndHandleExceptions(() =>
        //    {
        //        this.CheckIfUserIsAdmin(sessionKey);

        //        var users = (from user in context.Users
        //                     select new AdminMinimizedUserModel()
        //                     {
        //                         Id = user.Id,
        //                         Nickname = user.Nickname,
        //                         Username = user.Username,
        //                         UserType = user.UserType,
        //                         IsActive = user.IsActive
        //                     }).Skip(page * count).Take(count);

        //        var responseMsg = this.Request.CreateResponse(HttpStatusCode.OK, users);

        //        return responseMsg;
        //    });

        //    return response;
        //}

        [HttpGet, ActionName("user")]
        // api/admin/user/{id}?sessionKey={sessionKety}
        public HttpResponseMessage GetUser(int id, string sessionKey)
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var searchedUser = 
                            (from user in context.Users
                             where user.Id == id
                             select new AdminUserFullModel()
                             {
                                 Nickname = user.Nickname,
                                 Username = user.Username,
                                 AboutMe = user.AboutMe,
                                 Birthday = user.Birthday,
                                 Email = user.Email,
                                 Gender = user.Gender,
                                 Hometown = user.Hometown,
                                 LastVisit = user.LastVisit,
                                 Occupation = user.Occupation,
                                 RegistrationDate = user.RegistrationDate,
                                 StudentNumber = user.StudentNumber,
                                 UserType = user.UserType,
                                 WebSite = user.WebSite,
                                 IsActive = user.IsActive,
                                 Courses = (from c in user.Courses
                                            let marks = c.Marks.FirstOrDefault(m => m.Student.Id == user.Id)
                                            where marks != null
                                            select new CourseUserModel()
                                            {
                                                ExamScore = marks.ExamScore,
                                                ExamScoreMax = c.Marks.Max(m => m.ExamScore),
                                                HomeworksCount = c.Lectures.Count(l => l.HomeworkDeadline != null),
                                                Id = c.Id,
                                                SubmitedHomeworksCount = c.Lectures.Count(l => l.Homeworks.Any(h => h.Author.Id == user.Id)),
                                                TestScore = marks.TestScore,
                                                Title = c.Name,
                                                FinalResult = new FinalResultModel
                                                {
                                                    Position = marks.Position,
                                                    Score = marks.Score,
                                                    Status = marks.FinalResult
                                                }
                                            })
                             }).FirstOrDefault();

                if (searchedUser == null)
                {
                    throw new ArgumentException("User not found!");
                }

                var responseMsg = this.Request.CreateResponse(HttpStatusCode.OK, searchedUser);

                return responseMsg;
            });

            return response;
        }

        [HttpPut, ActionName("updateuser")]
        // api/admin/updateuser/{id}?sessionKey={sessionKey}
        public HttpResponseMessage UpdateUser([FromBody]AdminUpdateUserModel userToUpdate, string sessionKey, int id)
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var user = (from currentUser in context.Users
                            where currentUser.Id == id
                            select currentUser).FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException("User not found!");
                }

                user.Username = userToUpdate.Username ?? user.Username;
                user.Nickname = userToUpdate.Nickname ?? user.Nickname;
                user.WebSite = userToUpdate.WebSite ?? user.WebSite;
                user.Gender = userToUpdate.Gender == Gender.Unknown ? user.Gender : userToUpdate.Gender;
                user.Hometown = userToUpdate.Hometown ?? user.Hometown;
                user.Birthday = userToUpdate.Birthday ?? user.Birthday;
                user.Email = userToUpdate.Email ?? user.Email;
                user.Occupation = userToUpdate.Occupation ?? user.Occupation;
                user.AboutMe = userToUpdate.AboutMe ?? user.AboutMe;

                if (user.UserType != UserType.Admin && userToUpdate.UserType == UserType.Admin)
                {
                    user.UserType = UserType.Admin;
                }
                else if (user.UserType == UserType.Regular && userToUpdate.UserType == UserType.Lecturer)
                {
                    user.UserType = UserType.Lecturer;
                }

                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });

            return response;
        }

        [HttpDelete, ActionName("deactivateuser")]
        // api/admin/deactivateuser/{id}?sessionKey={sessionKey}
        public HttpResponseMessage DeactivateUser(string sessionKey, int id)
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
            {
                this.CheckIfUserIsAdmin(sessionKey);

                var user = (from currentUser in context.Users
                            where currentUser.Id == id
                            select currentUser).FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException("User not found!");
                }

                user.IsActive = false;

                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });

            return response;
        }

        #endregion

        #region Helper Methods

        private void CheckIfUserIsAdmin(string sessionKey)
        {
            var user = GetUser(sessionKey);

            if (user.UserType != UserType.Admin)
            {
                throw new UnauthorizedAccessException("User is not admin!");
            }
            else if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("User is deactivated.");
            }
        }

        #endregion
    }
}
