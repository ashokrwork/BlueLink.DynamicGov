var cmsContext =
    {
        cmsServiceBaseUrl: 'http://cloudready:363/',
    oosWordViewerUrl: 'http://cloudready/wv/wordviewerframe.aspx?WOPISrc=',
        pdfJsViewUrl: '/pdfjs/web/viewer.html',
        PageLength:5
        
    };

var adminURL = 'http://cloudready:61190/';

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
            if ($rootScope.externalUnitsSource!= null)
            if ($rootScope.externalUnitsSource.length == 0) {
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