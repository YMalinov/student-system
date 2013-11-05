/// <reference path="../_referencesAdmin.js" />
window.adminPersister = (function () {
    var nickname = localStorage.getItem('nickname');
    var sessionKey = localStorage.getItem('sessionKey');

    var AdminPersister = Class.create({
        init: function (apiUrl) {
            this.apiUrl = apiUrl;
        },
        getAdminNickname: function () {
            nickname = localStorage.getItem('nickname');
            return nickname;
        },
        getAdminSessionKey: function () {
            sessionKey = localStorage.getItem('sessionKey');
            return sessionKey;
        },

        // users
        getAllUsers: function () {
            var url = this.apiUrl + "allusers" + "?sessionKey=" + sessionKey;

            return httpRequester.getJson(url);
        },
        getUser: function (id) {
            var url = this.apiUrl + "user/" + id + "?sessionKey=" + sessionKey;

            return httpRequester.getJson(url);
        },
        updateUser: function (userId, userData) {
            var url = this.apiUrl + "updateuser/" + userId + "?sessionKey=" + sessionKey;

            return httpRequester.putJson(url, userData);
        },

        // courses
        getAllCourses: function () {
            var url = this.apiUrl + "courses" + "?sessionKey=" + sessionKey;

            return httpRequester.getJson(url);
        },
        getCourse: function (id) {
            var url = this.apiUrl + "course/" + id + "?sessionKey=" + sessionKey;

            return httpRequester.getJson(url);
        },
        updateCourse: function (courseData) {
            var url = this.apiUrl + "updatecourse/"+ courseData.courseid + "?sessionKey=" + sessionKey;

            return httpRequester.putJson(url, courseData);
        },
        createCourse: function (courseData) {
            var url = this.apiUrl + "course" + "?sessionKey=" + sessionKey;

            return httpRequester.postJson(url, courseData);
        },
        removeCourse: function (id) {
            var url = this.apiUrl + "course/" + id + "?sessionKey=" + sessionKey;

            return httpRequester.deleteJson(url);
        }
    });

    return {
        get: function (apiUrl) {
            return new AdminPersister(apiUrl);
        }
    }
}());