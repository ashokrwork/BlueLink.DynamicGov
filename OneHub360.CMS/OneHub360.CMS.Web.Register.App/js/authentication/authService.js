'use strict';
OneHub360.factory('authService', ['$http', '$q', '$state', function ($http, $q, $state) {

    var authenticationServiceUrl = appContext.appServiceBaseUrl;

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
    };

    var _login = function (loginData) {

        
            var token = '';
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            //var deferred = $q.defer();

        $http.post('http://onehub360sp:363/' + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                token = response.access_token;

                //localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false })

                sessionStorage.setItem('authorizationData', response);

                _authentication.isAuth = true;
                _authentication.userName = response.userName;

                $state.go('feed');

            }).error(function (err, status) {
                _logOut();
            });
        

    };

    var _logOut = function () {

        sessionStorage.removeItem('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

        $state.go('login');

    };

    var _fillAuthData = function () {

        var authData = sessionStorage.getItem('authorizationData');
        console.log('Data field');

    };
    
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;

    return authServiceFactory;
}]);