OneHub360.controller('profileController',
    [
        '$scope', '$http', '$timeout', '$rootScope', function (
            $scope, $http, $timeout, $rootScope) {

            
            $scope.SignatureAppLink = "/wpf/modules/app/signature/OneHub360.App.WPF.Signature.xbap?userId=" + HubStorage().currentUserId;
            
            $scope.Init = function () {
                $scope.status = 0;
                $timeout(function () {

                    var userInfo = $.grep($rootScope.peopleSource, function (e) { return e.Id == HubStorage().currentUserId });
                    $scope.UserInfo = userInfo[0];

                    var signatureApiCallUrl = appContext.appServiceBaseUrl + 'api/user/signature/' + HubStorage().currentUserId;

                    console.log(signatureApiCallUrl);

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
                });
            }
        }
    ]);