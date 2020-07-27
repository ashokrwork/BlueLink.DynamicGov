var cmsContext =
    {
        cmsServiceBaseUrl: 'http://cloudready:363/',
    oosWordViewerUrl: 'http://cloudready/wv/wordviewerframe.aspx?WOPISrc=',
        pdfJsViewUrl: '/pdfjs/web/viewer.html'
    };
var adminURL = 'http://cloudready:61190/';

OneHub360.config(function ($stateProvider, $urlRouterProvider) {
    
    $stateProvider.state("Modal.createdraftmemo", {
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createdraftmemo.html"
            }
        }
    });

    $stateProvider.state("Modal.editdraftmemomultisend", {
        url: '/dm/editdraftmemomultisend/:id',
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/forms/managemultisend.html"
            }
        }
    });

    $stateProvider.state("Modal.editdraftmemo", {
        url: '/dm/editdraftmemo/:id',
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createdraftmemo.html"
            }
        }
    });

    $stateProvider.state("createdraftmemo2", {
        url: "/df/create",
        templateUrl: "/partials/modules/cms/createdraftmemo.html"
    });

    $stateProvider.state("Modal.createdraftletter", {
        url:'/cdl',
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createdraftletter.html"
            }
        }
    });

    $stateProvider.state("Modal.createdraftletter.threadid", {
        url: '/:threadid',
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createdraftletter.html"
            }
        }
    });

    $stateProvider.state("Modal.editdraftletter", {
        url: '/dm/editdraftletter/:id',
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createdraftletter.html"
            }
        }
    });
    $stateProvider.state("Modal.createincomingletter", {
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/createincomingletter.html"
            }
        }
    });

    $stateProvider.state("Modal.webscan", {
        views: {
            "modal": {
                templateUrl: "/partials/modules/cms/addons/webscan.html"
            }
        }
    });

    $stateProvider
      .state('viewdraftmemo', {
          url: "/dm/v/:id",
          templateUrl: "/partials/modules/cms/viewdraftmemo.html"
      });

    $stateProvider
      .state('viewdraftletter', {
          url: "/dl/v/:id",
          templateUrl: "/partials/modules/cms/viewdraftletter.html"
      });

    $stateProvider
      .state('viewoutgoingletter', {
          url: "/ol/v/:id",
          templateUrl: "/partials/modules/cms/viewoutgoingletter.html"
      });

    $stateProvider
      .state('viewincomingletter', {
          url: "/il/v/:id",
          templateUrl: "/partials/modules/cms/viewincomingletter.html"
      });

    $stateProvider
     .state('viewoutgoingmemo', {
         url: "/om/v/:id",
         templateUrl: "/partials/modules/cms/viewoutgoingmemo.html"
     });

    $stateProvider
     .state('viewincominggmemo', {
         url: "/im/v/:id",
         templateUrl: "/partials/modules/cms/viewincomingmemo.html"
     });
});

var externalUnitsSource = [];

OneHub360.run(['$rootScope', '$timeout', function ($rootScope, $timeout) {

    var externalUnitsCacheServiceUrl = adminURL + 'api/organizationunit/getall';

    $.ajax({
        url: externalUnitsCacheServiceUrl,
        type: 'GET',
        success: function (data) {
            externalUnitsSource = data;
            $rootScope.externalUnitsSource = externalUnitsSource;
        },
        error: function (data) {
            console.log('Error');
            console.log(data);
        }
    });

    
    $rootScope.UpdateExternalUnitsSource = function () {
        var externalUnitsCacheServiceUrl = adminURL + 'api/organizationunit/getall';

        $timeout(function () {
            $rootScope.makeSecureGet(externalUnitsCacheServiceUrl).success(
                function (data, textStatus, jqXHR) {
                    $rootScope.externalUnitsSource = data;
                }
                );
        });
    }
}]);

OneHub360.directive("externalUnitsPicker", function ($compile, $http, $timeout, $rootScope) {
    return {
        link: function (scope, element, attrs) {
            var template = '<div class="input-group">' +
                            '<span class="input-group-addon"><i class="fa fa-building fa-fw"></i></span>' +
                            '<angucomplete-alt field-required-class="autocompleteRequired" field-required="' + attrs.reuired + '" input-class="form-control" text-no-results="لا يوجد جهات بالإسم المدخل" text-searching="جاري العمل..." placeholder="' + attrs.title + '" search-fields="Title,PersonTitle" local-data="externalUnitsSource"  title-field="PersonTitle" description-field="Title" image-field="LogoUrl" id="' + attrs.pickerid + '" selected-object="' + attrs.selectedobject + '" minlength="2"  remote-url-data-field="results"/>' +
                            '<span data-toggle="tooltip" data-placement="top" ng-click="UpdateExternalUnitsSource();" class="input-group-addon btn"><i class="fa fa-refresh fa-fw"></i></span>' +
                            '</div>' +
                            '</div>';
            if ($rootScope.externalUnitsSource == null) {
                $rootScope.UpdateExternalUnitsSource();
            }
            var linkFn = $compile(template);
            var content = linkFn(scope);
            element.append(content);
        }
    }
});

OneHub360.directive('orgunittileline', function ($http, $timeout) {
    return {
        scope: { loadOrganization: '&callbackFn' },
        link: function (scope, element, attrs) {
           
                var orgObserver =
                attrs.$observe('orgid', function (newValue) {
                    if (newValue) {
                        scope.loadOrganization(newValue);
                        orgObserver();
                    }
                });
            
        },
        templateUrl: "/partials/modules/cms/shared/externalorgtimeline.html",
        controller: "orgUnitController"
    };
});

OneHub360.directive('orgunitoneline', function ($http, $timeout) {
    return {
        scope: { loadOrganization: '&callbackFn' },
        link: function (scope, element, attrs) {
            
            var orgObserver =
                attrs.$observe('orgid', function (newValue) {
                    if (newValue) {
                        scope.loadOrganization(newValue);
                        orgObserver();
                    }
                });
        },
        templateUrl: "/partials/modules/cms/shared/externalorgoneline.html",
        controller: "orgUnitController"
    };
});
OneHub360.directive('aviatorselectedorg', function ($http, $timeout) {
    return {
        scope: { loadOrganization: '&callbackFn' },
        link: function (scope, element, attrs) {

            var orgObserver =
                attrs.$observe('orgid', function (newValue) {
                    if (newValue) {
                        scope.loadOrganization(newValue);
                        orgObserver();
                    }
                });
        },
        templateUrl: "/partials/modules/cms/shared/aviatorselectedorg.html",
        controller: "orgUnitController"
    };
});