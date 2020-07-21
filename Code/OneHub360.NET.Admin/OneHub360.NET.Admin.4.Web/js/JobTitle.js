//JobTitle Controller
app.controller('JobTitle', function (JobTitleService,$rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();

    $scope.loading = null;
    $scope.JobTitleData = null;
    $scope.listmodeJobTitle = 1;

    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    JobTitleService.GetAllRecords().then(function (response) {
        $scope.JobTitleData = response.data; // Success
        $scope.rowCollection = $scope.JobTitleData;
        $scope.listmodeJobTitle = 1;
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
    $scope.JobTitle = {
        Id: '',
        Title: '', Responsibilities: '', Rank: '', Description: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset JobTitledetails
    $scope.clear = function () {
        $scope.JobTitle.Id = '';
        $scope.JobTitle.Title = '';
        $scope.JobTitle.Responsibilities = '';
        $scope.JobTitle.Rank = '';
        $scope.JobTitle.Description = '';
        $scope.JobTitle.CreatedBy = '';
        $scope.JobTitle.CreationDate = '';
        $scope.JobTitle.LastModified = '';
        $scope.JobTitle.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;
        if ($scope.JobTitle.Title != "") {
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/JobTitle/Create/',
                data: $scope.JobTitle
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
            $scope.updateitemJobTitle = 0;
            $scope.listmodeJobTitle = 0;
            $scope.newitemJobTitle = 1;
        }
        $scope.loading = false;
    };
    // Edit JobTitledetails
    $scope.edit = function (data) {
        $scope.JobTitle = {
            Id: data.Id, Title: data.Title,
            Responsibilities: data.Responsibilities, Rank: data.Rank, Description: data.Description,
            CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified,
            LastModifiedBy: data.LastModifiedBy
        };
        $scope.updateitemJobTitle = 1;
        $scope.listmodeJobTitle = 0;
        $scope.newitemJobTitle = 0;
    }

    // Cancel JobTitledetails
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitemJobTitle = 0;
        $scope.listmodeJobTitle = 1;
        $scope.newitemJobTitle = 0;
    }
    // Cancel JobTitledetails
    $scope.New = function () {
        $scope.clear();
        $scope.updateitemJobTitle = 0;
        $scope.listmodeJobTitle = 0;
        $scope.newitemJobTitle = 1;
    }
    $scope.Filter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
        if ($scope.keyword != null) {
            JobTitleService.Filter($scope.keyword).then(function (response) {
                $scope.JobTitleData = response.data; // Success

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

        JobTitleService.GetAllRecords().then(function (response) {
            $scope.JobTitleData = response.data; // Success

        })
    };
    // Update JobTitledetails
    $scope.update = function () {
        $scope.loading = true;

        if ($scope.JobTitle.Title != "") {
            $scope.listmodeJobTitle = 1;
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/JobTitle/Update?id=' + $scope.JobTitle.Id,
                    data: $scope.JobTitle
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
            $scope.updateitemJobTitle = 1;
            $scope.listmodeJobTitle = 0;
            $scope.newitemJobTitle = 0;
        }
        $scope.loading = false;
    };
    // Delete JobTitledetails
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
                url: appApiBaseUrl + 'api/JobTitle/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.JobTitlesData.splice(index, 1);
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
app.factory('JobTitleService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/JobTitle/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/JobTitle/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END JobTitle Controller//////////////////