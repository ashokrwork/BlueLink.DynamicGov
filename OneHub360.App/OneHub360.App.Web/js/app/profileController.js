OneHub360.controller('profileController',
    ['$scope', '$http', '$timeout', '$rootScope', '$window', function (
            $scope, $http, $timeout, $rootScope, $window) {


        $scope.SignatureAppLink = "/wpf/modules/app/signature/OneHub360.App.WPF.Signature.xbap?userId=" + HubStorage().currentUserId;

        $scope.Init = function () {
            $scope.status = 0;
            $timeout(function () {

                var userInfo = $.grep($rootScope.peopleSource, function (e) { return e.Id == HubStorage().currentUserId });
                $scope.UserInfo = userInfo[0];

                var signatureApiCallUrl = appContext.appServiceBaseUrl + 'api/user/signature/' + HubStorage().currentUserId;

                $http.get(signatureApiCallUrl)
                    .success(function (data, status, headers, config) {
                        if (data == null)
                            $scope.isEmpty = true;
                        $scope.SignatureData = data;
                        $scope.status = 1;
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                        $scope.status = -1;
                    });
                $scope.profileUserInfo = $http.get(appContext.adminServiceBaseUrl + 'api/userinfo/GetById?id=' + HubStorage().currentUserId).success(function (result) {
                    $scope.profileUserInfo = result;
                    $scope.content = $scope.profileUserInfo.Photo;
                    $scope.profileUserInfo.Passwordconfirm = $scope.profileUserInfo.Password;
                });
            });
        }

        $scope.imageUpload = function (event) {
            var files = event.target.files; //FileList object

            if (files.length > 0) {
                var file = files[0];
                var reader = new FileReader();
                reader.onload = function () {

                    var buffer = reader.result;
                    var uint8 = new Uint8Array(buffer);
                    var result = [];
                    for (var i = 0; i < uint8.length; i++) {
                        result.push(uint8[i]);
                    }
                    $scope.content = result;

                };
                //reader.readAsDataURL(file);
                reader.readAsArrayBuffer(file);
            }
        }


        $scope.showContent = function ($fileContent) {
            $scope.content = $fileContent;
            //console.log($fileContent);
        };
        $scope.update = function () {
            //if ($scope.profileUserInfo.Password === $scope.profileUserInfo.Passwordconfirm)
                if ($scope.updateorgform.$valid) {
                    if ($scope.profileUserInfo.Passwordconfirm !== $scope.profileUserInfo.Password) {
                        $window.alert('Password not matched');
                    }
                    else {
                        console.log($scope.profileUserInfo);
                        if ($scope.content != null) {
                            if ($scope.content != null)
                                $scope.profileUserInfo.Photo = $scope.content;
                            console.log($scope.profileUserInfo);
                            $http({
                                method: 'Put',
                                url: appContext.adminServiceBaseUrl + 'api/UserInfo/UpdateProfile?id=' + $scope.profileUserInfo.Id,
                                data: $scope.profileUserInfo
                            }).then(function successCallback(response) {
                                $window.location.reload();

                            }, function errorCallback(response) {

                                console.log("Error : " + response.data);
                            });
                        }

                        else {
                            $scope.updateorgform.submitted = true;
                        }
                    }

                }
        }
    }
    ]);

OneHub360.directive('onReadFile', function ($parse) {
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