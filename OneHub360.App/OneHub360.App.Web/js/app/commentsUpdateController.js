OneHub360.controller('commentsUpdateController',
    ['$scope', '$http', '$timeout', '$state', 'ngToast', 
function ($scope, $http, $timeout, $state, ngToast) {

    var id = $state.params.id;

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

                    console.log(data);
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        });

        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/comment/getall/' + id + '/' + HubStorage().getItem('currentUserId');

            console.log(shareWebApiCallUrl);

            $http.get(shareWebApiCallUrl)
                .success(function (data, status, headers, config) {
                    $scope.status = 1;
                    $scope.CommentsList = data;
                    console.log(data);
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        });
    }

    $scope.AddComment = function()
    {
        
        var webApiCall = appContext.appServiceBaseUrl + '/api/feed/comment/add';

        console.log($scope.commentData);

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
            window.history.back();
        }
        else {
            window.history.back();
        }
    }

    $scope.getMoment = function(date)
    {
        return moment(date).fromNow();
    }
}]);

