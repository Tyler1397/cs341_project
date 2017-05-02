cs341.controller('loginController', function ($scope, $http, $rootScope, $state) {
    scope = $scope;
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Login");
    $('#error').hide();

    scope.errorExists = false;
    scope.message = '';
    scope.loading = false;
    scope.username = '';
    scope.password = '';

    scope.errorExists = function(){
        return $scope.data.User == null;
    }

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

    scope.submit = function () {
        var delayMillis = 1000; //1 second
        scope.loading = true;
        setTimeout(function () {
            scope.login();
        }, delayMillis)
    }


});