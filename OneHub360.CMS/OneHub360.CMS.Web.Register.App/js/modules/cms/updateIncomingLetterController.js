OneHub360.controller('updateIncomingLetterController', [
    '$scope', '$http', '$timeout', 'ngToast', '$state', function (
        $scope, $http, $timeout, ngToast, $state) {

        $scope.Init = function()
        {
            $scope.Id = $state.params.id;

            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/reg/incomingletter/' + $scope.Id;

            $timeout(function () {
                $scope.status = 0;
                $http.get(
                    webApiCall
                )
                    .success(function (data) {
                        $scope.letterData = data;
                        $scope.status = 1;
                    })
                    .error(function (data) {
                        $scope.status = -1;
                        ngToast.create('حدث خطأ');
                        console.log(data);
                    });
            });

        }

        $scope.Update = function()
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/reg/incomingletter/update';

            $timeout(function () {
                $scope.actionStatus = 0;
                $http.post(
                    webApiCall,
                    JSON.stringify($scope.letterData),
                    {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }
                )
                    .success(function (data) {

                        
                            ngToast.create('تم تعديل بيانات الكتاب بنجاح');
                        


                        
                    })
                    .error(function (data) {
                        
                        ngToast.create('حدث خطأ');
                        
                    });
            });
        }
    }]);