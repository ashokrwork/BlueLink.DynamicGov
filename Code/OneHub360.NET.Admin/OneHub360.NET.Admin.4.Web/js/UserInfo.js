//UserInfo Controller
app.controller('UserInfo', function (UserInfoService,Upload,$rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();

    $scope.selectedStatus = null;
    $scope.selectedOrganizationUnit = null;
    $scope.selectedJobTitle = null;
    $scope.selectedReportedTo = null;
    $scope.ADselectedUser = null;

    $scope.loading = null;
    $scope.UserInfoData = null;
    $scope.keyword = null;
    $scope.listmode = 1;
    $scope.loading = true;
    $scope.statusenum = [
     {
         Id: '0', Name: 'St'
     }, {
        Id: '1', Name: 'Registered'
    },
    { Id: '2', Name: 'Updated' },
     { Id: '3', Name: 'Approved' },
      { Id: '4', Name: 'Inactive' }
    ];

    // Fetching records from the factory created at the bottom of the script file
    UserInfoService.GetAllRecords().then(function (response) {
        $scope.UserInfoData = response.data; // Success
        $scope.listmode = 1;
        $scope.rowCollection = $scope.UserInfoData;

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
    $scope.OrganizationUnits = null;
    $scope.OrganizationUnits = $http.get(appApiBaseUrl + 'api/OrganizationUnit/GetLocal').success(function (result) {
        $scope.OrganizationUnits = result;


    });
    $scope.JobTitles = null;
    $scope.JobTitles = $http.get(appApiBaseUrl + 'api/JobTitle/getall').success(function (result) {
        $scope.JobTitles = result;
    });

    $scope.ADUsers = null;
    $scope.ADUsers = $http.get(appApiBaseUrl + 'api/userinfo/GetADUsers').success(function (result) {
        $scope.ADUsers = result;
    });

    // upload image 

    $scope.showContent = function ($fileContent) {
        $scope.content = $fileContent;
        //console.log($fileContent);
    };

    //$scope.uploadPic = function (file) {
    //    //$scope.Photostream = file.result;
    //    console.log(file);
    //    //file.upload = Upload.upload({
    //    //    url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
    //    //    data: {username: $scope.username, file: file},
    //    //});

    //    //file.upload.then(function (response) {
    //    //    $timeout(function () {
    //    //        file.result = response.data;
    //    //    });
    //    //}, function (response) {
    //    //    if (response.status > 0)
    //    //        $scope.errorMsg = response.status + ': ' + response.data;
    //    //}, function (evt) {
    //    //    // Math.min is to fix IE which reports 200% sometimes
    //    //    file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
    //    //});
    //}

    $scope.loading = false;

    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };

    $scope.UserInfo = {
        ADUsername:'',
        Id: '',
        LoginName: '',
        ArabicFullName: '',
        Email: '', LatinFullName: '', Mobile: '', Photo: '', PersonalMessage: '',
        BirthDate: '', About: '', OfficePhone: '',
        Status: '', OrganizationUnitID: '', JobTitleID: ''
        , ReportingToID: ''
        , OrganizationUnit: '', JobTitle: ''
        , ReportingTo: ''
        , CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };
    $scope.changeimage = function (img) {
        $scope.picFile1 = img;
    
    }
    // Reset UserInfo details
    $scope.clear = function () {
        $scope.UserInfo.Id = '';
        $scope.UserInfo.LoginName = '';
        $scope.UserInfo.ArabicFullName = '';
        $scope.UserInfo.Email = '';
        $scope.UserInfo.LatinFullName = '';
        $scope.UserInfo.Mobile = '';
        $scope.UserInfo.Photo = '';
        $scope.UserInfo.PersonalMessage = '';
        $scope.UserInfo.BirthDate = '';
        $scope.UserInfo.About = '';
        $scope.UserInfo.OfficePhone = '';
        $scope.UserInfo.Password='';
        $scope.UserInfo.Status = '';
        $scope.UserInfo.OrganizationUnitID = '';
        $scope.UserInfo.JobTitleID = '';
        $scope.UserInfo.ReportingToID = '';
        $scope.UserInfo.Passwordconfirm = '';
        $scope.UserInfo.OrganizationUnit = '';
        $scope.UserInfo.JobTitle = '';
        $scope.UserInfo.ReportingTo= '';
        $scope.UserInfo.ADUsername = '';

        $scope.UserInfo.CreatedBy = '';
        $scope.UserInfo.CreationDate = '';
        $scope.UserInfo.LastModified = '';
        $scope.UserInfo.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;
        if ($scope.neworgform.$valid) {
            if ($scope.selectedOrganizationUnit != null)
                $scope.UserInfo.OrganizationUnitID = $scope.selectedOrganizationUnit.Id;
            if ($scope.selectedJobTitle != null)
                $scope.selectedJobTitle = $scope.selectedJobTitle.Id;
            if ($scope.selectedReportedTo != null)
                $scope.UserInfo.ReportingToID = $scope.selectedReportedTo.Id;
            if ($scope.selectedReportedTo != null)
                $scope.UserInfo.statusenum = $scope.selectedReportedTo.Id;
            if ($scope.content!=null)
                $scope.UserInfo.Photo = $scope.content;
            $scope.UserInfo.ADUsername = $scope.ADselectedUser;
            $scope.UserInfo.LoginName = 'cpa\\' + $scope.UserInfo.LoginName;
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/UserInfo/Create/',
                data: $scope.UserInfo
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
            $scope.neworgform.submitted = true;
        }
        $scope.loading = false;

    };
    // Edit UserInfo details
    $scope.edit = function (data) {
        $scope.UserInfo = {
            Id: data.Id, LoginName: data.LoginName, ArabicFullName: data.ArabicFullName,
            About : data.About,Password : data.Password,Passwordconfirm : data.Password,
            Email: data.Email, LatinFullName: data.LatinFullName, Mobile: data.Mobile, Photo: data.Photo,
            PersonalMessage: data.PersonalMessage, BirthDate: new Date(data.BirthDate), Status: data.Status, OrganizationUnit: data.OrganizationUnit,
            ReportingTo :data.ReportingTo, JobTitle:data.JobTitle,
            CreatedBy: data.CreatedBy, CreationDate: data.CreationDate,
            LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy,
            ADUsername: data.ADUsername
        };
        $scope.ADselectedUser = $scope.UserInfo.ADUsername;
        if (data.BirthDate == null)
        {
            $scope.UserInfo.BirthDate = new  Date('1/1/1900')
        }
        $scope.selectedOrganizationUnit = null;
        if (data.OrganizationUnitID != null) {
            $scope.selectedOrganizationUnit = $.grep($scope.OrganizationUnits, function (b) {
                return b.Id === data.OrganizationUnitID;
            })[0];
        }
        $scope.selectedJobTitle = null;
        if (data.JobTitleID != null) {

            $scope.selectedJobTitle = $.grep($scope.JobTitles, function (b) {
                return b.Id === data.JobTitleID;
            })[0];
        }

        $scope.selectedReportedTo = null;
        if (data.ReportingToID != null) {

            $scope.selectedReportedTo = $.grep($scope.UserInfoData, function (b) {
                return b.Id === data.ReportingToID;
            })[0];
        }

     

        if ($scope.UserInfo.Status != null) {
            var result = $.grep($scope.statusenum, function (e) { return e.Id == $scope.UserInfo.Status; });
            $scope.selectedStatus = result[0];
        }
        $scope.content = data.Photo;

        $scope.updateitem = 1;
        $scope.listmode = 0;
        $scope.newitem = 0;
    }

    // Cancel UserInfo details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
    }
    // Cancel UserInfo details
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
            UserInfoService.Filter($scope.keyword).then(function (response) {
                $scope.UserInfoData = response.data; // Success

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

        UserInfoService.GetAllRecords().then(function (response) {
            $scope.UserInfoData = response.data; // Success

        })
    };
    // Update UserInfo details
    $scope.update = function () {

        $scope.loading = true;

        if($scope.UserInfo.Password===$scope.UserInfo.Passwordconfirm)
            if ($scope.updateorgform.$valid)
            {
            var confirm = $mdDialog.confirm()
           .title('تعديل بيانات')
           .textContent('هل تريد حقا تعديل البيانات ؟')
           .ok('نعم')
           .cancel('لا');

            $mdDialog.show(confirm).then(function () {
                $scope.listmode = 1;
                if ($scope.selectedOrganizationUnit != null)
                    $scope.UserInfo.OrganizationUnitID = $scope.selectedOrganizationUnit.Id;
                if ($scope.selectedJobTitle != null)
                    $scope.UserInfo.JobTitleID = $scope.selectedJobTitle.Id;
                if ($scope.selectedReportedTo != null)
                    $scope.UserInfo.ReportingToID = $scope.selectedReportedTo.Id;
                $scope.UserInfo.ADUsername = $scope.ADselectedUser;
                $scope.UserInfo.Photo = $scope.content;
                $http({
                    method: 'Put',
                    url: appApiBaseUrl + 'api/UserInfo/Update?id=' + $scope.UserInfo.Id,
                    data: $scope.UserInfo
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

            $scope.toast('حدث خطأ حاول مرة اخرى');
         
        }
        else
            $scope.toast('كلمة المرور غير متطابقة');
        $scope.loading = false;

    };
    $scope.showConfirm = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app

    };
    // Delete UserInfo details
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
                url: appApiBaseUrl + 'api/UserInfo/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.UserInfosData.splice(index, 1);
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

app.factory('UserInfoService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/UserInfo/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/UserInfo/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END UserInfo Controller//////////////////

app.directive('onReadFile', function ($parse) {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element, attrs) {
            var fn = $parse(attrs.onReadFile);

            element.on('change', function (onChangeEvent) {
                var reader = new FileReader();

                reader.onload = function (onLoadEvent) {
                    var buffer = onLoadEvent.target.result;
                    var uint8 = new Uint8Array(buffer); // Assuming the binary format should be read in unsigned 8-byte chunks
                    // If you're on ES6 or polyfilling
                    // var result = Array.from(uint8);
                    // Otherwise, good old loop
                    var result = [];
                    for (var i = 0; i < uint8.length; i++) {
                        result.push(uint8[i]);
                    }

                    // Result is an array of numbers, each number representing one byte (from 0-255)
                    // On your backend, you can construct a buffer from an array of integers with the same uint8 format
                    scope.$apply(function () {
                        fn(scope, {
                            $fileContent: result
                        });
                    });
                };

                reader.readAsArrayBuffer((onChangeEvent.srcElement || onChangeEvent.target).files[0]);
            });
        }
    };
});