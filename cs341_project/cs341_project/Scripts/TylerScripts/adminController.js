cs341.controller('adminController', function ($state, $scope, $rootScope, $http, $filter) {
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");

    $scope.active = '';

    $scope.userData = $rootScope.data;

    // if the user trys to access page and they havent logged in, redirect to login page
    if ($scope.userData == null || $scope.userData.Valid == false) {
        $state.transitionTo('login', { arg: 'arg' });
    }

    //function takes in an appointment id and sets it to active, if it was already active, hide it
    $scope.changeAppointment = function (id) {
        if ($scope.active === id) {
            $scope.active = '';
        } else {
            $scope.active = id;
        }
    }

    // function takes in a time var, then checks to see if the time is available, given the current selections
    $scope.employeeAvailable = function (time) {
        for (var i = 0; i < $scope.userData.Admin.Appointments.length; i++) {
            var temp = $scope.userData.Admin.Appointments[i];
            if (temp.Employee === $scope.employee && temp.Time === time && temp.Date === $filter('date')($scope.date, 'MM/dd/yyyy')) {
                return false;
            }
        }
        return true;
    }

    // function takes in an appointment id and deletes it from the database
    $scope.deleteAppointment = function (id) {
        var answer = confirm("Are you sure you want to cancel appointment?");
        if (answer == true) {
            $http({
                url: "api/DeleteAppointment",
                method: "Post",
                data: id + ""
            })
   .then(function (response) {
       for (var i = 0; i < $scope.userData.Admin.Appointments.length ; i++) {
           if ($scope.userData.Admin.Appointments[i].Id === id) {
               $scope.userData.Admin.Appointments.splice(i, 1);
               return;
           }
       }
   });
        }
    }

    // function takes in an appointment id and updates it in the database
    $scope.updateAppointment = function (id) {
        alert($scope.changeTime + " time");
    }

    // function takes in an id and returns true if it is the active appointment
    $scope.hideInactive = function (id) {
        if (id === $scope.active) {
            return true;
        } else {
            return false;
        }
    }

    // welcome message
    $scope.message = "Hello " + $scope.userData.User.FirstName;

    // all allowable times for appointments
    $scope.time = ['8:00AM - 9:00AM','9:00AM - 10:00AM','10:00AM - 11:00AM','11:00AM - 12:00PM','1:00PM - 2:00PM','2:00PM - 3:00PM','3:00PM - 4:00PM', '4:00PM - 5PM'];

    // function grabs all info for a new appointment,checks to see if it is a valid appointment, and attempts to schedule it
    $scope.createAppointment = function () {
        var input = {
            Date: $filter('date')($scope.date, 'MM/dd/yyyy'),
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
