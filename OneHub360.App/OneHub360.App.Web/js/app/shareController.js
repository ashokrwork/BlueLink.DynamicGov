﻿OneHub360.controller('shareController',
    [
        '$scope', '$http', '$timeout', '$state', function (
            $scope, $http, $timeout, $state) {

    $scope.status = 0;

    $scope.feedItemId = '';

    $scope.loadShared = function (feedItemId) {

        $scope.feedItemId = feedItemId;

        if (feedItemId == null) {
            return;
        }

        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/getsharing/' + feedItemId;

            $http.get(shareWebApiCallUrl)
                .success(function (data, status, headers, config) {
                    $scope.status = 1;
                    $scope.ShareInfo = data;
                    $scope.SharingList = JSON.parse(data.SharedWith);
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                    $scope.status = -1;
                    $scope.ShareInfo = data;
                });
        });
    }

    $scope.openShareDialog = function (feedItemId)
    {
        $state.go('Modal.updatesharing', {
            id: feedItemId //selectedItem and id is defined
        });
        //$uibModal.open({
        //    controller: 'shareUpdateController',
        //    templateUrl: '/partials/app/updatesharing.html',
        //    resolve: {
        //        id: function () {
        //        return feedItemId;
        //        }
        //}
        //});
    }

}]);

