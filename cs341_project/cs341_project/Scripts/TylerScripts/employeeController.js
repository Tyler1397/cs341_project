cs341.controller('employeeController', function ($state, $scope, $rootScope,$http) {
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");


    $scope.userData = $rootScope.data;

    if ($scope.userData == null || $scope.userData.Valid == false) {
        $state.transitionTo('login', { arg: 'arg' });
    }

    $scope.message = "Hello " + $scope.userData.User.FirstName;

    // function takes in a message id, and deletes it
    $scope.deleteMessage = function (id) {
        $http({
            url: "api/DeleteMessage",
            method: "Post",
            data: JSON.stringify(id + "")
        }).then(function () {
            for (var i = 0; i < $scope.userData.User.Messages.length; i++) {
                if ($scope.userData.User.Messages[i].Id === id) {
                    $scope.userData.User.Messages.splice(i, 1);
                    return;
                }
            }
        });
    }

});