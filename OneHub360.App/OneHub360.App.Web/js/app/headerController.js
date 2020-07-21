
OneHub360.controller('headerController', ['$scope', '$http', '$state', '$rootScope', '$cookies', '$window','$timeout',
    function ($scope, $http, $state, $rootScope, $cookies, $window, $timeout) {

    //var favoriteCookie = $cookies.get('theme_css');

    //document.getElementById('theme_css').href = favoriteCookie;
    $scope.changeTheme = function (themeurl)
    {
        //var now = new $window.Date(),
        //// this will set the expiration to 6 months
        //exp = new $window.Date(now.getFullYear(), now.getMonth() + 6, now.getDate());

        //$cookies.put('theme_css', themeurl, {
        //    expires: exp
        //});
        //document.getElementById('theme_css').href = themeurl;
    }
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
