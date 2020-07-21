OneHub360.controller('avaitorController',
    [
        '$scope', '$http', '$timeout', '$rootScope', function (
            $scope, $http, $timeout, $rootScope) {

            $scope.status = 0;

            $scope.initCurrent = function () {

                var watcher = $scope.$watch(function () {
                    return HubStorage().currentUserId;
                }, function (newVal, oldVal) {
                    var userInfo = $.grep($rootScope.peopleSource, function (e) { return e.Id == newVal });
                   
                    if (userInfo.length != 0)
                    {
                        
                        $scope.UserInfo = userInfo[0];
                        $scope.status = 1;
                       
                    }
                    
                }, true);
            }
            $scope.loadUser = function (userId) {
                $scope.status = 0;
                if (userId == null) {
                    return;
                }
                var userInfo = $.grep($rootScope.peopleSource, function (e) { return e.Id == userId });
                $scope.UserInfo = userInfo[0];
                $scope.status = 1;

                //$scope.UserInfo.Picture = $http.get(appContext.appServiceBaseUrl + 'api/user/picture/' + HubStorage().currentUserId).success(function (result) {
                //    $scope.UserInfo.Picture = result;

                //});
                  
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

