/// <reference path="../angular/angular.min.js" />

var appContext =
    {
        appServiceBaseUrl: 'http://onehub360sp:363/',
        PageLength: 3,
        SupportedModes: ['user','register']
    };

var IncomingLetterStatus =
    {
        Registered: 31,
        Indexed: 32,
        Scanned: 33,
        ScannedIndexed: 34,
        All : -1
    }

function HubStorage()
{
    return sessionStorage;
}

OneHub360.run(function (amMoment) {
    amMoment.changeLocale('ar-sa');
});



//OneHub360.run(['$http', function ($http) {
//    if (sessionStorage.getItem('authorizationData')) {
//        $http.defaults.headers.common['Authorization'] = 'Bearer ' + sessionStorage.getItem('authorizationData');
//    }
//}]);


// States

OneHub360.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/feed/1');

    $stateProvider
      .state('profile', {
          url: "/me",
          templateUrl: "/partials/app/profile.html"
      });

    $stateProvider
      .state('login', {
          url: "/login/:mode/:message",
          templateUrl: "/partials/app/login.html"
      });

    $stateProvider
      .state('new', {
          url: "/new/:message",
          templateUrl: "/partials/app/newitem.html"
      });

    $stateProvider.state('feed', {
        url: "/feed/:option",
        templateUrl: "/partials/app/feed.html",
        data: { transition: 'fade-in' }
    });

    $stateProvider.state('incoming', {
        url: "/incoming",
        templateUrl: "/partials/modules/cms/incomingletters.html",
        data: { transition: 'fade-in' }
    });

    $stateProvider.state('outgoing', {
        url: "/outgoing",
        templateUrl: "/partials/modules/cms/outgoingletters.html",
        data: { transition: 'fade-in' }
    });

    $stateProvider.state('viewoutgoingletter', {
        url: "/ol/v/:id",
        templateUrl: "/partials/modules/cms/viewoutgoingletter.html"
    });

    $stateProvider.state('indexincomingletter', {
        url: "/indexincomingletter/:id",
        templateUrl: "/partials/modules/cms/indexincomingletter.html",
        data: { transition: 'fade-in' }
    });

    $stateProvider.state('search', {
        url: "/search",
        templateUrl: "/partials/app/search.html",
        data: { transition: 'fade-in' },
        params: { term: null, }
    });

    $stateProvider.state('dashboard', {
        url: "/dashboard",
        templateUrl: "/partials/app/dashboard.html"
    });

    $stateProvider.state("Modal.editdraftletter", {
        url: '/dm/editdraftletter/:id',
        views: {
            "modal": {
                templateUrl: "partials/modules/cms/createincomingletter.html"
            }
        }
    });

    $stateProvider.state("Modal", {
        views: {
            "modal": {
                templateUrl: "/partials/modal/modal.html"
            }
        },
        onEnter: ['$state', '$rootScope', function ($state, $rootScope) {
            $(document).on("keyup", function (e) {
                if (e.keyCode == 27) {
                    $(document).off("keyup");
                    $rootScope.goBack();
                }
            });
        }],
        abstract: true
    });

   

    $stateProvider.state('Modal.createincomingletter', {
        url: '/createincomingletter',
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createincomingletter.html"
            }
        }
    });
});

OneHub360.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider.state("Modal.sharing", {
        url:'/s/:id',
        views: {
            "modal": {
                templateUrl: "/partials/app/updatesharing.html",
                controller: function ($scope, $stateParams) {
                    $scope.feedItemId = $stateParams.id;
                }
            }
        },
    });

    $stateProvider.state("userdetails", {
        url: "/user",
        templateUrl: "/partials/app/user.html"
    });

    $stateProvider.state("Modal.comment", {
        url: '/c/:id',
        views: {
            "modal": {
                templateUrl: "/partials/app/commentsview.html",
                controller: function ($scope, $stateParams) {
                    $scope.feedItemId = $stateParams.id;
                }
            }
        },
    });

});

// Autocomplete search

OneHub360.run(['$rootScope', function ($rootScope) {

    $rootScope.GetCurrentUserInfo = function()
    {
        $rootScope.peopleSource
    }

    $rootScope.localSearch = function (str) {
        var matches = [];
        $rootScope.peopleSource.forEach(function (person) {
            
            if (person.Id.toLowerCase() == HubStorage().getItem('currentUserId'))
            {
                return;
            }
            if ((person.FullName.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0) ||
                (person.DisplayName.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0) ||
                (person.About.toLowerCase().indexOf(str.toString().toLowerCase()) >= 0))
            {
                matches.push(person);
            }
        });
        return matches;
    };
}]);

// Secure request

OneHub360.run(['$rootScope', '$http', function ($rootScope, $http) {
    $rootScope.makeSecureGet = function (getUrl) {
        var authData = HubStorage().getItem('authorizationData');

        return $http({
            method: 'GET', url: getUrl, headers: {
                'Authorization': 'Bearer ' + authData
            }
        });
    }

    $rootScope.makeSecurePost = function (postUrl) {
        var authData = HubStorage().getItem('authorizationData');

        return $http({
            method: 'POST', url: postUrl, headers: {
                'Authorization': 'Bearer ' + authData
            }
        });
    }

    $rootScope.makeSecurePost = function (postUrl, postData, contentType) {
        
        var authData = HubStorage().getItem('authorizationData');

        return $http.post(
            postUrl,
            postData,
            { method: 'POST', headers: { 'Content-Type': contentType, 'Authorization': 'Bearer ' + authData } }
            );
    }
}]);

// Session storage

OneHub360.run(['$rootScope', '$timeout', '$state', function ($rootScope, $timeout, $state) {
    $rootScope.isLoggedIn = true;
    $rootScope.$on('$stateChangeSuccess', function (ev, to, toParams, from, fromParams) {
        if (HubStorage().getItem('authorizationData') == null) {
            $rootScope.isLoggedIn = false;
            if (to.name == 'login') {
                return;
            }
            else {
                $state.go('login', { message: '401',mode : '' });
            }
        }
        else {
            $rootScope.isLoggedIn = true;
            if (to.name == 'login') {
                $state.go('feed', { option: '1' });
            }
        }
        $rootScope.backEnabled = from.abstract;
        $rootScope.goBack = function () {
            $state.go(from, fromParams);
        }

      

       

        closeNav();
    });

}]);

// IE Cache Disable

OneHub360.config(['$httpProvider', function ($httpProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};    
    }    

    // Answer edited to include suggestions from comments
    // because previous version of code introduced browser-related errors

    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);

OneHub360.config(['ngToastProvider', function (ngToastProvider) {
    ngToastProvider.configure({
        animation: 'fade', // or 'slide'
        dismissButton: true,
        horizontalPosition: 'center'
    });
}]);

// Used to enable the document vieweing in IFrame
OneHub360.config(function ($sceProvider) {
    $sceProvider.enabled(false);
});


$.datepicker.setDefaults($.datepicker.regional["ar"]);

OneHub360.run(['$rootScope', function ($rootScope) {

    $rootScope.IsUser = function () {
        if (HubStorage().Mode == 'user') {
            return true;
        }
        else {
            return false;
        }
    }
}]);