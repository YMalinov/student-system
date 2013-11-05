/// <reference path="libs/_references.js" />

$(function() {
    var appLayout = new kendo.Layout('<div id="main-content"></div>');
    var data = persisters.get("api/");
    vmFactory.setPersister(data);

    if (localStorage.getItem("isAdmin") == 2) {
        $("#adinBtn").remove();
        $("#navbar").append('<li><a id="adinBtn" href="Scripts/admin.html">Admin</a></li>');
    }

    var router = new kendo.Router();
    router.route("/", function () {
        var btnSearch = document.getElementById("btnSearch");
        $("#btnSearch").on("click", function (e) {
            var searchTag = document.getElementById("txtSearch").value;
            router.navigate("/search/" + searchTag);

        });
        viewsFactory.getHomeView().then(function(homeViewHtml) {
            vmFactory.getHomeViewModel().then(function(vm) {
                var view = new kendo.View(homeViewHtml,
                    {
                        model: vm
                    });
                appLayout.showIn("#main-content", view);
            }, function(err) {
                console.log(err);
            });
        });
    });

    //only for registered users
    router.route("/search/:q", function (searchTag) {
        viewsFactory.getSearchView().then(function (searchViewHtml) {
            vmFactory.getSearchViewModel(searchTag).then(function (vm) {
                var view = new kendo.View(searchViewHtml,
                    {
                        model: vm
                    });
                appLayout.showIn("#main-content", view);
            }, function (err) {
                console.log(err);
            });
        });
    });
    
    router.route("/login", function() {
        if (data.isUserLoggedIn()) {
            router.navigate("/members");
        } else {
            viewsFactory.getLoginView().then(function(loginViewHtml) {
                var loginVm = vmFactory.getLoginVM(function() {
                    router.navigate("/members");
                }, function(err) {
                    console.log(err);
                });
                var view = new kendo.View(loginViewHtml, {
                    model: loginVm
                });
                appLayout.showIn("#main-content", view);
            });
        }
    });

    router.route("/courses", function () {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getCourseView().then(function (courseView) {
                vmFactory.getCourseViewModel().then(function (vm) {
                    var view = new kendo.View(courseView,
                    {
                        model: vm
                    });
                    appLayout.showIn("#main-content", view);
                }, function (err) {
                    console.log(err);
                });
            });
        }
    });

    router.route("/course/:id", function (id) {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getSingleCourseView().then(function (singleCourseView) {
                vmFactory.getSingleCourseViewModel(id).then(function (vm) {
                    var view = new kendo.View(singleCourseView,
                    {
                        model: vm
                    });
                    appLayout.showIn("#main-content", view);
                })
            })
        }
    });

    router.route("/logout", function () {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            vmFactory.getLogout().then(function () {
                router.navigate("/login");
            });
        }
    });

    //only for registered users
    router.route("/members", function() {
        if (!data.isUserLoggedIn()) {
            router.navigate("/login");
        } else {
            viewsFactory.getProfileView().then(function(profileViewHtml) {
                vmFactory.getProfileViewModel().then(function (vm) {
                    var view = new kendo.View(profileViewHtml,
                    {
                        model: vm
                    });
                    appLayout.showIn("#main-content", view);
                }, function(err) {
                    console.log(err);
                });
            });
        }
    });

    $(function() {
        appLayout.render("#app");
        router.start();
    });
});