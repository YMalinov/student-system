/// <reference path="../libs/_references.js" />
/// <reference path="dataLayer.js" />

window.vmFactory = (function () {
	var data = null;

	function getLoginViewModel(successCallback) {		
		var viewModel = {
			username: "DonchoMinkov",
			password: "123456q",
			login: function () {
			    var user = {
			        username: this.get("username"),
			        authCode: this.get("password")
			    };

				data.users.login(user)
					.then(function () {
					    if (localStorage.getItem("isAdmin") == 2) {
					        $("#adinBtn").remove();
					        $("#navbar").append('<li><a id="adinBtn" href="Scripts/admin.html">Admin</a></li>');
					    }

						if (successCallback) {
							successCallback();
						}
					});
			},
			register: function () {
			    var user = {
			        username: this.get("username"),
			        authCode: this.get("password")
			    };

				data.users.register(user)
					.then(function () {
					    if (localStorage.getItem("isAdmin") == 2) {
					        $("#adinBtn").remove();
					        $("#navbar").append('<li><a id="adinBtn" href="Scripts/admin.html">Admin</a></li>');
					    }

						if (successCallback) {
							successCallback();
						}
					});
			}
		};
		return kendo.observable(viewModel);
	};

	function getHomeViewModel() {
		return data.home.getVideos()
			.then(function (videos) {
			    var viewModel = {
			        videos: []
			    };
			    var items = $($.parseXML(videos)).find("item");
				for (var i = 0; i < items.length; i++) {
				    var title = $(items[i]).find('title').text();
				    var link = $(items[i]).find('link').text();
				    viewModel.videos.push({ title: title, link: link });
				}
				return kendo.observable(viewModel);
			});
	}

	function getProfileViewModel() {
	    return data.users.getById();
	}

	function getCourseViewModel() {
	    return data.courses.getAll().then(function (courses) {
	        var viewModel = {
	            courses: courses
	        }

	        return kendo.observable(viewModel);
	    });
	}

	function getSearchViewModel(searchTag) {
	    return data.search.byQueryString(searchTag);
	}

	function getLogout() {
	    return data.users.logout();
	}

	function getSingleCourseViewModel(id) {
	    return data.courses.getById(id);
	}

	return {
		getLoginVM: getLoginViewModel,
		getHomeViewModel: getHomeViewModel,
		getProfileViewModel: getProfileViewModel,
		getSearchViewModel: getSearchViewModel,
		getCourseViewModel: getCourseViewModel,
		getLogout: getLogout,
		getSingleCourseViewModel: getSingleCourseViewModel,
		setPersister: function (persister) {
			data = persister;
		}
	};
}());