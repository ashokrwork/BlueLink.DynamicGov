app.controller('indicators', function
    ( $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();
        $scope.indicatorvalue = '1';
        $http({
            method: 'GET',
            url: appApiBaseUrl + 'api/indicators/get'
        }).then(function (response) {
            $scope.indicatorvalue = response.data;
            if ($scope.indicatorvalue == 1)
                $scope.indicatortext = 'جميع الوحدات';
            if ($scope.indicatorvalue == 2)
                $scope.indicatortext = ' الهيكل التنظيمي';
        });
        // Update Organization details
        $scope.update = function () {
            $scope.loading = true;
            {
              
                    var confirm = $mdDialog.confirm()
              .title('تعديل بيانات')
              .textContent('هل تريد حقا تعديل البيانات ؟')
              .ok('نعم')
              .cancel('لا');
                    $mdDialog.show(confirm).then(function () {
                        if ($scope.indicatorvalue == 1)
                            $scope.indicatortext = 'عام';
                        if ($scope.indicatorvalue == 2)
                            $scope.indicatortext = 'مؤسسي';
                        $http({
                            method: 'GET',
                            url: appApiBaseUrl + 'api/indicators/update?value=' + $scope.indicatorvalue,
                        }).then(function successCallback(response) {
                            $scope.clear();
                            $scope.toast('تمت العملية بنجاح');

                        }, function errorCallback(response) {
                            $scope.toast("Error : " + response.data.ExceptionMessage);
                        });
                    });
                }
            
        }
    
});