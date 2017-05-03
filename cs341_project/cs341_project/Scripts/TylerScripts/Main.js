// Author: Tyler Timm
// Description: This script handles the routing for the web application
var cs341 = angular.module("cs341", ['ui.router']);

cs341.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $urlRouterProvider.otherwise('login');

    $stateProvider
        // ABOUT PAGE AND MULTIPLE NAMED VIEWS =================================
        .state('login', {
            url: '/login',
            templateUrl: 'Pages/login.html',
            controller: 'loginController'
        })
        .state('patient', {
            url: '/patient',
            templateUrl: 'Pages/patient.html',
            controller: 'patientController'
        })
        .state('admin', {
            url: '/admin',
            templateUrl: 'Pages/admin.html',
            controller: 'adminController'
        })
        .state('employee', {
            url: '/employee',
            templateUrl: 'Pages/employee.html',
            controller: 'employeeController'
        })
        .state('register', {
            url: '/register',
            templateUrl: 'Pages/register.html',
            controller: 'registerController'
        });
});
