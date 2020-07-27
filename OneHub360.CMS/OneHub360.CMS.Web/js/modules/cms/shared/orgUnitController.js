OneHub360.controller('orgUnitController',
    [
        '$scope', '$http', '$timeout', '$rootScope', function (
            $scope, $http, $timeout, $rootScope) {

            $scope.status = 0;

            
            $scope.loadOrganization = function (orgId) {
                $scope.status = 0;
                if (orgId == null) {
                    return;
                }
              
                var orgInfo = $.grep($rootScope.externalUnitsSource, function (e) { return e.Id == orgId });
                $scope.OrgInfo = orgInfo[0];
                $scope.status = 1;
                //console.log($scope.OrgInfo);

                //$timeout(function () {
                //    var aviatorWebApiCallUrl = appContext.appServiceBaseUrl + '/api/user/aviator/' + userId;

                //    $http.get(aviatorWebApiCallUrl)
                //        .success(function (data, status, headers, config) {
                //            $scope.status = 1;
                //            $scope.UserInfo = data;
                //        })
                //        .error(function (data, status, headers, config) {
                //            console.log('Error loading user: ' + userId);
                //            console.log(data);
                //            $scope.status = -1;
                //            $scope.UserInfo = data;
                //        });
                //});
            }

        }]);
