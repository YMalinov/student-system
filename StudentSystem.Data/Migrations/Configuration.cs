using System;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using StudentSystem.Model;

namespace StudentSystem.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystemContext context)
        {
            //AddCourse(context);
            //AddUser(context);
            //AddSecondaryUsers(context);
        }

        private static void AddSecondaryUsers(StudentSystemContext context)
        {
            context.Users.AddOrUpdate(new User
            {
                Username = "doncho",
                Gender = Gender.Male,
                Nickname = "Doncho Minkov",
                AuthCode = "7288edd0fc3ffcbe93a0cf06e3568e28521687bc",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            });
            context.Users.AddOrUpdate(new User
            {
                Username = "joro",
                Gender = Gender.Male,
                Nickname = "Georgi Georgiev",
                AuthCode = "7288edd0fc3ffcbe93a0cf06e3568e28521687bc",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            });
            context.Users.AddOrUpdate(new User
            {
                Username = "teo",
                Gender = Gender.Male,
                Nickname = "Teodor Kurtev",
                AuthCode = "7288edd0fc3ffcbe93a0cf06e3568e28521687bc",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            });
            context.Users.AddOrUpdate(new User
            {
                Username = "pirin",
                Gender = Gender.Male,
                Nickname = "Pirin Karabenchev",
                AuthCode = "7288edd0fc3ffcbe93a0cf06e3568e28521687bc",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            });
            context.Users.AddOrUpdate(new User
            {
                Username = "yanko",
                Gender = Gender.Male,
                Nickname = "Yanko Malinov",
                AuthCode = "7288edd0fc3ffcbe93a0cf06e3568e28521687bc",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            });
            context.SaveChanges();
        }

        private static void AddCourse(StudentSystemContext context)
        {
            var course = new Course
            {
                Name = "JS библиотеки",
                Description =
                    "По време на курса ще се запознаете с най-често използванит JavaScript библиотеки, улесняващи разработката на клиентски приложения с JavaScript. Ще разгледате  най-утвърдените начини за разработка на SPA приложения (Single Page Applications) и ще използвате богатия ресурс на библиотеки като Kendo UI и Angular JS. Курсът е отворен само за онлайн участие.",
                StartDate = new DateTime(2013, 08, 21),
                EndDate = new DateTime(2013, 09, 03),
                LecturesPerWeek = 5,
                SignUpDeadline = new DateTime(2013, 08, 21),
                Lectures = new Collection<Lecture>()
                {
                    new Lecture()
                    {
                        Name = "Въведение в курса",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=Hnr0E7QZlkU",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/0.%20Course%20Introduction/JavaScript-Frameworks-Course-Introduction.pptx",
                                Type = ResourceType.Presentation
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "Underscore.js",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=66sYZhaQdcc",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/1.%20Underscore.js/Underscore.js.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/1.%20Underscore.js/underscore.js-demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "mustache.js",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=aqetDxb-ZVw",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/3.%20Mustache.js/mustache.js.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/3.%20Mustache.js/mustache.js-demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "RequireJS",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=0evHxijFbVo",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/5.%20RequireJS/RequireJS.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/5.%20RequireJS/RequireJS-demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "Modernizr",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Наков - 22.08.2013",
                                Link = "http://www.youtube.com/watch?v=rvYkiMk-m1Y",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/4.%20Modernizr.js/Modernizr.js.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/4.%20Modernizr.js/Modernizr.js-Demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "Sammy.js",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=AHkR5YjkSLU",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/6.%20Sammy.js/Sammy.js.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Title = "Домашна работа - Battle-game Server",
                                Link = "https://www.dropbox.com/s/nt7pb9lnl1bpszn/Battle-Game.zip",
                                Type = ResourceType.Archive
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/6.%20Sammy.js/Sammy.js-demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "SPA приложения и JavaScript шаблони",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=45GiUY-OqG8",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/8.%20JavaScript%20Patterns%20and%20SPA/JavaScript%20Patterns%20and%20SPA.pptx",
                                Type = ResourceType.Presentation
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "KendoUI",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "част 1 - Ивайло Кенов",
                                Link = "http://www.youtube.com/watch?v=3J5ubH1sSkg",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Title = "част 2 - Ивайло Кенов",
                                Link = "http://www.youtube.com/watch?v=VBlM-xCzaEI",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/9.%20KendoUI/KendoUI.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/9.%20KendoUI/KendUI-demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "AngularJS",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=s5__gMRiNGk",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/10.%20AngularJS/AngularJS.pptx",
                                Type = ResourceType.Presentation
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/10.%20AngularJS/AngularJS-demos.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "Примерен изпит за подготовка",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Дончо Минков",
                                Link = "http://www.youtube.com/watch?v=ApOqh3SQx7c",
                                Type = ResourceType.YouTubeLink
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/11.%20Sample%20Exam/Exam-Car-Rental-System.doc",
                                Type = ResourceType.WordDocument
                            },
                            new Resource()
                            {
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/Lectures/11.%20Sample%20Exam/Car-Rental-System-Sample-Exam-Solution.zip",
                                Type = ResourceType.Demo
                            }
                        }
                    },
                    new Lecture()
                    {
                        Name = "Отборна работа",
                        Resources = new Collection<Resource>()
                        {
                            new Resource()
                            {
                                Title = "Условие",
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/TeamWork/JavaScript-Frameworks-Teamwork-Assignment-August-2013.docx",
                                Type = ResourceType.WordDocument
                            },
                            new Resource()
                            {
                                Title = "Примерни проекти",
                                Link =
                                    "http://downloads.academy.telerik.com/svn/js-frameworks/2013/TeamWork/JS-Frameworks-Teamwork-Sample-Projects.docx",
                                Type = ResourceType.WordDocument
                            }
                        }
                    }
                }
            };

            context.Courses.AddOrUpdate(course);
            context.SaveChanges();

            foreach (var lecture in course.Lectures)
            {
                lecture.HomeworkDeadline = course.EndDate;
            }
            context.SaveChanges();
        }

        private static void AddUser(StudentSystemContext context)
        {
            var userSaykor = new User
            {
                Username = "saykor",
                Gender = Gender.Male,
                Nickname = "Saykor",
                AuthCode = "7288edd0fc3ffcbe93a0cf06e3568e28521687bc",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            };
            context.Users.AddOrUpdate(userSaykor);

            var userVlado = new User
            {
                Username = "vlado",
                Gender = Gender.Male,
                Nickname = "Vlado",
                AuthCode = "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3",
                RegistrationDate = DateTime.Now,
                LastVisit = DateTime.Now
            };
            context.Users.AddOrUpdate(userVlado);

            context.SaveChanges();
        }
    }
}
