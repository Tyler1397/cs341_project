cs341.controller('patientController', function ($scope, $rootScope) {
    $scope.userData = $rootScope.data;

    $scope.message = "Hello "+$scope.userData.User.FirstName;


    $("#datepicker").datepicker();

    $scope.time = ['8:00AM - 9:00AM', '8:30AM - 9:30AM', '9:00AM - 10:00AM',
        '9:30AM - 10:30AM', '10:00AM - 11:00AM', '10:30AM - 11:30AM', '11:00AM - 12:00PM',
    '1:00PM - 2:00PM', '1:30PM - 2:30PM', '2:00PM - 3:00PM', '2:30PM - 3:30PM', '3:00PM - 4:00PM', '3:30PM - 4:30PM', '4:00PM - 5PM'];
});