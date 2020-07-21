//OrganizationType Controller
app.controller('OrganizationType', function (OrganizationTypeService, $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {

     $rootScope.Checkauth();

    $scope.OrganizationTypeData = null;
    $scope.listmodeOrganizationType = 1;
    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    OrganizationTypeService.GetAllRecords().then(function (response) {
        $scope.OrganizationTypeData = response.data; // Success
      
        $scope.listmodeOrganizationType = 1;
        $scope.rowCollection = $scope.OrganizationTypeData;

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
    $scope.OrganizationType = {
        Id: '',
        Name: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset OrganizationType details
    $scope.clear = function () {
        $scope.OrganizationType.Id = '';
        $scope.OrganizationType.Name = '';
        $scope.OrganizationType.CreatedBy = '';
        $scope.OrganizationType.CreationDate = '';
        $scope.OrganizationType.LastModified = '';
        $scope.OrganizationType.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
       
        if ($scope.neworgform.$valid ) {
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/OrganizationType/Create/',
                data: $scope.OrganizationType
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                OrganizationTypeService.GetAllRecords().then(function (response) {
                    $window.location.reload();
                }, function () {
                    $scope.toast("Error : " + response.data.ExceptionMessage);

                });
                $scope.clear();
                $scope.toast('تمت العملية بنجاح');
                $scope.updateitemOrganizationType = 0;
                $scope.listmodeOrganizationType = 1;
                $scope.newitemOrganizationType = 0;
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.toast("Error : " + response.data.ExceptionMessage);
                $scope.updateitemOrganizationType = 0;
                $scope.listmodeOrganizationType = 0;
                $scope.newitemOrganizationType = 1;
            });
        }
        else {
            $scope.toast('من فضلك ادخل كل البيانات');

            $scope.neworgform.submitted = true;
        }
    };
    //filter the organization types
    $scope.Filter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
        if ($scope.keyword != null) {
            OrganizationTypeService.Filter($scope.keyword).then(function (response) {
                $scope.OrganizationTypeData = response.data; // Success

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

        OrganizationTypeService.GetAllRecords().then(function (response) {
            $scope.OrganizationTypeData = response.data; // Success

        })
    };
    // Edit OrganizationType details
    $scope.edit = function (data) {
        $scope.OrganizationType = { Id: data.Id, Name: data.Name, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.updateitemOrganizationType = 1;
        $scope.listmodeOrganizationType = 0;
        $scope.newitemOrganizationType = 0;
    }

    // Cancel OrganizationType details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitemOrganizationType = 0;
        $scope.listmodeOrganizationType = 1;
        $scope.newitemOrganizationType = 0;
    }
    // Cancel OrganizationType details
    $scope.New = function () {
        $scope.clear();
        $scope.updateitemOrganizationType = 0;
        $scope.listmodeOrganizationType = 0;
        $scope.newitemOrganizationType = 1;
    }
    // Update OrganizationType details
    $scope.update = function () {

        if ($scope.updateorgform.$valid) {
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $scope.listmodeOrganizationType = 1;
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/OrganizationType/Update?id=' + $scope.OrganizationType.Id,
                    data: $scope.OrganizationType
                }).then(function successCallback(response) {
                    $scope.toast('تمت العملية بنجاح');
                    $window.location.reload();

                }, function () {
                    $scope.toast("Error : " + response.data.ExceptionMessage);
                    $scope.clear();

                }, function errorCallback(response) {
                    alert("Error : " + response.data.ExceptionMessage);
                });
                $scope.updateitemOrganizationType = 0;
                $scope.listmodeOrganizationType = 1;
                $scope.newitemOrganizationType = 0;
            });
        }
        else {
            $scope.toast('من فضلك ادخل كلمة او اكثر للبحث في البيانات');
            $scope.updateorgform.submitted = true;
        }
    };
    // Delete OrganizationType details
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
                url: appApiBaseUrl + 'api/OrganizationType/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.OrganizationTypesData.splice(index, 1);
                $scope.toast('تمت العملية بنجاح');
                $window.location.reload();

            }, function errorCallback(response) {
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
        });
    }
});

app.factory('OrganizationTypeService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {

        return $http.get(appApiBaseUrl + 'api/OrganizationType/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/OrganizationType/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END OrganizationType Controller//////////////////