OneHub360.controller('sideNavController', ['$scope', '$http', '$state', '$rootScope', function ($scope, $http, $state, $rootScope) {



    $scope.$watch(function () { return HubStorage().Mode; }, function (newVal, oldVal) {
        
            $scope.Mode = newVal;
        
    });



}]);

OneHub360.directive('sidenav', function () {
    return {
        templateUrl: "/partials/app/sidenav.html"
    };
});