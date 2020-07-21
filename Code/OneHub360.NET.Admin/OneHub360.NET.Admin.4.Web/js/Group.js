//Group Controller
app.controller('Group', function (GroupService, $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();
    $scope.loading = null;
    $scope.GroupData = null;
    $scope.keyword = null;
    $scope.selectedUser = "";
    $scope.listmode = 0;
    $scope.usersmode = 1;
    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    GroupService.GetAllRecords().then(function (response) {
        $scope.GroupData = response.data; // Success
        $scope.listmode = 1;
        $scope.usersmode = 0;
        $scope.rowCollection = $scope.GroupData;

        //var toast = $mdToast.simple()
        //             .textContent('حدث خطأ, حاول مرة اخرى.')
        //             .action('موافق')
        //             .position('top left').hideDelay(20000)
        //             .highlightAction(false);
        //$mdToast.show(toast).then(function (response) {
        //    if (response == 'موافق') {
        //      alert('You clicked \'موافق\'.');
        //    }
        //});
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

    $scope.Group = {
        Id: '',
        Name: '',Users:'',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset Group details
    $scope.clear = function () {
        $scope.Group.Id = '';
        $scope.Group.Name = '';
        $scope.Group.Users = '';

        $scope.Group.CreatedBy = '';
        $scope.Group.CreationDate = '';
        $scope.Group.LastModified = '';
        $scope.Group.LastModifiedBy = '';
    }
    //users section
    $scope.showusers = function (data) {
        $scope.Group = { Id: data.Id, Users: data.Users, Name: data.Name, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.usersmode = 1;
        $scope.listmode = 0;
    }
    //add new user
    $scope.Adduser = function () {
        $scope.loading = true;
        //$scope.adduserform.$valid
        if ($scope.UsersearchText != null) {
            if ($scope.selectedUser != null) {
                var result = $.grep($scope.Group.Users, function (e) { return e.Id == $scope.selectedUser.Id; });
                if (result.length > 0) {
                    $scope.toast('مستخدم موجود مسبقا');
                }
                else {

                    $http({
                        method: 'POST',
                        url: appApiBaseUrl + 'api/Group/adduser?userid=' + $scope.selectedUser.Id + '&groupid=' + $scope.Group.Id,

                    }).then(function successCallback(response) {
                        // this callback will be called asynchronously
                        // when the response is available
                        $scope.Group.Users.push({ Id: $scope.selectedUser.Id, ArabicFullName: $scope.selectedUser.ArabicFullName });
                        $scope.toast('تمت العملية بنجاح');
                    }, function errorCallback(response) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        $scope.toast("Error : " + response.data.ExceptionMessage);
                    });
                }
            }
            else {
                $scope.adduserform.submitted = true;
                $scope.toast('من فضلك ادخل كل البيانات');
            }
        }
        else {
            $scope.adduserform.submitted = true;
            $scope.toast('من فضلك ادخل كل البيانات');

        }
        $scope.loading = false;
    };
    $scope.deleteuser = function (item) {

        var confirm = $mdDialog.confirm()
            .title('مسح بيانات')
            .textContent('هل تريد حقا مسح المستخدم المختار؟')
            .ok('نعم')
            .cancel('لا');

        $mdDialog.show(confirm).then(function () {
            $scope.loading = true;

            $http({
                method: 'DELETE',
                url: appApiBaseUrl + 'api/Group/DeleteUser?userid=' + item.Id + '&groupid=' + $scope.Group.Id,
            }).then(function successCallback(response) {
                $scope.toast('تمت العملية بنجاح');
                var index = $scope.Group.Users.indexOf(item);
                $scope.Group.Users.splice(index, 1);
            }, function errorCallback(response) {
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
            $scope.loading = false;

        }, function () {
            //$scope.status = 'You decided to keep your debt.';
        });
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;
        if ($scope.neworgform.$valid) {
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/Group/Create/',
                data: $scope.Group
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
            $scope.neworgform.submitted = true;

            $scope.toast('من فضلك ادخل كل البيانات');

        }
        $scope.loading = false;

    };
    
   
    // Edit Group details
    $scope.edit = function (data) {
        $scope.Group = { Id: data.Id, Name: data.Name, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.updateitem = 1;
        $scope.listmode = 0;
        $scope.newitem = 0;
    }

    // Cancel Group details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
    }
    // Cancel Group details
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
            GroupService.Filter($scope.keyword).then(function (response) {
                $scope.GroupData = response.data; // Success

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

        GroupService.GetAllRecords().then(function (response) {
            $scope.GroupData = response.data; // Success

        })
    };
    // Update Group details
    $scope.update = function () {
        $scope.loading = true;


        if ($scope.updateorgform.$valid) {
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/Group/Update?id=' + $scope.Group.Id,
                    data: $scope.Group
                }).then(function successCallback(response) {
                    $scope.clear();
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
            $scope.updateorgform.submitted = true;
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
    // Delete Group details
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
                url: appApiBaseUrl + 'api/Group/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.GroupsData.splice(index, 1);
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

app.factory('GroupService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/Group/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/Group/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END Group Controller//////////////////