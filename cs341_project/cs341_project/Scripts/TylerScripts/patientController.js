cs341.controller('patientController', function ($state, $scope, $rootScope, $http) {
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");

    
    $scope.userData = $rootScope.data;

    if ($scope.userData == null || $scope.userData.Valid == false) {
        $("#main").fadeOut("slow",function(){
            $state.transitionTo('login', { arg: 'arg' });
        })
    }

    $scope.message = "Hello "+ $scope.userData.User.FirstName;


    $("#datepicker").datepicker();

    $scope.time = ['8:00AM - 9:00AM', '8:30AM - 9:30AM', '9:00AM - 10:00AM',
        '9:30AM - 10:30AM', '10:00AM - 11:00AM', '10:30AM - 11:30AM', '11:00AM - 12:00PM',
    '1:00PM - 2:00PM', '1:30PM - 2:30PM', '2:00PM - 3:00PM', '2:30PM - 3:30PM', '3:00PM - 4:00PM', '3:30PM - 4:30PM', '4:00PM - 5PM'];

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