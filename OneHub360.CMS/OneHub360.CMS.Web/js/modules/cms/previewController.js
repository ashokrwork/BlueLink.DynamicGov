OneHub360.controller('previewController',
    ['$scope', '$http', '$timeout', 'ngToast', 'itemData', '$uibModalInstance', function
        ($scope, $http, $timeout, ngToast, itemData, $uibModalInstance) {


        var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/correspondence/preview/' + itemData.Id;

        console.log(itemData);

        $timeout(function () {
            $http.get(webApiCall)
               .success(function (data, status, headers, config) {
                   $scope.status = 1;
                   console.log(data);
                   $scope.PreviewUrl = data;
               })
               .error(function (data, status, headers, config) {
                   $scope.status = -1;
                   console.log(data);
               });
        });

        $scope.close = function (reload) {
            
                $uibModalInstance.dismiss();
        }

    }]);