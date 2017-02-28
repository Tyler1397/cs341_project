
var cs341 = angular.module("cs341", ['ui.router']);

cs341.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $urlRouterProvider.otherwise('home');

    $stateProvider

        // HOME STATES AND NESTED VIEWS ========================================
        .state('home', {
            url: '/home',
            templateUrl: 'Pages/home.html'
        })
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
            controller: 'loginController'
        })
        .state('register', {
            url: '/register',
            templateUrl: 'Pages/register.html',
            controller: 'registerController'
        });
});