//LoginController
app.controller('login', function ($mdToast, $cookies, $window, $scope, $http, $mdDialog) {

    $scope.Username = '';
    $scope.Password = '';
    $scope.toast = function (text) {
        $mdToast.show(
                       $mdToast.simple()
                          .textContent(text)
                          .hideDelay(4000).position('top left')
                    );
    };

    var UserInfo = {
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
    //login
    $scope.login = function () {
        console.log(UserInfo);
        if ($scope.Username != '' || $scope.Password != '') {
            UserInfo.LoginName = $scope.Username;
            UserInfo.Password = $scope.Password;
           
            $http({
                method: 'POST',
                url: appApiBaseUrl + 'api/userinfo/login/',
                data: UserInfo
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
               // var expireDate = new Date();
               // expireDate.setDate(expireDate.getDate() + 1);
                var user = response.data;
               // $cookies.put('userID', user.Id, { 'expires': expireDate });
                $cookies.put('userID', user.Id);
                $window.location = "#";
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                console.log(response)
                $scope.toast("Error : Unable to login");

            });
        }
        else {
            $scope.toast('من فضلك ادخل كل البيانات');
        }

    };
});