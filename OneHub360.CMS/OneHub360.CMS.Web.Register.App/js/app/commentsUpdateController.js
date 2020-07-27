OneHub360.controller('commentsUpdateController',
    ['$scope', '$http', '$timeout', '$state', 'ngToast', 'id', '$uibModalInstance',
function ($scope, $http, $timeout, $state, ngToast, id, $uibModalInstance) {

    $scope.status = 0;

    $scope.loadComments = function () {
        if (id == null) {
            return;
        }

        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/comment/getmodel';

            $http.get(shareWebApiCallUrl)
                .success(function (data, status, headers, config) {
                    $scope.status = 1;
                    data.CreatedBy = HubStorage().getItem('currentUserId');
                    data.ThreadId = id;
                    data.FK_User = HubStorage().getItem('currentUserId');
                    $scope.commentData = data;
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        });

        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/comment/getall/' + id + '/' + HubStorage().getItem('currentUserId');

            $http.get(shareWebApiCallUrl)
                .success(function (data, status, headers, config) {
                    $scope.status = 1;
                    $scope.CommentsList = data;
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        });
    }

    $scope.AddComment = function()
    {
        
        var webApiCall = appContext.appServiceBaseUrl + '/api/feed/comment/add';

        $timeout(function () {
            $scope.status = 0;
            $http.post(
                webApiCall,
                JSON.stringify($scope.commentData),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            )
                .success(function (data) {
                    ngToast.create('تم إضافة التعليق بنجاح');
                    $scope.loadComments();
                    $scope.status = 1;
                })
                .error(function (data) {
                    $scope.status = 1;
                    ngToast.create('حدث خطأ');
                    console.log(data);
                });
        });
    }

    $scope.close = function (reload) {
        if (reload) {
            $state.go($state.current.name, $state.current.params, { reload: true });
            $uibModalInstance.close();
        }
        else {
            $uibModalInstance.dismiss();
        }
    }
}]);

