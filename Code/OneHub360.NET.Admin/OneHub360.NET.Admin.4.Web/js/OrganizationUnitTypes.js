//OrganizationUnitType Controller
app.controller('OrganizationUnitType', function (OrganizationUnitTypeService, $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();
    $scope.OrganizationUnitTypeData = null;
    $scope.listmodeOrganizationUnitType = 1;
    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    OrganizationUnitTypeService.GetAllRecords().then(function (response) {
        $scope.OrganizationUnitTypeData = response.data; // Success
        $scope.rowCollection = $scope.OrganizationUnitTypeData;
        $scope.listmodeOrganizationUnitType = 1;
    }, function () {
        $mdDialog.show(
         $mdDialog.alert()
           .clickOutsideToClose(true)
           .title('خطأ')
           .textContent('خطأ في تحميل البيانات من فضلك حاول مرة اخرى')
           .ok('موافق')
       );
    });
    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };
    $scope.loading = false;
    $scope.OrganizationUnitType = {
        Id: '',
        Name: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset OrganizationUnitType details
    $scope.clear = function () {
        $scope.OrganizationUnitType.Id = '';
        $scope.OrganizationUnitType.Name = '';
        $scope.OrganizationUnitType.CreatedBy = '';
        $scope.OrganizationUnitType.CreationDate = '';
        $scope.OrganizationUnitType.LastModified = '';
        $scope.OrganizationUnitType.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {

        if ($scope.neworgform.$valid) {
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/OrganizationUnitType/Create/',
                data: $scope.OrganizationUnitType
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                OrganizationUnitTypeService.GetAllRecords().then(function (response) {
                    $window.location.reload();
                }, function () {
                    $scope.toast("Error : " + response.data.ExceptionMessage);

                });
                $scope.clear();
                $scope.toast('تمت العملية بنجاح');
                $scope.updateitemOrganizationUnitType = 0;
                $scope.listmodeOrganizationUnitType = 1;
                $scope.newitemOrganizationUnitType = 0;
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.toast("Error : " + response.data.ExceptionMessage);
                $scope.updateitemOrganizationUnitType = 0;
                $scope.listmodeOrganizationUnitType = 0;
                $scope.newitemOrganizationUnitType = 1;
            });
        }
        else {
            $scope.neworgform.submitted = true;
            $scope.toast('من فضلك ادخل كل البيانات');
            $scope.updateitemOrganizationUnitType = 0;
            $scope.listmodeOrganizationUnitType = 0;
            $scope.newitemOrganizationUnitType = 1;
        }
    };
    //filter the organization types
    $scope.Filter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
        if ($scope.keyword != null) {
            OrganizationUnitTypeService.Filter($scope.keyword).then(function (response) {
                $scope.OrganizationUnitTypeData = response.data; // Success

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

        OrganizationUnitTypeService.GetAllRecords().then(function (response) {
            $scope.OrganizationUnitTypeData = response.data; // Success

        })
    };
    // Edit OrganizationUnitType details
    $scope.edit = function (data) {
        $scope.OrganizationUnitType = { Id: data.Id, Name: data.Name, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.updateitemOrganizationUnitType = 1;
        $scope.listmodeOrganizationUnitType = 0;
        $scope.newitemOrganizationUnitType = 0;
    }

    // Cancel OrganizationUnitType details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitemOrganizationUnitType = 0;
        $scope.listmodeOrganizationUnitType = 1;
        $scope.newitemOrganizationUnitType = 0;
    }
    // Cancel OrganizationUnitType details
    $scope.New = function () {
        $scope.clear();
        $scope.updateitemOrganizationUnitType = 0;
        $scope.listmodeOrganizationUnitType = 0;
        $scope.newitemOrganizationUnitType = 1;
    }
    // Update OrganizationUnitType details
    $scope.update = function () {

        if ($scope.updateorgform.$valid) {
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $scope.listmodeOrganizationUnitType = 1;
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/OrganizationUnitType/Update?id=' + $scope.OrganizationUnitType.Id,
                    data: $scope.OrganizationUnitType
                }).then(function successCallback(response) {
                    $scope.toast('تمت العملية بنجاح');
                    $window.location.reload();

                }, function () {
                    $scope.toast("Error : " + response.data.ExceptionMessage);
                    $scope.clear();

                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });
                $scope.updateitemOrganizationUnitType = 0;
                $scope.listmodeOrganizationUnitType = 1;
                $scope.newitemOrganizationUnitType = 0;
            });
        }
    else {
            $scope.updateorgform.submitted = true;
            $scope.toast('من فضلك ادخل كلمة او اكثر للبحث في البيانات');

        }
    };
    // Delete OrganizationUnitType details
    $scope.delete = function (index) {
        var confirm = $mdDialog.confirm()
           .title('مسح بيانات')
           .textContent('هل تريد حقا مسح البيانات المختارة؟')
           .ok('نعم')
           .cancel('لا');

        $mdDialog.show(confirm).then(function () {
            $scope.loading = true;
            $http({
                method: 'DELETE',
                url: appApiBaseUrl + 'api/OrganizationUnitType/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.OrganizationUnitTypesData.splice(index, 1);
                $scope.toast('تمت العملية بنجاح');
                $window.location.reload();

            }, function errorCallback(response) {
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
        });
    }
});

app.factory('OrganizationUnitTypeService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {

        return $http.get(appApiBaseUrl + 'api/OrganizationUnitType/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/OrganizationUnitType/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END OrganizationUnitType Controller//////////////////