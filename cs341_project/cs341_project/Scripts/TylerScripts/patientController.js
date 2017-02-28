cs341.controller('patientController', function ($scope) {

    $scope.message = 'Tyler';

    $("#datepicker").datepicker();

    $scope.future = [{
        dr: 'Dr. Timm (Dentist)',
        title: 'Appointment 1',
        date: '12/15/2016'
    }, {
        dr: 'Dr. Goehrig (Hygenist)',
        title: 'Appointment 2',
        date: '9/1/2016'
    }, {
        dr: 'Dr. Joe (Dentist)',
        title: 'Appointment 3',
        date: '4/6/2016'
    }
    ]

    $scope.past = [{
        dr: 'Dr. Wolfe (Dentist)',
        title: 'Root Canal',
        date: '12/15/2016'
    }, {
        dr: 'Dr. Goehrig (Hygenist)',
        title: 'Cleaning',
        date: '9/1/2016'
    }
    ]
    $scope.time = ['8:00AM - 9:00AM', '8:30AM - 9:30AM', '9:00AM - 10:00AM',
        '9:30AM - 10:30AM', '10:00AM - 11:00AM', '10:30AM - 11:30AM', '11:00AM - 12:00PM',
    '1:00PM - 2:00PM', '1:30PM - 2:30PM', '2:00PM - 3:00PM', '2:30PM - 3:30PM', '3:00PM - 4:00PM', '3:30PM - 4:30PM', '4:00PM - 5PM'];
});