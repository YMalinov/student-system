/// <reference path="../libs/class.js" />
/// <reference path="../libs/q.js" />
/// <reference path="../libs/jquery-2.0.3.js" />
/// <reference path="../libs/httpRequester.js" />
/// <reference path="../libs/cryptoJS.js" />
/// <reference path="../libs/httpRequester.js" />

window.persisters = (function () {

    var username = localStorage.getItem('username');
    var sessionKey = localStorage.getItem('sessionKey');
    var isAdmin = localStorage.getItem('isAdmin');

    function saveUserData(userData) {
        localStorage.setItem('username', userData.username);
        localStorage.setItem('sessionKey', userData.sessionKey);
        localStorage.setItem('isAdmin', userData.userType);
        username = userData.username;
        sessionKey = userData.sessionKey;
    }

    function clearUserData() {
        localStorage.removeItem('username');
        localStorage.removeItem('sessionKey');
        localStorage.removeItem('isAdmin');
        username = null;
        sessionKey = null;
    }

    var MainPersiter = Class.create({
        init: function (root) {
            this.rootUrl = root;
            this.users = new UserPersiter(this.rootUrl);
            this.courses = new CoursePersister(this.rootUrl);
            this.search = new SearchPersister(this.rootUrl);
            this.events = new EventPersister(this.rootUrl);
            this.home = new HomePersister();
        },

        isUserLoggedIn: function () {
            return (sessionKey != null);
        },

        getUsername: function () {
            return username;
        }
    });

    var UserPersiter = Class.create({
        init: function (root) {
            this.rootUrl = root + "users/";
        },

        register: function (user) {
            var url = this.rootUrl + "register/";

            user.authCode = CryptoJS.SHA1(user.authKey).toString();

            return httpRequester.postJson(url, user).then(function (result) {
                saveUserData(result);
            });
        },

        login: function (user) {
            var url = this.rootUrl + "login/";

            user.authCode = CryptoJS.SHA1(user.authKey).toString();
            return httpRequester.postJson(url, user).then(function (result) {
                saveUserData(result);
            });
        },

        logout: function (user) {
            var url = this.rootUrl + "logout" + "?sessionKey=" + sessionKey;
            clearUserData();
            return httpRequester.putJson(url).then(function () {
            });
        },

        getById: function (userId) {
            var url = this.rootUrl + "GetUser/" + username + "?sessionKey=" + sessionKey;
            return httpRequester.getJson(url);
        },

        getAll: function () {
            return httpRequester.getJson(this.rootUrl);
        }
    });

    var CoursePersister = Class.create({
        init: function (root) {
            this.rootUrl = root + "courses/";
        },

        getAll: function () {
            return httpRequester.getJson(this.rootUrl);
        },

        getById: function (courseId) {
            var url = this.rootUrl + courseId;
            return httpRequester.getJson(url);
        },

        getByIdWithSessionKey: function (courseId) {
            var url = this.rootUrl + courseId + "?sessionKey=" + sessionKey;
            return httpRequester.getJson(url);
        },

        getArchive: function () {
            var url = this.rootUrl + "archieve/";
            return httpRequester.getJson(url);
        }

    });

    var EventPersister = Class.create({
        init: function (root) {
            this.rootUrl = root + "events/"
        },

        create: function (event) {
            return httpRequester.postJson(this.rootUrl, event);
        },

        getAll: function () {
            return httpRequester.getJson(this.rootUrl);
        }
    });

    var SearchPersister = Class.create({
        init: function (root) {
            this.rootUrl = root + "search";
        },

        byQueryString: function (queryString) {
            var url = this.rootUrl + "?q=" + queryString;
            return httpRequester.getJson(url);
        }
    });

    var HomePersister = Class.create({
        init: function () {
        },
        getVideos: function () {
            var deferred = Q.defer();
            $.ajax({
                url: "http://gdata.youtube.com/feeds/base/users/TelerikAcademy/uploads?orderby=updated&client=ytapi-youtube-rss-redirect&v=2&alt=rss",
                dataType: "jsonp",
                success: function (result) {
                    deferred.resolve(result);
                },
                error: function (errorData) {
                    deferred.reject(errorData);
                }
            });
            return deferred.promise;
        }
    });
    
    return {
        get: function (rootUrl) {
            return new MainPersiter(rootUrl);
        }
    };
})();