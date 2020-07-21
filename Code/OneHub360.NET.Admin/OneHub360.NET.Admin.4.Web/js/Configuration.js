//Configuration Controller
app.controller('Configuration', function (ConfigurationService,$rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();

    $scope.loading = null;
    $scope.ConfigurationData = null;
    $scope.listmodeConfiguration = 1;

    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    ConfigurationService.GetAllRecords().then(function (response) {
        $scope.ConfigurationData = response.data; // Success
        $scope.rowCollection = $scope.ConfigurationData;
        //console.log($scope.ConfigurationData);
        $scope.listmodeConfiguration = 1;
    }, function () {
        $mdDialog.show(
        $mdDialog.alert()
          .clickOutsideToClose(true)
          .title('خطأ')
          .textContent('خطأ في تحميل البيانات من فضلك حاول مرة اخرى')
          .ok('موافق')
      );
    });
    $scope.loading = false;

    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };
    $scope.Configuration = {
        Id: '',
        ConfigurationName: '', ConfigurationValue: '',  ConfigurationGroup: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset Configurationdetails
    $scope.clear = function () {
        $scope.Configuration.Id = '';
        $scope.Configuration.ConfigurationName = '';
        $scope.Configuration.ConfigurationValue = '';
        $scope.Configuration.ConfigurationGroup = '';
        $scope.Configuration.CreatedBy = '';
        $scope.Configuration.CreationDate = '';
        $scope.Configuration.LastModified = '';
        $scope.Configuration.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;
        if ($scope.Configuration.ConfigurationName != "") {
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/Configuration/Create/',
                data: $scope.Configuration
            }).then(function successCallback(response) {
                $scope.toast('تمت العملية بنجاح');
                $window.location.reload();

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            $scope.toast('من فضلك ادخل كل البيانات');
            $scope.updateitemConfiguration = 0;
            $scope.listmodeConfiguration = 0;
            $scope.newitemConfiguration = 1;
        }
        $scope.loading = false;
    };
    // Edit Configurationdetails
    $scope.edit = function (data) {
        $scope.Configuration = {
            Id: data.Id, ConfigurationName: data.ConfigurationName,
            ConfigurationValue: data.ConfigurationValue,  ConfigurationGroup: data.ConfigurationGroup,
            CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified,
            LastModifiedBy: data.LastModifiedBy
        };
        $scope.updateitemConfiguration = 1;
        $scope.listmodeConfiguration = 0;
        $scope.newitemConfiguration = 0;
    }

    // Cancel Configurationdetails
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitemConfiguration = 0;
        $scope.listmodeConfiguration = 1;
        $scope.newitemConfiguration = 0;
    }
    // Cancel Configurationdetails
    $scope.New = function () {
        $scope.clear();
        $scope.updateitemConfiguration = 0;
        $scope.listmodeConfiguration = 0;
        $scope.newitemConfiguration = 1;
    }
    $scope.Filter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
        if ($scope.keyword != null) {
            ConfigurationService.Filter($scope.keyword).then(function (response) {
                $scope.ConfigurationData = response.data; // Success

            })
        }
        else
            $scope.toast('من فضلك ادخل كلمة او اكثر للبحث في البيانات');

    }
    $scope.clearfilter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;

        ConfigurationService.GetAllRecords().then(function (response) {
            $scope.ConfigurationData = response.data; // Success

        })
    };
    // Update Configurationdetails
    $scope.update = function () {
        $scope.loading = true;

        if ($scope.Configuration.ConfigurationName != "") {
            //$scope.listmodeConfiguration = 1;
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/Configuration/Update?id=' + $scope.Configuration.Id,
                    data: $scope.Configuration
                }).then(function successCallback(response) {
                    $scope.toast('تمت العملية بنجاح');
                    $window.location.reload();


                }, function errorCallback(response) {
                    $scope.toast("Error : " + response.data.ExceptionMessage);
                });
               
            });
        }
        else {
            $scope.toast('من فضلك ادخل كلمة او اكثر للبحث في البيانات');
            $scope.updateitemConfiguration = 1;
            $scope.listmodeConfiguration = 0;
            $scope.newitemConfiguration = 0;
        }
        $scope.loading = false;
    };
    // Delete Configurationdetails
    $scope.delete = function (index) {
        $scope.loading = true;
        var confirm = $mdDialog.confirm()
          .title('مسح بيانات')
          .textContent('هل تريد حقا مسح البيانات المختارة؟')
          .ok('نعم')
          .cancel('لا');

        $mdDialog.show(confirm).then(function () {
            $scope.loading = true;

            $http({
                method: 'DELETE',
                url: appApiBaseUrl + 'api/Configuration/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.ConfigurationsData.splice(index, 1);
                $scope.toast('تمت العملية بنجاح');
                $window.location.reload();
            }, function errorCallback(response) {
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
        $scope.loading = false;
    };

});
app.factory('ConfigurationService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/Configuration/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/Configuration/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END Configuration Controller//////////////////