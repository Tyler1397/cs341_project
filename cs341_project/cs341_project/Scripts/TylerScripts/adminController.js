cs341.controller('adminController', function ($state, $scope, $rootScope) {
    $('#main').hide().fadeIn("slow");
    $('#loginLogout').text("Logout");


    $scope.userData = $rootScope.data;

    if ($scope.userData == null || $scope.userData.Valid == false) {
        $state.transitionTo('login', { arg: 'arg' });
    }

    $scope.message = "Hello " + $scope.userData.User.FirstName;
});