// Author: Tyler Timm
// Description: This script handles the functionality of the login page
cs341.controller('loginController', function ($scope, $http, $rootScope, $state) {
    scope = $scope;

    // visual effects
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Login");
    $('#error').hide();

    // switch to patient-employee help guide
    document.getElementById("patientHelp").style.display = "block";
    document.getElementById("adminHelp").style.display = "none";

    // variables to track input and errors
    scope.errorExists = false;
    scope.message = '';
    scope.loading = false;
    scope.username = '';
    scope.password = '';

    // return true if an error exists
    scope.errorExists = function(){
        return $scope.data.User == null;
    }

    // attemp to login to the web app
    scope.login = function () {
        scope.input = {
            Username: scope.username,
            Password: scope.password
        }
        $http({
            url: "api/Login",
            method: "Post",
            data: scope.input
        })
    .then(function (response) {
        if (response.data.User == null) {
            scope.errorExists = true;
            scope.message = response.data.Message;
            scope.loading = false;
            
        } else {
            scope.errorExists = false;
            $rootScope.data = response.data;
            $scope.data = $rootScope.data;
            scope.loading = false;
            $("#main").fadeOut("slow", function () {
                switch ($rootScope.data.User.Type) {
                    case "admin":
                        $state.transitionTo('admin', { arg: 'arg' });
                        break;
                    case "employee":
                        $state.transitionTo('employee', { arg: 'arg' });
                        break;
                    case "patient":
                        $state.transitionTo('patient', { arg: 'arg' });
                        break;
                }
            });
        }

    });
    }

    // login
    scope.submit = function () {
        scope.loading = true;
        scope.login();
    }
});
