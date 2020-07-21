var app = angular.module("Admin", ["ngRoute"]);

var appApiBaseUrl = '';

app.config(function ($routeProvider) {
        $routeProvider
        .when("/", {
            templateUrl: "home.html"
        })
        .when("/Organizations", {
            templateUrl: "Organizations.html"
        })
        .when("/Organization/:Id", {
            templateUrl: "Organization.html"
        })
        .when("/OrganizationUnits", {
            templateUrl: "OrganizationUnits.html"
        })
        .when("/JobTitles", {
            templateUrl: "JobTitles.html"
        })
        .when("/UsersInfo", {
            templateUrl: "UsersInfo.html"
        })
        .when("/Roles", {
            templateUrl: "Roles.html"
        })
        .when("/OrganizationTypes", {
            templateUrl: "OrganizationTypes.html"
        })
        .when("/OrganizationUnitTypes", {
            templateUrl: "OrganizationUnitTypes.html"
        });
});


app.controller('JobTitle', function ($scope, $http, $routeParams) {
    $http.get(appApiBaseUrl + 'api/JobTitle/').
        then(function (response) {
            $scope.jobTitles = response.data;
            console.log(response);
        });
});

app.controller('OrganizationUnit', function ($scope, $http, $routeParams) {
    $http.get(appApiBaseUrl + 'api/OrganizationUnit/').
        then(function (response) {
            $scope.organizationUnits = response.data;
        });
});

//Organization Controller
app.controller('Organization', function ($scope, $http, $routeParams) {
    $http.get(appApiBaseUrl + 'api/Organization/').
        then(function (response) {
            $scope.organizations = response.data;
        });

    $http.get(appApiBaseUrl + 'api/Organization/' + $routeParams.Id).
        then(function (response) {
            $scope.organization = response.data;
        });

    $http.put(appApiBaseUrl + 'api/Organization/' + $routeParams.Id);
});

app.controller('OrganizationType', function ($scope, $http, $routeParams) {
    $http.get(appApiBaseUrl + 'api/OrganizationType/').
        then(function (response) {
            $scope.organizationTypes = response.data;
        });
});

app.controller('OrganizationUnitType', function ($scope, $http, $routeParams) {
    $http.get(appApiBaseUrl + 'api/OrganizationUnitType/').
        then(function (response) {
            $scope.organizationUnitTypes = response.data;
        });

    $http.get(appApiBaseUrl + 'api/OrganizationUnitType/' + $routeParams.Id).
        then(function (response) {
            $scope.organizationUnitType = response.data;
        });
});