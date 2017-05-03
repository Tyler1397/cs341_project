// Author: Tyler Timm
// Description: This script handles the functionality of the patient page
cs341.controller('patientController', function ($state, $scope, $rootScope, $http) {
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");

    // load global data
    $scope.userData = $rootScope.data;

    if ($scope.userData == null || $scope.userData.Valid == false) {
        $("#main").fadeOut("slow",function(){
            $state.transitionTo('login', { arg: 'arg' });
        })
    }

    $scope.message = "Hello "+ $scope.userData.User.FirstName;

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

    // function takes in an appointment id and deletes it from the database
    $scope.deleteAppointment = function (id) {
        var answer = confirm("Are you sure you want to cancel appointment?");
        if (answer == true) {
            $http({
                url: "api/DeleteAppointment",
                method: "Post",
                data: JSON.stringify(id + "")
            })
   .then(function (response) {
       for (var i = 0; i < $scope.userData.User.Appointments.length ; i++) {
           if ($scope.userData.User.Appointments[i].Id === id) {
               $scope.userData.User.Appointments.splice(i, 1);
               break;
           }
       }
       $http({
           url: "api/GetMessages",
           method: "Post",
           data: JSON.stringify($scope.userData.User.Username)
       })
.then(function (response) {
    $scope.userData.User.Messages = response.data;
    $rootScope.data = $scope.userData;
});
   });
        }
    }
});