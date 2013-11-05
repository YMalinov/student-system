/// <reference path="../_references.js" />

window.controllers = (function () {
    var baseUrl = "http://localhost:9290/api/admin/";
    var data = adminPersister.get(baseUrl);

    function stringToDate(stringDate) {
        console.log(stringDate);
    }

    function AllUsersController($scope, $http, $location) {
        data.getAllUsers()
        .then(function (users) {
            $scope.users = users;
            $scope.$apply();
        }, function (error) {
            console.log(error);
        });
    }

    function SingleUserController($scope, $http, $location, $routeParams) {
        var userId = $routeParams.id;
        data.getUser(userId)
        .then(function (user) {
            user.birthday = user.birthday.split('T')[0];
            $scope.user = user;
            $scope.$apply();
        }, function (error) {
        });

        $scope.save = function () {
            data.updateUser(userId, $scope.user)
            .then(function (succes) {
                console.log();
            }, function (error) {
                console.log();
            });
        }
    }

    function AllCoursesController($scope, $http, $location, $routeParams) {
        data.getAllCourses()
        .then(function (courses) {
            $scope.courses = courses;
            $scope.$apply();
        }, function (error) {
            console.log(error);
        });

        $scope.remove = function (id) {
            data.removeCourse(id)
            .then(function (success) {
                data.getAllCourses()
                .then(function (courses) {
                    $scope.courses = courses;
                    $scope.$apply();
                });
            }, function (error) {
                $scope.message = "Problem occurred: " + error.responseMessage;
            });
        }
    }

    function SingleCourseController($scope, $http, $location, $routeParams) {
        var courseId = $routeParams.id;
        data.getCourse(courseId)
        .then(function (course) {
            course.startdate = course.startdate.split("T")[0];
            course.enddate = course.enddate.split("T")[0];
            course.signupdeadline = course.signupdeadline.split("T")[0];
            $scope.course = course;
            $scope.$apply();
        }, function (error) {
            console.log();
        });
        
        $scope.save = function () {
            debugger;
            data.updateCourse($scope.course)
            .then(function (success) {
                $scope.message = "Updated."
                $scope.$apply();
            }, function (error) {
                $scope.message = "Problem occurred: " + error.responseMessage;
            });
        }
    }

    function NewCourseController($scope, $http, $location, $routeParams) {
        $scope.course = createEmptyCourse();
        $scope.message = ""

        $scope.save = function () {
            debugger;
            data.createCourse($scope.course)
            .then(function (success) {
                $scope.message = "Course added."
                $scope.course = createEmptyCourse();
            }, function (error) {
                $scope.message = "Problem occurred: " + error.responseMessage;
            });
        }
    }

    function createEmptyCourse() {
        var newCourse = {};
        newCourse.coursetitle = "";
        newCourse.description = "";
        newCourse.startdate = "";
        newCourse.enddate = "";
        newCourse.lecturesperweek = 0;
        newCourse.signupdeadline = "";

        return newCourse;
    }

    return {
        getAllUsersController: AllUsersController,
        getSingleUserController: SingleUserController,
        getAllCoursesController: AllCoursesController,
        getSingleCourseController: SingleCourseController,
        getNewCourseController: NewCourseController
    };
})();