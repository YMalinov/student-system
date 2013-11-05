using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using StudentSystem.Model;
using StudentSystem.WebApi.Models;

namespace StudentSystem.WebApi.Controllers
{
    public class UsersController : BaseController
    {
        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 30;
        private const int MinNicknameLength = 6;
        private const int MaxNicknameLength = 30;
        private const string ValidUsernameCharacters = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_.";
        private const string ValidNicknameCharacters = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_. -";
        private const string SessionKeyChars = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
        private const int SessionKeyLength = 50;
        private const int Sha1Length = 40;

        [HttpPost]
        [ActionName("register")]
        // api/users/register
        public HttpResponseMessage Register(RegisterUserModel model)
        {
            var responseMsg = PerformOperation(() =>
            {
                ValidateUsername(model.Username);
                //ValidateNickname(model.Nickname);
                ValidateAuthCode(model.AuthCode);
                var usernameToLower = model.Username.ToLower();
                var entityUser = this.context.Users.FirstOrDefault(u => u.Username == usernameToLower || u.Nickname == model.Nickname);
                if (entityUser != null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Conflict, "Username or nickname is not free.");
                }

                var user = new User
                {
                    Username = usernameToLower,
                    Nickname = model.Nickname,
                    AuthCode = model.AuthCode,
                    RegistrationDate = DateTime.Now,
                    LastVisit = DateTime.Now,
                    IsActive = true,
                    StudentNumber = GetStudentNumber(),
                    AboutMe = model.AboutMe,
                    Birthday = model.Birthday,
                    Email = model.Email,
                    Gender = model.Gender,
                    Hometown = model.Hometown,
                    Occupation = model.Occupation,
                    WebSite = model.WebSite,
                };

                this.context.Users.Add(user);
                this.context.SaveChanges();

                user.SessionKey = GenerateSessionKey(user.Id);
                this.context.SaveChanges();

                var registeredUser = new RegisteredUserModel
                {
                    Username = user.Nickname,
                    SessionKey = user.SessionKey,
                    UserType = entityUser.UserType
                };
                var response = this.Request.CreateResponse(HttpStatusCode.Created, registeredUser);

                return response;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("login")]
        // api/users/login
        public HttpResponseMessage Login(RegisterUserModel model)
        {
            var responseMsg = PerformOperation(() =>
            {
                ValidateUsername(model.Username);
                ValidateAuthCode(model.AuthCode);
                var usernameToLower = model.Username.ToLower();
                var entityUser = this.context.Users.FirstOrDefault(u => u.Username == model.Username && u.AuthCode == model.AuthCode);
                if (entityUser == null || !entityUser.IsActive)
                {
                    throw new ArgumentException("Invalid username or password.");
                }

                entityUser.SessionKey = GenerateSessionKey(entityUser.Id);
                this.context.SaveChanges();

                var logedinUser = new RegisteredUserModel
                {
                    Username = entityUser.Username,
                    SessionKey = entityUser.SessionKey,
                    UserType = entityUser.UserType
                };
                var response = this.Request.CreateResponse(HttpStatusCode.Created, logedinUser);

                return response;

            });

            return responseMsg;
        }

        [HttpPut]
        [ActionName("logout")]
        // api/users/logout
        public HttpResponseMessage Logout(string sessionKey)
        {
            var responseMsg = PerformOperation(() =>
            {
                var user = GetUser(sessionKey);

                user.SessionKey = null;
                this.context.SaveChanges();
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getuser")]
        // api/users/get/{id}
        public UserFullModel GetUser(string username, string sessionKey)
        {
            var response = this.PerformOperationAndHandleExceptions(() =>
            {
                var currentUser = GetUser(sessionKey);
                var searchUser = context.Users.FirstOrDefault(u => u.Username == username);
                if (searchUser == null)
                {
                    throw new ArgumentException("Invalid user id.");
                }
                var userFModel = new UserFullModel
                {
                    AboutMe = searchUser.AboutMe,
                    Birthday = searchUser.Birthday,
                    Email = searchUser.Email,
                    Gender = searchUser.Gender,
                    Hometown = searchUser.Hometown,
                    LastVisit = searchUser.LastVisit,
                    Nickname = searchUser.Nickname,
                    Occupation = searchUser.Occupation,
                    RegistrationDate = searchUser.RegistrationDate,
                    StudentNumber = searchUser.StudentNumber,
                    Username = searchUser.Username,
                    WebSite = searchUser.WebSite
                };

                if (currentUser.Id == searchUser.Id)
                {
                    userFModel.Courses = from c in currentUser.Courses
                                         let marks = c.Marks.FirstOrDefault(m => m.Student.Id == currentUser.Id)
                                         where marks != null
                                         select new CourseUserModel
                                         {
                                             ExamScore = marks.ExamScore,
                                             ExamScoreMax = c.Marks.Max(m => m.ExamScore),
                                             HomeworksCount = c.Lectures.Count(l => l.HomeworkDeadline != null),
                                             Id = c.Id,
                                             SubmitedHomeworksCount = c.Lectures.Count(l => l.Homeworks.Any(h => h.Author.Id == currentUser.Id)),
                                             TestScore = marks.TestScore,
                                             Title = c.Name,
                                             FinalResult = new FinalResultModel
                                             {
                                                 Position = marks.Position,
                                                 Score = marks.Score,
                                                 Status = marks.FinalResult
                                             }
                                         };
                }

                return userFModel;
            });

            return response;
        }

        private int GetStudentNumber()
        {
            var rnd = new Random();
            int studentNumber = 0;
            studentNumber = 1000000 + rnd.Next(1000000);
            var user = context.Users.FirstOrDefault(u => u.StudentNumber == studentNumber);

            while (user != null)
            {
                studentNumber = 1000000 + rnd.Next(1000000);
                user = context.Users.FirstOrDefault(u => u.StudentNumber == studentNumber);
            }

            return studentNumber;
        }


        private string GenerateSessionKey(int userId)
        {
            var rand = new Random();
            var skeyBuilder = new StringBuilder(SessionKeyLength);
            skeyBuilder.Append(userId);
            while (skeyBuilder.Length < SessionKeyLength)
            {
                var index = rand.Next(SessionKeyChars.Length);
                skeyBuilder.Append(SessionKeyChars[index]);
            }

            return skeyBuilder.ToString();
        }

        private void ValidateAuthCode(string authCode)
        {
            if (authCode == null || authCode.Length != Sha1Length)
            {
                throw new ArgumentOutOfRangeException("Password should be encrypted");
            }
        }

        private void ValidateNickname(string nickname)
        {
            if (nickname == null)
            {
                return;
                //throw new ArgumentNullException("Nickname cannot be null");
            }
            else if (nickname.Length < MinNicknameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Nickname must be at least {0} characters long",
                    MinNicknameLength));
            }
            else if (nickname.Length > MaxNicknameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Nickname must be less than {0} characters long",
                    MaxNicknameLength));
            }
            else if (nickname.Any(ch => !ValidNicknameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Nickname must contain only Latin letters, digits .,_");
            }
        }

        private void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null");
            }
            else if (username.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be at least {0} characters long",
                    MinUsernameLength));
            }
            else if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be less than {0} characters long",
                    MaxUsernameLength));
            }
            else if (username.Any(ch => !ValidUsernameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Username must contain only Latin letters, digits .,_");
            }
        }
    }
}
