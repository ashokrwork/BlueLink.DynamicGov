OneHub360.controller('shareUpdateController',
    ['$scope', '$http', '$timeout', '$state', 'ngToast', 'id', '$uibModalInstance',
    function ($scope, $http, $timeout, $state, ngToast, id, $uibModalInstance) {
        
        

    $scope.status = 0;

    $scope.SharingList = [];
    $scope.ShareInfo = new Object();

    $scope.loadShared = function () {
        
        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/getsharing/' + id;

            $http.get(shareWebApiCallUrl)
                .success(function (data, status, headers, config) {
                    $scope.status = 1;
                    $scope.ShareInfo = data;
                    $scope.SharingList = $.parseJSON(data.SharedWith);
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                    $scope.status = -1;
                });
        });
    }

    $scope.addSharing = function(selectedUser)
    {
        console.log($scope.SharingList.indexOf(selectedUser.originalObject.Id));
        if (selectedUser != null) {
            if ($scope.SharingList.indexOf(selectedUser.originalObject.Id) == -1) {
                $scope.SharingList.push(selectedUser.originalObject.Id);
            }
            else
            {
                console.log('User selected before');
            }
        }
    }

    $scope.removeSharing = function(index)
    {
        $scope.SharingList.splice(index, 1);
    }

    $scope.updateSharing = function ()
    {
        $scope.ShareInfo.SharedWith = JSON.stringify($scope.SharingList);

        $timeout(function () {
            var shareWebApiCallUrl = appContext.appServiceBaseUrl + '/api/feed/updatesharing';

            $http.post(shareWebApiCallUrl, JSON.stringify($scope.ShareInfo),
                    {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                .success(function (data, status, headers, config) {
                    $scope.status = 1;
                    ngToast.create('تم الحفظ بنجاح');
                    $scope.close(true);
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                    $scope.status = -1;
                    $scope.ShareInfo = data;
                });
        });
    }

    $scope.close = function(reload)
    {
        if(reload)
        {
            $state.go($state.current.name, $state.current.params, { reload: true });
            $uibModalInstance.close();
        }
        else
        {
            $uibModalInstance.dismiss();
        }
    }

}]);

