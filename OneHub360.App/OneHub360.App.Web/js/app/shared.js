/// <reference path="../jquery/jquery-3.1.0.min.js" />
/// <reference path="../angular/angular.min.js" />

OneHub360.directive("peoplePicker", function ($compile, $http, $timeout, $rootScope) {
    //https://ghiden.github.io/angucomplete-alt/
    return {
        link: function (scope, element, attrs) {
            var template = '<div class="form-group">' +
                            //'<span class="input-group-addon"><i class="fa fa-user fa-fw"></i></span>' +
                            '<angucomplete-alt field-required-class="autocompleteRequired" field-required="' + attrs.reuired + '" input-class="form-control" text-no-results="لا يوجد أشخاص بالإسم المدخل" text-searching="جاري العمل..." placeholder="' + attrs.title + '" search-fields="FullName,DisplayName" local-data="peopleSource"  title-field="FullName" description-field="About" image-field="Picture" id="' + attrs.pickerid + '" selected-object="' + attrs.selectedobject + '" minlength="2"  remote-url-data-field="results" title-field="firstName"/>' +
                            //'<span data-toggle="tooltip" data-placement="top" ng-click="UpdatePeopleSource();" class="input-group-addon"><i class="fa fa-refresh fa-fw"></i></span>' +
                            '</div>' +
                            '</div>';
            if ($rootScope.peopleSource.length == 0)
            {
                $rootScope.UpdatePeopleSource();
            }
            var linkFn = $compile(template);
            var content = linkFn(scope);
            element.append(content);
        }
    }
});

OneHub360.directive("peoplepickerCurrent", function ($compile, $http, $timeout, $rootScope) {
    return {
        link: function (scope, element, attrs) {
            var template = '<div class="input-group">' +
                            '<span class="input-group-addon"><i class="fa fa-user fa-fw"></i></span>' +
                            '<angucomplete-alt local-search="localSearch" field-required-class="autocompleteRequired" field-required="' + attrs.reuired + '" input-class="form-control" text-no-results="لا يوجد أشخاص بالإسم المدخل" text-searching="جاري العمل..." placeholder="' + attrs.title + '" local-data="peopleSource" title-field="FullName" description-field="DisplayName" image-field="Picture" id="' + attrs.pickerid + '" selected-object="' + attrs.selectedobject + '" minlength="2"  title-field="firstName"/>' +
                            '<span data-toggle="tooltip" data-placement="top" ng-click="UpdatePeopleSource();" class="input-group-addon"><i class="fa fa-refresh fa-fw"></i></span>' +
                            '</div>' +
                            '</div>';
            if ($rootScope.peopleSource.length == 0) {
                $rootScope.UpdatePeopleSource();
            }
            var linkFn = $compile(template);
            var content = linkFn(scope);
            element.append(content);
        }
    }
});

OneHub360.directive("backButton", function ($rootScope, $compile) {
    return {
        template: '<button class="close" ng-disabled="backEnabled" ng-click="goBack();"><i class="fa fa-arrow-circle-o-left fa-3" aria-hidden="true"></i></button>',
    }
});

OneHub360.directive("loading", function () {
    return {
        template: '<center><div id="mainLoadingDiv" class="page-loader-wrapper"><div class="loader"><div class="m-t-30"><img class="zmdi-hc-spin" src="/assets/images/logo.svg" width="48" height="48" alt="InfiniO"></div><p>جاري تحميل النظام</p></div></div></center>'
    }
});

OneHub360.directive("smallloading", function () {
    return {
        template: '<i class="fa fa-spinner fa-pulse fa-3x fa-fw"></i>'
    }
});

OneHub360.directive("feedActions", function () {
    return {
        scope: { loadFeedActions: '&callbackFn' },
        link: function (scope, element, attrs) {
            attrs.$observe('threadid', function (newValue) {
                if (newValue) {
                    scope.loadFeedActions(newValue);
                    scope.id = newValue;
                }
            });
        },
        templateUrl: "/partials/app/feedactions.html",
        controller: "feedActionsController"
    };
});

OneHub360.directive('aviatortimeline', function ($http,$timeout) {
    return {
        scope: { loadUser: '&callbackFn' },
        link: function (scope, element, attrs) {
            if (attrs.mode == 'current') {
                scope.loadUser(HubStorage().getItem('currentUserId'));
            }
            else {
                var userObserver =
                attrs.$observe('userid', function (newValue) {
                    if (newValue) {
                        scope.loadUser(newValue);
                        userObserver();
                    }
                });
            }
        },
        templateUrl: "/partials/app/aviatortimeline.html",
        controller: "avaitorController"
    };
});

OneHub360.directive('aviatorcurrent', function ($http, $timeout) {
    return {
        templateUrl: "/partials/app/aviatorcurrent.html",
        controller: "avaitorController"
    };
});

OneHub360.directive('aviatoroneline', function ($http, $timeout) {
    return {
        scope: { loadUser: '&callbackFn' },
        link: function (scope, element, attrs) {
            if (attrs.mode == 'current') {
                scope.loadUser(HubStorage().getItem('currentUserId'));
            }
            else {
                var userObserver =
                attrs.$observe('userid', function (newValue) {
                    if (newValue) {
                        scope.loadUser(newValue);
                        userObserver();
                    }
                });
            }
        },
        templateUrl: "/partials/app/aviatoroneline.html",
        controller: "avaitorController"
    };
});

OneHub360.directive('aviatormulti', function ($http, $timeout) {
    return {
        scope: { loadUser: '&callbackFn' },
        link: function (scope, element, attrs) {
            if (attrs.mode == 'current') {
                scope.loadUser(HubStorage().getItem('currentUserId'));
            }
            else {
                var userObserver =
                attrs.$observe('userid', function (newValue) {
                    if (newValue) {
                        scope.loadUser(newValue);
                        userObserver();
                    }
                });
            }
        },
        templateUrl: "/partials/app/aviatormulti.html",
        controller: "avaitorController"
    };
});

OneHub360.directive('aviatorselecteduser', function ($http, $timeout) {
    return {
        scope: { loadUser: '&callbackFn' },
        link: function (scope, element, attrs) {
            if (attrs.mode == 'current') {
                scope.loadUser(HubStorage().getItem('currentUserId'));
            }
            else {
                var userObserver =
                attrs.$observe('userid', function (newValue) {
                    if (newValue) {
                        scope.loadUser(newValue);
                        userObserver();
                    }
                });
            }
        },
        templateUrl: "/partials/app/aviatorselecteduser.html",
        controller: "avaitorController"
    };
});

OneHub360.directive('feedShare', function ($http, $timeout) {
    return {
        scope: { loadShared: '&callbackFn' },
        link: function (scope, element, attrs) {
            attrs.$observe('feeditemid', function (newValue) {
                if (newValue) {
                    scope.loadShared(newValue);
                    scope.id = newValue;
                }
            });
        },
        templateUrl: "/partials/app/sharebutton.html",
        controller: "shareController"
    };
});

OneHub360.directive('feedComment', function ($http, $timeout) {
    return {
        scope: { loadComments: '&callbackFn' },
        link: function (scope, element, attrs) {
            attrs.$observe('threadid', function (newValue) {
                if (newValue) {
                    scope.loadComments(newValue);
                    scope.threadid = newValue;
                }
            });
        },
        templateUrl: "/partials/app/commentbutton.html",
        controller: "commentController"
    };
});