cs341.controller('registerController', function ($scope,$http,$state) {
    scope = $scope;

    scope.adminKey = "admin";
    scope.employeeKey = "employ";
    scope.ErrorMesage = '';

    scope.invalid = function () {
        if(scope.firstName == null || scope.lastName == null || scope.username == null || scope.password == null){
            return true;
        }
        return false;
    }

    scope.redirectToLogin = function () {
        $state.transitionTo('login', { arg: 'arg' });
    }

    scope.register = function () {
        input = {
            FirstName: scope.firstName,
            LastName: scope.lastName,
            Username: scope.username,
            Password: scope.password,
            Role: null,
            Type: null
        }

        if (scope.key === scope.adminKey) {
            input.Type = "admin";
        }else if (scope.key === scope.employeeKey) {
            input.Type = "employee";
            input.Role = scope.employee;
        } else {
            input.Type = "patient";
        }


        $http({
            url: "api/AddUser",
            method: "Post",
            data: input
        })
   .then(function (response) {
       alert(response.data);
       $state.transitionTo('login', { arg: 'arg' });
   });
    }

});