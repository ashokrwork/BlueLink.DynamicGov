/// <reference path="../angular/angular.min.js" />




$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


var OneHub360 = angular.module('OneHub360',
    [
        'ui.router',
        'angucomplete-alt',
        'ngToast',
        'angular-storage',
        'angular.filter',
        'angularMoment',
        'ngFileUpload',
        'ui.bootstrap',
        'notification',
        'angularUtils.directives.dirPagination',
        'ngCookies',
        'daterangepicker'
    ]);

OneHub360.run(['$rootScope', '$timeout', function ($rootScope, $timeout) {
    
    $rootScope.peopleSource = peopleSource;
    $rootScope.UpdatePeopleSource = function () {
        var peoplePickerCacheServiceUrl = appContext.appServiceBaseUrl + 'api/user/Smartautocomplete?userid=' + HubStorage().currentUserId;

        $timeout(function () {
            $rootScope.makeSecureGet(peoplePickerCacheServiceUrl).success(
                function (data, textStatus, jqXHR) {
                    $rootScope.peopleSource = data;
                    
                }
                );
        });
    }
}]);

OneHub360.factory('Globals', function () {
    return {
        isIE: bowser.msie
    };
});

// Intro

function StartIntro()
{
    

    introJs().setOptions(
        {
            'prevLabel': '<i class="fa fa-step-forward" aria-hidden="true"></i>',
            'nextLabel': '<i class="fa fa-step-backward" aria-hidden="true"></i>',
            'skipLabel': '<i class="fa fa-times-circle" aria-hidden="true"></i>',
            'doneLabel': '<i class="fa fa-times-circle" aria-hidden="true"></i>',
            'showStepNumbers': false,
            'keyboardNavigation': true,
            'scrollToElement': false,
            'overlayOpacity': 0.1,
            'disableInteraction': true,
            'showBullets': false,
            'showProgress': true
        }
        ).start();
}
