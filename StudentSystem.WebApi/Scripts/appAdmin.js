/// <reference path="_references.js" />

angular.module("admin", [])
	.config(["$routeProvider", function ($routeProvider) {
	    $routeProvider
			.when("/", {
			    templateUrl: "partialsAdmin/home.html"
			})
            .when("/users", {
                templateUrl: "partialsAdmin/allUsers.html",
                controller: controllers.getAllUsersController
            })
            .when("/user/:id", {
                templateUrl: "partialsAdmin/singleUser.html",
                controller: controllers.getSingleUserController
            })
            .when("/courses", {
                templateUrl: "partialsAdmin/allCourses.html",
                controller: controllers.getAllCoursesController
            }).when("/course/new", {
                templateUrl: "partialsAdmin/singleCourse.html",
                controller: controllers.getNewCourseController

            })
            .when("/course/:id", {
                templateUrl: "partialsAdmin/singleCourse.html",
                controller: controllers.getSingleCourseController
            })
			.otherwise({ redirectTo: "/" });
	}]);