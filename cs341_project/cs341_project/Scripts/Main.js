
var cs341 = angular.module("cs341", ["ngRoute"]);

// configure our routes
cs341.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
         .when("/", {
             templateUrl: "Pages/home.htm",
             controller: "loginController"
         })
        // route for the home page
        .when("/home", {
            templateUrl: "Pages/home.htm",
            controller: "loginController"
        })
        // route for the about page
        .when("/login", {
            templateUrl: "Pages/login.htm",
            controller: "loginController"
        })
     .when("/calendar", {
         templateUrl: "Pages/calendar.html",
         controller: "calendarController"
     })
});
