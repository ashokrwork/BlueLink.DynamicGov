//OrganizationUnit Controller
app.controller('OrganizationUnit', function (OrganizationUnitService, $rootScope,$mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();
    $scope.ArabicFullNamesearchText = null;
    $scope.OrganizationsearchText = null;
    $scope.OrganizationUnitsearchText = null;
    $scope.OrganizationUnitTypesearchText = null;

    $scope.loading = null;
    $scope.OrganizationUnitData = null;
    $scope.listmodeOrganizationUnit = 1;
    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    OrganizationUnitService.GetAllRecords().then(function (response) {
        $scope.OrganizationUnitData = response.data; // Success
        $scope.listmodeOrganizationUnit = 1;
        $scope.rowCollection = $scope.OrganizationUnitData;

    }, function () {
        $mdDialog.show(
        $mdDialog.alert()
          .clickOutsideToClose(true)
          .title('خطأ')
          .textContent('خطأ في تحميل البيانات من فضلك حاول مرة اخرى')
          .ok('موافق')
      );
    });
    $scope.OrganizationUnitTypes = null;
    $scope.OrganizationUnitTypes = $http.get(appApiBaseUrl + 'api/OrganizationUnitType/getall').success(function (result) {
        $scope.OrganizationUnitTypes = result;

    });

    $scope.Organizations = null;
    $scope.Organizations = $http.get(appApiBaseUrl + 'api/Organization/getall').success(function (result) {
        $scope.Organizations = result;

    });
    $scope.UserInfo = null;
    $scope.UserInfo = $http.get(appApiBaseUrl + 'api/UserInfo/getall').success(function (result) {
        $scope.UserInfo = result;

    });

    $scope.loading = false;

    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };
    $scope.OrganizationUnit = {
        Id: '',
        Name: '', About: '', Location: '', OrganizationUnitParent: '', ManagerId: '',

        Organization: '', OrganizationUnitType: '', OrganizationUnitParentID: '',
        OrganizationID: '', OrganizationUnitTypeID: '',
        CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: '',Prifix:''
    };

    // Reset OrganizationUnitdetails
    $scope.clear = function () {
        $scope.OrganizationUnit.Id = '';
        $scope.OrganizationUnit.Name = '';
        $scope.OrganizationUnit.About = '';
        $scope.OrganizationUnit.Location = '';
        $scope.OrganizationUnit.OrganizationUnitParent = '';
        $scope.OrganizationUnit.ManagerId = '';
        $scope.OrganizationUnit.Organization = '';
        $scope.OrganizationUnit.OrganizationUnitType = '';
        $scope.OrganizationUnit.OrganizationUnitParentID = '';
        $scope.OrganizationUnit.OrganizationID = '';
        $scope.OrganizationUnit.OrganizationUnitTypeID = '';
        $scope.OrganizationUnit.Prifix = '';
    }
    //Add New Item
    $scope.save = function () {

        $scope.loading = true;
        if ($scope.neworgform.$valid 
            ) {
             if ($scope.content!=null)
                 $scope.OrganizationUnit.Logo = $scope.content
            if ($scope.selectedUserInfo != null)
                $scope.OrganizationUnit.ManagerId = $scope.selectedUserInfo.Id;
            if ($rootScope.organizationID != null)
                $scope.OrganizationUnit.OrganizationID = $rootScope.organizationID;
            else
                $scope.OrganizationUnit.OrganizationID = "0";
            console.log($scope.OrganizationUnit.OrganizationID);
            if ($scope.selectedOrganizationUnit != null)
                $scope.OrganizationUnit.OrganizationUnitParentID = $scope.selectedOrganizationUnit.Id;
            if ($scope.selectedOrganizationUnitType != null)
                $scope.OrganizationUnit.OrganizationUnitTypeID = $scope.selectedOrganizationUnitType.Id;
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/OrganizationUnit/Create/',
                data: $scope.OrganizationUnit
            }).then(function successCallback(response) {
                $scope.toast('تمت العملية بنجاح');
                //$window.location.reload();

                $scope.updateitemOrganizationUnit = 0;
                $scope.listmodeOrganizationUnit = 1;
                $scope.newitemOrganizationUnit = 0;

                OrganizationUnitService.GetAllRecords().then(function (response) {
                    $scope.OrganizationUnitData = response.data; // Success
                    $scope.listmodeOrganizationUnit = 1;
                    $scope.rowCollection = $scope.OrganizationUnitData;

                }, function () {
                    $mdDialog.show(
                    $mdDialog.alert()
                      .clickOutsideToClose(true)
                      .title('خطأ')
                      .textContent('خطأ في تحميل البيانات من فضلك حاول مرة اخرى')
                      .ok('موافق')
                  );
                });

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.toast("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            $scope.neworgform.submitted = true;
            $scope.toast('من فضلك ادخل كل البيانات');
            $scope.updateitemOrganizationUnit = 0;
            $scope.listmodeOrganizationUnit = 0;
            $scope.newitemOrganizationUnit = 1;
        }
        $scope.loading = false;
    };
    $scope.showContent = function ($fileContent) {
        $scope.content = $fileContent;
        //console.log($fileContent);
    };
    // Edit OrganizationUnitdetails
    $scope.edit = function (data) {
        $scope.OrganizationUnit = {
            Id: data.Id, Title: data.Title,
            Prifix:data.Prifix,
            About: data.About, Location: data.Location, OrganizationUnitParent: data.OrganizationUnitParent,
            ManagerId: data.ManagerId, Organization: data.Organization, OrganizationUnitType: data.OrganizationUnitType,
            OrganizationUnitParentID: data.OrganizationUnitParentID,
            OrganizationID: data.OrganizationID,
            OrganizationUnitTypeID: data.OrganizationUnitTypeID, PersonTitle: data.PersonTitle, Logo: data.Logo
        }; 
        $scope.content = $scope.OrganizationUnit.Logo;
        $scope.selectedOrganization = null;
        if (data.OrganizationID != null) {
            $scope.selectedOrganization =
                 $scope.selectedOrganization = $.grep($scope.Organizations, function (b) {
                     return b.Id === data.Organization.Id;
                 })[0]; 
        }
        $scope.selectedOrganizationUnit = null;
        if (data.OrganizationUnitParentID != null)
        {
                 $scope.selectedOrganizationUnit = $.grep($scope.OrganizationUnitData, function (b) {
                     return b.Id === data.OrganizationUnitParent.Id;
                 })[0];
        }
      

        $scope.selectedOrganizationUnitType = null;
        if (data.OrganizationUnitTypeID != null)
        {
           
            $scope.selectedOrganizationUnitType = $.grep($scope.OrganizationUnitTypes, function (b) {
                return b.Id === data.OrganizationUnitType.Id;
                 })[0];
        }

        $scope.selectedUserInfo = null;
        if (data.ManagerId != null)
        {
            $scope.selectedUserInfo = $.grep($scope.UserInfo, function (b) {
                return b.Id === data.ManagerId;
            })[0];
        }
        $scope.updateitemOrganizationUnit = 1;
        $scope.listmodeOrganizationUnit = 0;
        $scope.newitemOrganizationUnit = 0;
    }

    // Cancel OrganizationUnitdetails
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitemOrganizationUnit = 0;
        $scope.listmodeOrganizationUnit = 1;
        $scope.newitemOrganizationUnit = 0;
    }
    // Cancel OrganizationUnitdetails
    $scope.New = function () {
        console.log($rootScope.organizationID);
        $scope.clear();
        $scope.updateitemOrganizationUnit = 0;
        $scope.listmodeOrganizationUnit = 0;
        $scope.newitemOrganizationUnit = 1;
    }
    $scope.Filter = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
        if ($scope.keyword != null) {
            OrganizationUnitService.Filter($scope.keyword).then(function (response) {
                $scope.OrganizationUnitData = response.data; // Success

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

        OrganizationUnitService.GetAllRecords().then(function (response) {
            $scope.OrganizationUnitData = response.data; // Success

        })
    };
    // Update OrganizationUnitdetails
    $scope.update = function () {
        $scope.loading = true;
        $scope.error = false;
        if ($scope.updateorgform.$valid) {
            if ($scope.content != null)
                $scope.OrganizationUnit.Logo = $scope.content
            //if ($scope.selectedUserInfo != null)
            //    $scope.OrganizationUnit.ManagerId = $scope.selectedUserInfo.Id;
            //else
            //{
            //    $scope.error = true;
            //    $scope.toast('من فضلك اختار مدير');

            //}
            //if ($scope.selectedOrganization != null)
            //    $scope.OrganizationUnit.OrganizationID = $scope.selectedOrganization.Id;
            //else {
            //    $scope.error = true;
            //    $scope.toast('من فضلك اختار منظمة');

            //}
            if ($scope.selectedOrganizationUnit != null)
                $scope.OrganizationUnit.OrganizationUnitParentID = $scope.selectedOrganizationUnit.Id;
            //else {
            //    $scope.error = true;
            //    $scope.toast('من فضلك اختار المنظمة المتبوعة');

            //}
            if ($scope.selectedOrganizationUnitType != null)
                $scope.OrganizationUnit.OrganizationUnitTypeID = $scope.selectedOrganizationUnitType.Id;
            else {
                $scope.error = true;
                $scope.toast('من فضلك اختار نوع جهة');

            }
            var confirm = $mdDialog.confirm()
          .title('تعديل بيانات')
          .textContent('هل تريد حقا تعديل البيانات ؟')
          .ok('نعم')
          .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                if (!$scope.error) {
                    $http({
                        method: 'Put',
                        url: appApiBaseUrl + 'api/OrganizationUnit/Update?id=' + $scope.OrganizationUnit.Id,
                        data: $scope.OrganizationUnit
                    }).then(function successCallback(response) {
                        $scope.toast('تمت العملية بنجاح');
                        //$window.location.reload();

                        $scope.updateitemOrganizationUnit = 0;
                        $scope.listmodeOrganizationUnit = 1;
                        $scope.newitemOrganizationUnit = 0;

                        OrganizationUnitService.GetAllRecords().then(function (response) {
                            $scope.OrganizationUnitData = response.data; // Success
                            $scope.listmodeOrganizationUnit = 1;
                            $scope.rowCollection = $scope.OrganizationUnitData;

                        }, function () {
                            $mdDialog.show(
                            $mdDialog.alert()
                              .clickOutsideToClose(true)
                              .title('خطأ')
                              .textContent('خطأ في تحميل البيانات من فضلك حاول مرة اخرى')
                              .ok('موافق')
                          );
                        });

                    }, function errorCallback(response) {
                        $scope.toast("Error : " + response.data.ExceptionMessage);
                    });
                    $scope.updateitemOrganizationUnit = 0;
                    $scope.listmodeOrganizationUnit = 1;
                    $scope.newitemOrganizationUnit = 0;
                }
            });


        }
        else {
            $scope.updateorgform.submitted = true;
            $scope.toast('خطأ في تعديل البيانات من فضلك حاول مرة اخرى');

        }
        $scope.loading = false;
    };
   
    // Delete OrganizationUnitdetails
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
                url: appApiBaseUrl + 'api/OrganizationUnit/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.OrganizationUnitsData.splice(index, 1);
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
app.factory('OrganizationUnitService', function ($http, $rootScope) {

    var fac = {};
    fac.GetAllRecords = function () {
        if ($rootScope.organizationID != null)
            return $http.get(appApiBaseUrl + 'api/OrganizationUnit/getbyorganizationID?id=' + $rootScope.organizationID);
            else
            return $http.get(appApiBaseUrl + 'api/OrganizationUnit/GetLocal');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/OrganizationUnit/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END OrganizationUnit Controller//////////////////