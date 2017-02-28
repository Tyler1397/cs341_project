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
});