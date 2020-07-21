//Organization Controller
app.controller('Organization', function (OrganizationService,$location, $rootScope, $mdToast, $window, $scope, $http, $mdDialog) {
    $rootScope.Checkauth();

   
    $scope.OrganizationTypesearchText = '';
    $scope.rowCollection = [];
    $scope.loading = null;
    $scope.OrganizationData = null;
    $scope.keyword = null;
    $scope.listmode = 1;
    $scope.loading = true;
    // Fetching records from the factory created at the bottom of the script file
    OrganizationService.GetAllRecords().then(function (response) {
        $scope.OrganizationData = response.data; // Success
        $scope.listmode = 1;
        $scope.rowCollection = $scope.OrganizationData;
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

        //$mdToast.show(
        //           $mdToast.simple()
        //              .textContent('حدث خطأ, حاول مرة اخرى.')
        //              .hideDelay(4000).position('top left')
        //        );
    });
    $scope.Organizationtypes = null;
    $scope.Organizationtypes = $http.get(appApiBaseUrl + 'api/OrganizationType/getall').success(function (result) {
        $scope.Organizationtypes = result;

    });
    function querySearch(query) {
        var results = query ? $scope.Organizationtypes.filter(createFilterFor(query)) : $scope.Organizationtypes;
        var deferred = $q.defer();
        $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
        return deferred.promise;
    }
    /**
    * Create filter function for a query string
    */
    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);

        return function filterFn(Organizationtype) {
            return (Organizationtype.Name.indexOf(lowercaseQuery) === 0);
        };

    }

    $scope.loading = false;

    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };

    $scope.Organization = {
        Id: '',
        Name: '',
        About: '',
        Email: '', Fax: '', IsLocal: '', Address: '', OfficeNumber: '', OrganizationTypeID: '', CreatedBy: '', CreationDate: '', LastModified: '', LastModifiedBy: ''
    };

    // Reset Organization details
    $scope.clear = function () {
        $scope.Organization.Id = '';
        $scope.Organization.Name = '';
        $scope.Organization.About = '';
        $scope.Organization.Email = '';
        $scope.Organization.Fax = '';
        $scope.Organization.IsLocal = '';
        $scope.Organization.Address = '';
        $scope.Organization.OfficeNumber = '';
        $scope.Organization.OrganizationTypeID = '';
        $scope.Organization.CreatedBy = '';
        $scope.Organization.CreationDate = '';
        $scope.Organization.LastModified = '';
        $scope.Organization.LastModifiedBy = '';
    }
    //Add New Item
    $scope.save = function () {
        $scope.loading = true;

        if ($scope.Organization.Name != "" &&
     $scope.selected.Id != "") {
            $scope.Organization.OrganizationTypeID = $scope.selected.Id;
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/Organization/Create/',
                data: $scope.Organization
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
            $scope.updateitem = 0;
            $scope.listmode = 0;
            $scope.newitem = 1;
        }
        $scope.loading = false;

    };
    // Edit Organization details
    $scope.edit = function (data) {
        $scope.Organization = { Id: data.Id, Name: data.Name, About: data.About, Email: data.Email, Fax: data.Fax, IsLocal: data.IsLocal, Address: data.Address, OfficeNumber: data.OfficeNumber, OrganizationTypeID: data.OrganizationTypeID, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        $scope.selected = $scope.Organizationtypes.constructor();
       
        if (data.OrganizationTypeID != null) {
            $scope.selected = $.grep($scope.Organizationtypes, function (b) {
                return b.Id === data.OrganizationTypeID;
            })[0];
        }
        $scope.updateitem = 1;
        $scope.listmode = 0;
        $scope.newitem = 0;
    }

    // Cancel Organization details
    $scope.cancel = function () {
        $scope.clear();
        $scope.updateitem = 0;
        $scope.listmode = 1;
        $scope.newitem = 0;
    }
    // Cancel Organization details
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
            OrganizationService.Filter($scope.keyword).then(function (response) {
                $scope.OrganizationData = response.data; // Success

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

        OrganizationService.GetAllRecords().then(function (response) {
            $scope.OrganizationData = response.data; // Success

        })
    };
    // Update Organization details
    $scope.update = function () {
        $scope.loading = true;

        if ($scope.updateorgform.$valid) {
            {
                if ($scope.selected == null) {
                    $scope.toast('من فضلك اختار نوع الجهة');
                }
                else {
                    $scope.Organization.OrganizationTypeID = $scope.selected.Id;
                    var confirm = $mdDialog.confirm()
              .title('تعديل بيانات')
              .textContent('هل تريد حقا تعديل البيانات ؟')
              .ok('نعم')
              .cancel('لا');

                    $mdDialog.show(confirm).then(function () {
                        $http({
                            method: 'Put',
                            url: appApiBaseUrl + 'api/Organization/Update?id=' + $scope.Organization.Id,
                            data: $scope.Organization
                        }).then(function successCallback(response) {
                            $scope.clear();
                            $scope.toast('تمت العملية بنجاح');

                        }, function errorCallback(response) {
                            $scope.toast("Error : " + response.data.ExceptionMessage);
                        });
                        $window.location.reload();
                    });
                }
            }
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
    $scope.showunits = function (data) {
        //$scope.Organization = { Id: data.Id, Users: data.Users, Name: data.Name, CreatedBy: data.CreatedBy, CreationDate: data.CreationDate, LastModified: data.LastModified, LastModifiedBy: data.LastModifiedBy };
        //$scope.Organization.Users = $http.get(appApiBaseUrl + 'api/organization/Getunits?organizationid=' + $scope.Organization.Id).success(function (result) {
        //    $scope.Organization.Users = result;
        //});
        $location.path("/OrganizationUnits");
        $rootScope.organizationID = data.Id;
    }
    $scope.showConfirm = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app

    };
    // Delete Organization details
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
                url: appApiBaseUrl + 'api/Organization/Delete?id=' + index,
            }).then(function successCallback(response) {
                //$scope.OrganizationsData.splice(index, 1);
                $scope.toast('تمت العملية بنجاح');
                $window.location.reload();
            }, function errorCallback(response) {
                $scope.toast(" من فضلك امسح كل المنظمات تحت المنظمة المختارة ");

                //$scope.toast("Error : " + response.data.ExceptionMessage);
            });
            $scope.loading = false;

        }, function () {
            //$scope.status = 'You decided to keep your debt.';
        });
    }
});

app.factory('OrganizationService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(appApiBaseUrl + 'api/Organization/getall');
    }
    fac.Filter = function (filterkeyword) {
        return $http.get(appApiBaseUrl + 'api/Organization/filter?keyword=' + filterkeyword);
    }
    return fac;
});
//////////////////END Organization Controller//////////////////