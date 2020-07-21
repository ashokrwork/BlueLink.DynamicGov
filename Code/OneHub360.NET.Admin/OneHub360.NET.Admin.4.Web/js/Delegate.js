//Delegate Controller
app.controller('Delegate', function (DelegateService, $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();

    $scope.loading = null;
    $scope.DelegateData = null;
    $scope.keyword = null;
    $scope.selecteddelegator = null;
    $scope.selecteddelegate = null;

    $scope.listmode = 1;
    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    DelegateService.GetAllRecords().then(function (response) {
        $scope.DelegateData = response.data; // Success
        $scope.rowCollection = $scope.DelegateData;
        $scope.listmode = 1;
    }, function () {
        //in case there is an error
        $mdDialog.show(
          $mdDialog.alert()
            .clickOutsideToClose(true)
            .title('خطأ')
            .textContent('خطأ في تحميل البيانات من فضلك حاول مرة اخرى')
            .ok('موافق')
        );
     
    });
    $scope.Users = null;
    $scope.Users = $http.get(appApiBaseUrl + 'api/userinfo/getall').success(function (result) {
        $scope.Users = result;

    });
    $scope.loading = false;

    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };

    $scope.Delegate = {
        Id: '',
        Fromdate: null,
        Todate: null,
        Delegateid: '',
        Delegatorid: '',
        DelegatorUser: '',
        DelegeteUser: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset Delegate details
    $scope.clear = function () {
        $scope.Delegate.Id = '';
        $scope.Delegate.Fromdate =null;
        $scope.Delegate.Todate = null;
        $scope.Delegate.Delegateid = '';

        $scope.Delegate.Delegatorid = '';
        $scope.Delegate.DelegatorUser = '';


        $scope.Delegate.DelegeteUser = '';

        $scope.Delegate.CreatedBy = '';
        $scope.Delegate.CreationDate = '';
        $scope.Delegate.LastModified = '';
        $scope.Delegate.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;

        if ($scope.Delegate.Fromdate != "" || $scope.Delegate.Todate != "") {
           
            $scope.Delegate.Delegatorid = $scope.selecteddelegator.Id;
            $scope.Delegate.DelegateId = $scope.selecteddelegate.Id;

            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/Delegate/Create/',
                data: $scope.Delegate
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available

                $scope.clear();
                $scope.toast('تمت العملية بنجاح');
                $scope.updateitem = 0;
                $scope.listmode = 1;
                $scope.newitem = 0;
                $window.location.reload();
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.toast("Error : " + response.data.ExceptionMessage);
                $scope.updateitem = 0;
                $scope.listmode = 0;
                $scope.newitem = 1;
            });
        }
        else {
            $scope.toast('من فضلك ادخل كل البيانات');
            $scope.updateitem = 0;
            $scope.listmode = 0;
            $scope.newitem = 1;
        }
        $scope.loading = false;

    };
    // Edit Delegate details
    $scope.edit = function (data) {
        $scope.Delegate = { Id: data.Id, 
            Fromdate: new Date(data.Fromdate),
            Todate: new Date(data.Todate),
            Delegateid: data.Delegateid,
            Delegatorid: data.Delegatorid,
            DelegatorUser: data.DelegatorUser,
            DelegeteUser: data.DelegeteUser, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy
        };
        $scope.selecteddelegator = $scope.Delegate.DelegatorUser;
        $scope.selecteddelegate = $scope.Delegate.DelegeteUser;

        $scope.updateitem = 1;
        $scope.listmode = 0;
        $scope.newitem = 0;
    }

    // Cancel Delegate details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
    }
 
    // Cancel Delegate details
    $scope.New = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 0;
        $scope.newitem = 1;
    }
    $scope.Filter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
        if ($scope.keyword != null) {
            DelegateService.Filter($scope.keyword).then(function (response) {
                $scope.DelegateData = response.data; // Success

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

        DelegateService.GetAllRecords().then(function (response) {
            $scope.DelegateData = response.data; // Success

        })
    };
    // Update Delegate details
    $scope.update = function () {

        $scope.loading = true;
        if ($scope.Delegate.Fromdate != "" || $scope.Delegate.Todate != "") {
            $scope.Delegate.Delegatorid = $scope.selecteddelegator.Id;
            $scope.Delegate.Delegateid = $scope.selecteddelegate.Id;
            var confirm = $mdDialog.confirm()
                      .title('تعديل بيانات')
                      .textContent('هل تريد حقا تعديل البيانات ؟')
                      .ok('نعم')
                      .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/Delegate/Update?id=' + $scope.Delegate.Id,
                    data: $scope.Delegate
                }).then(function successCallback(response) {
                    $scope.clear();
                    $scope.listmode = 1;

                    $scope.toast('تمت العملية بنجاح');
                    $window.location.reload();

                }, function errorCallback(response) {
                    $scope.toast("Error : " + response.data.ExceptionMessage);
                });
                $scope.updateitem = 0;
                $scope.listmode = 1;
                $scope.newitem = 0;
            });
        }

        else {
            $scope.toast('من فضلك ادخل كلمة او اكثر للبحث في البيانات');
            $scope.updateitem = 1;
            $scope.listmode = 0;
            $scope.newitem = 0;
        }
        $scope.loading = false;

    };
    $scope.showConfirm = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app

    };
    // Delete Delegate details
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
                url: appApiBaseUrl + 'api/Delegate/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.DelegatesData.splice(index, 1);
                $scope.toast('تمت العملية بنجاح');
                $window.location.reload();
            }, function errorCallback(response) {
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
            $scope.loading = false;

        }, function () {
            //$scope.status = 'You decided to keep your debt.';
        });
    }
});

app.factory('DelegateService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/Delegate/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/Delegate/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END Delegate Controller//////////////////