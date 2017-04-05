cs341.controller('adminController', function ($state, $scope, $rootScope,$http) {
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");


    $scope.userData = $rootScope.data;

    if ($scope.userData == null || $scope.userData.Valid == false) {
        $state.transitionTo('login', { arg: 'arg' });
    }

    $scope.changeAppointment = function (ele) {
        alert(ele);
    }

    $("#datepicker").datepicker();
    $scope.message = "Hello " + $scope.userData.User.FirstName;

    $scope.time = ['8:00AM - 9:00AM', '8:30AM - 9:30AM', '9:00AM - 10:00AM',
       '9:30AM - 10:30AM', '10:00AM - 11:00AM', '10:30AM - 11:30AM', '11:00AM - 12:00PM',
   '1:00PM - 2:00PM', '1:30PM - 2:30PM', '2:00PM - 3:00PM', '2:30PM - 3:30PM', '3:00PM - 4:00PM', '3:30PM - 4:30PM', '4:00PM - 5PM'];

    $scope.submit = function () {
        $('#datepicker').datepicker({ dateFormat: 'mm-dd-yyyy' });
        var test = $('#datepicker').val();
        var input = {
            Date: test,
            Patient: $scope.patient,
            Employee: $scope.employee,
            Approved: true,
            Cancelled: false,
            Notes: $scope.notes,
            Time: $scope.time
        };

        $http({
            url: "api/AddAppointment",
            method: "Post",
            data: input
        })
   .then(function (response) {
       $scope.userData.Admin.Appointments.push(response.data);
       $rootScope.data = $scope.userData;
   });
    }
});