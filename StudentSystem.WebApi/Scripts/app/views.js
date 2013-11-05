/// <reference path="../libs/_references.js" />


window.viewsFactory = (function () {
	var rootUrl = "Scripts/partials/";

	var templates = {};

	function getTemplate(name) {
		var promise = new RSVP.Promise(function (resolve, reject) {
			if (templates[name]) {
			    resolve(templates[name]);
			}
			else {
				$.ajax({
					url: rootUrl + name + ".html",
					type: "GET",
					success: function (templateHtml) {
						templates[name] = templateHtml;
						resolve(templateHtml);
					},
					error: function (err) {
					    reject(err);
					}
				});
			}
		});
		return promise;
	}

	function getLoginView() {
		return getTemplate("login-form");
	}

	function getHomeView() {
		return getTemplate("home");
	}

	function getProfileView() {
	    return getTemplate("members/profile");
	}

	function getSearchView() {
	    return getTemplate("search");
	}

	function getCourseView() {
	    return getTemplate("members/courses");
	}

	function getSingleCourseView() {
	    return getTemplate("members/singleCourse");
	}
    
	return {
		getLoginView: getLoginView,
		getHomeView: getHomeView,
		getProfileView: getProfileView,
		getSearchView: getSearchView,
		getCourseView: getCourseView,
		getSingleCourseView: getSingleCourseView
	};
}());