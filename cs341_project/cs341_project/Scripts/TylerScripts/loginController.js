cs341.controller('loginController', function ($scope, $http, $rootScope, $state) {
    scope = $scope;
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Login");
    $('#error').hide();

    scope.username = '';
    scope.password = '';


    scope.submit = function () {
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
        $rootScope.data = response.data;
        $scope.data = $rootScope.data;
        if ($scope.data.Valid == false || $scope.data.Valid == null) {
            $('#error').show("slow");
            return;
        }
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

    });
    }


});