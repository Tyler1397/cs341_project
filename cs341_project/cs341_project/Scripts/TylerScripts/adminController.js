cs341.controller('adminController', function ($scope,$rootScope) {
    $('#main').hide().fadeIn("slow");
    $scope.userData = $rootScope.data;
    $scope.message = 'Hello '+$scope.userData.User.FirstName;

});