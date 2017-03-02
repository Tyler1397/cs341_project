cs341.controller('loginController', function ($scope, $http, $rootScope, $state) {
    scope = $scope;

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
        $state.transitionTo('patient', { arg: 'arg' });
    });
    }


});