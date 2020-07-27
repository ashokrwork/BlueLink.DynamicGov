OneHub360.controller('feedActionsController',
    ['$scope', '$http', '$timeout', function
        ($scope, $http, $timeout)
    {
        $scope.status = 0;

        $scope.threadId = '';

        $scope.loadFeedActions = function (ThreadId) {

            $scope.threadId = ThreadId;

            if (ThreadId == null) {
                return;
            }

            $timeout(function () {
                var webApiCall = appContext.appServiceBaseUrl + 'api/feed/actions/' + $scope.threadId;

                $http.get(webApiCall)
                    .success(function (data, status, headers, config) {
                        $scope.status = 1;
                        $scope.actionsData = data; 
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                        $scope.status = -1;
                    });
            });
        }
    }]);