var app = angular.module("Admin", ["ngRoute", "ngMaterial", 'smart-table','ngCookies', "ngFileUpload"]);
 
var appApiBaseUrl = 'http://cloudready:61190/';
app.config(function ($routeProvider) {
    $routeProvider
    .when("/", { templateUrl: "home.html" })
    .when("/login", { templateUrl: "login.html" })
    .when("/Organizations", { templateUrl: "Organizations.html" })
    .when("/Organization/:Id", { templateUrl: "Organization.html" })
    .when("/OrganizationUnits", { templateUrl: "OrganizationUnits.html" })
    .when("/OrganizationTypes", { templateUrl: "OrganizationTypes.html" })
    .when("/JobTitles", { templateUrl: "JobTitles.html" })
    .when("/UsersInfo", { templateUrl: "UsersInfo.html" })
    .when("/Roles", { templateUrl: "Roles.html" })
    .when("/Groups", { templateUrl: "Groups.html" })
    .when("/Delegates", { templateUrl: "Delegates.html" })
    .when("/OrganizationTypeIDs", { templateUrl: "OrganizationTypeIDs.html" })
    .when("/OrganizationUnitTypes", { templateUrl: "OrganizationUnitTypes.html" })
    .when("/indicators", { templateUrl: "indicators.html" })
    .when("/Configuration", { templateUrl: "Configuration.html" });

});

app.run(function ($location,$rootScope, $cookies, $window) {
    $rootScope.Checkauth = function () {
        var userID = $cookies.get('userID');
        if(!userID) 
            $window.location = "#/login"; 
    };
    $rootScope.opennew = function () {
        $rootScope.organizationID = null;
        $location.path('/OrganizationUnits');
    };
});
app.config(function ($mdDateLocaleProvider) {
    $mdDateLocaleProvider.formatDate = function (date) {
        return moment(date).format('dd-MM-YYYY');
    };
});


