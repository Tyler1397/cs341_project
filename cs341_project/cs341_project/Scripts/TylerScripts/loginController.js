cs341.controller('loginController', function ($scope, $http) {
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
        alert(response.data);
    });
    }


});