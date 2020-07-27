
OneHub360.controller('headerController', ['$scope', '$http', '$state', '$rootScope', function ($scope, $http, $state, $rootScope) {

    
    
    
        
        
        
   

    $scope.Search = function()
    {
        $state.go('search', { term: $scope.term });
        $scope.term = '';
    }

    }]);

    OneHub360.directive('header', function () {
        return {
            templateUrl: "/partials/app/header.html"
        };
    });
