//Role Controller
app.controller('Role', function (RoleService, $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();
    $scope.loading = null;
    $scope.RoleData = null;
    $scope.keyword = null;
    $scope.UsersearchText = null;
    $scope.listmode = 1;
    $scope.rowCollection = $scope.OrganizationData;

    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    RoleService.GetAllRecords().then(function (response) {
        $scope.RoleData = response.data; // Success
        $scope.listmode = 1;
        $scope.rowCollection = $scope.RoleData;
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
    ////focus on the search list
    //function querySearch(query) {
    //    var results = query ? $scope.Users.filter(createFilterFor(query)) : $scope.Users;
    //    var deferred = $q.defer();
    //    $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
    //    return deferred.promise;
    //}
    ///**
    //* Create filter function for a query string
    //*/
    //function createFilterFor(query) {
    //    var lowercaseQuery = angular.lowercase(query);

    //    return function filterFn(user) {
    //        return (user.ArabicFullName.indexOf(lowercaseQuery) === 0);
    //    };

    //}


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

    $scope.Role = {
        Id: '',
        Name: '',
        Users: '',
        Description: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset Role details
    $scope.clear = function () {
        $scope.Role.Id = '';
        $scope.Role.Name = '';
        $scope.Role.Users = '';

        $scope.Role.Description = '';
        $scope.Role.CreatedBy = '';
        $scope.Role.CreationDate = '';
        $scope.Role.LastModified = '';
        $scope.Role.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;

        if ($scope.neworgform.$valid) {

            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/Role/Create/',
                data: $scope.Role
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
    // Edit Role details
    $scope.edit = function (data) {
        $scope.Role = { Id: data.Id, Users: data.Users, Name: data.Name, Description: data.Description, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.updateitem = 1;
        $scope.listmode = 0;
        $scope.newitem = 0;
    }

    // Cancel Role details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
    }
    //users section
    $scope.showusers = function (data) {
        $scope.Role = { Id: data.Id, Users: data.Users, Name: data.Name, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.Role.Users = $http.get(appApiBaseUrl + 'api/role/GetUserRole?roleid=' + $scope.Role.Id).success(function (result) {
            $scope.Role.Users = result;
        });
        $scope.usersmode = 1;
        $scope.listmode = 0;
    }
    //add new user
    $scope.Adduser = function () {
        $scope.loading = true;
        if ($scope.UsersearchText != null) {
            if ($scope.selectedUser != null) {
                var checkresult = null;
                if ($scope.Role.Users != null) {
                    checkresult = $.grep($scope.Role.Users, function (e) { return e.Id == $scope.selectedUser.Id; });
                }
                if (checkresult != null)
                    if (checkresult.length > 0) {
                        $scope.toast('مستخدم موجود مسبقا');
                    }


                    else {

                        $http({
                            method: 'POST',
                            url: appApiBaseUrl + 'api/Role/adduser?userid=' + $scope.selectedUser.Id + '&Roleid=' + $scope.Role.Id,

                        }).then(function successCallback(response) {
                            // this callback will be called asynchronously
                            // when the response is available
                            $scope.Role.Users.push({ Id: $scope.selectedUser.Id, ArabicFullName: $scope.selectedUser.ArabicFullName });
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
                url: appApiBaseUrl + 'api/Role/DeleteUser?userid=' + item.Id + '&Roleid=' + $scope.Role.Id,
            }).then(function successCallback(response) {
                $scope.toast('تمت العملية بنجاح');
                var index = $scope.Role.Users.indexOf(item);
                $scope.Role.Users.splice(index, 1);
            }, function errorCallback(response) {
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
            $scope.loading = false;

        }, function () {
            //$scope.status = 'You decided to keep your debt.';
        });
    }
    // Cancel Role details
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
            RoleService.Filter($scope.keyword).then(function (response) {
                $scope.RoleData = response.data; // Success

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

        RoleService.GetAllRecords().then(function (response) {
            $scope.RoleData = response.data; // Success

        })
    };
    // Update Role details
    $scope.update = function () {
        $scope.loading = true;


        if ($scope.updateorgform.$valid) {
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $scope.listmode = 1;
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/Role/Update?id=' + $scope.Role.Id,
                    data: $scope.Role
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
       
        }
        $scope.loading = false;

    };
    $scope.showConfirm = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app

    };
    // Delete Role details
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
                url: appApiBaseUrl + 'api/Role/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.RolesData.splice(index, 1);
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

app.factory('RoleService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/Role/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/Role/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END Role Controller//////////////////