'use strict';
OneHub360.factory('authInterceptorService',
    ['$q', '$injector', function ($q, $injector) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {



        config.headers = config.headers || {};
       
        var authData = sessionStorage.getItem('authorizationData');

        console.log(authData.access_token);

        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.access_token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var authData = sessionStorage.getItem('authorizationData');

            if (authData) {
                
                    return $q.reject(rejection);
                
            }
            authService.logOut();
            
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);