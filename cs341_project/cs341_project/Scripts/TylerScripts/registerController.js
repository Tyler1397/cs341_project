cs341.controller('registerController', function ($scope,$http) {
    scope = $scope;

    scope.adminKey = "admin";
    scope.employeeKey = "employ";

    scope.register = function () {
        scope.input = {
            FirstName: scope.firstName,
            LastName: scope.lastName,
            Username: scope.username,
            Password: scope.password,
            Role: null,
            Type: null
        }

        if (scope.key === scope.adminKey) {
            scope.input.Type = "admin";
        }

        if (scope.key === scope.employeeKey) {
            scope.input.Type = "employee";
            scope.input.Role = scope.employee;
            alert(scope.input.Role + " ");
        }

        $http({
            url: "api/AddUser",
            method: "Post",
            data: scope.input
        })
   .then(function (response) {
       alert(response.data);
   });
    }

});