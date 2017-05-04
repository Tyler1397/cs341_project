// Author: Tyler Timm
// Description: This class handles the functionality of the admin page
cs341.controller('adminController', function ($state, $scope, $rootScope, $http, $filter) {

    // visual effect
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");

    // switch to admin help guide
    document.getElementById("patientHelp").style.display = "none";
    document.getElementById("adminHelp").style.display = "block";

    // set active appointment to null
    $scope.active = '';

    // grab global data
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

    // function takes in an id representing a username and sets its status to deleted
    $scope.deleteUser = function (id) {
        var answer = confirm("Are you sure you want to delete " + id + "?");
        if (answer == true) {
            $http({
                url: "api/DeleteUser",
                method: "Post",
                data: JSON.stringify(id + "")
            })
   .then(function (response) {
       for (var i = 0; i < $scope.userData.User.Users.length; i++) {
           if ($scope.userData.User.Users[i].Username == id) {
               $scope.userData.User.Users[i].Status = "Deleted";
           }
       }
   });
        }
    }

    // function takes in a time var, then checks to see if the time is available, given the current selections
    $scope.userAvailable = function (time) {
        for (var i = 0; i < $scope.userData.User.Appointments.length; i++) {
            var temp = $scope.userData.User.Appointments[i];
            if (temp.Employee.Username === $scope.employee && temp.Time === time && temp.Date === $filter('date')($scope.date, 'MM/dd/yyyy')) {
                return false;
            }
            if (temp.Patient.Username === $scope.patient && temp.Time === time && temp.Date === $filter('date')($scope.date, 'MM/dd/yyyy')) {
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

    // function takes in an id and returns true if it is the active appointment
    $scope.hideInactive = function (id) {
        if (id === $scope.active) {
            return true;
        } else {
            return false;
        }
    }

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

    // function takes in an appointment object, and passes it into the web api to update the appointment, following that messages are refreshed
    $scope.updateAppointment = function (app) {
        var input = {
            Date: $filter('date')(app.Date, 'MM/dd/yyyy'),
            Patient: app.Patient.Username,
            Employee: app.Employee.Username,
            Status: "Changed",
            Title: app.Title,
            Time: app.Time,
            Id: app.Id
        };   
        $http({
            url: "api/UpdateAppointment",
            method: "Post",
            data: input
        })
   .then(function (response) {
       // remove the old appointment
       for (var i = 0; i < $scope.userData.User.Appointments.length; i++) {
           if ($scope.userData.User.Appointments[i].Id == input.Id) {
               $scope.userData.User.Appointments.splice(i, 1);
               break;
           }
       }
       // add new appointment to array and update global data
       $scope.userData.User.Appointments.push(response.data);
       $rootScope.data = $scope.userData;
       $scope.active = '';
       // update user messages
       $http({
           url: "api/GetMessages",
           method: "Post",
           data: JSON.stringify($scope.userData.User.Username)
       })
.then(function (response) {
    // update global data
    $scope.userData.User.Messages = response.data;
    $rootScope.data = $scope.userData;
});
   });
    }

    // welcome message
    $scope.message = "Hello " + $scope.userData.User.FirstName;

    // all allowable times for appointments
    $scope.times = ['8:00AM - 9:00AM', '9:00AM - 10:00AM', '10:00AM - 11:00AM', '11:00AM - 12:00PM', '1:00PM - 2:00PM', '2:00PM - 3:00PM', '3:00PM - 4:00PM', '4:00PM - 5PM'];

    // function grabs all info for a new appointment,checks to see if it is a valid appointment, and attempts to schedule it
    $scope.createAppointment = function () {
        var input = {
            Date: $filter('date')($scope.date, 'MM/dd/yyyy'),
            Patient: $scope.patient,
            Employee: $scope.employee,
            Status: "Active",
            Title: $scope.title,
            Time: $scope.time
        };

        $http({
            url: "api/AddAppointment",
            method: "Post",
            data: input
        })
   .then(function (response) {
       $scope.userData.User.Appointments.push(response.data);
       $rootScope.data = $scope.userData;
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


});
