OneHub360.controller('commentController',
    [
        '$scope', '$http', '$timeout', '$state', function ($scope, $http, $timeout, $state) {

    $scope.status = 0;

    $scope.threadid = '';

    $scope.loadComments = function (threadid) {

        $scope.threadid = threadid;

        if (threadid == null) {
            return;
        }

        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/comment/getcount/' + threadid + '/' + HubStorage().currentUserId;

            $http.get(shareWebApiCallUrl)
                .success(function (data, status, headers, config) {
                    $scope.CommentsCount = data;
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                    $scope.CommentsCount = 'x';
                });
        });
    }

    $scope.openCommentsDialog = function (threadid) {

        $state.go('Modal.updatecomments', {
            id: threadid //selectedItem and id is defined
        });

        //$uibModal.open({
        //    controller: 'commentsUpdateController',
        //    templateUrl: '/partials/app/commentsview.html',
        //    resolve: {
        //        id: function () {
        //            return threadid;
        //        }
        //    }
        //});
    }

}]);

