OneHub360.controller('multiSendController',
    ['$scope', '$http', '$timeout', '$state', 'ngToast',
    function ($scope, $http, $timeout, $state, ngToast) {

        console.log('Loaded');

        $scope.status = 0;

        $scope.AddtionalRecipientsList = [];
        $scope.CopyToList = [];
        $scope.MultiSendInfo = new Object();

        $scope.loadMultiSend = function () {

            $timeout(function () {
                var shareWebApiCallUrl = appContext.appServiceBaseUrl + 'cms/draftmemo/getmultisend/' + $state.params.id;

                $http.get(shareWebApiCallUrl)
                    .success(function (data, status, headers, config) {
                        $scope.status = 1;
                        $scope.MultiSendInfo = data;
                        $scope.AddtionalRecipientsList = $.parseJSON(data.AddtionalRecipients);
                        $scope.CopyToList = $.parseJSON(data.CopyTo);
                        console.log(data);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                        $scope.status = -1;
                    });
            });
        }

        $scope.addAddtionalRecipients = function (selectedUser) {
            if (selectedUser != null) {
                if (!$scope.AddtionalRecipientsList.indexOf(selectedUser.originalObject.Id) !== -1) {
                    $scope.AddtionalRecipientsList.push(selectedUser.originalObject.Id);
                }
                else {
                    console.log('User selected before');
                }
            }
        }

        $scope.removeAddtionalRecipients = function (index) {
            $scope.AddtionalRecipientsList.splice(index, 1);
        }

        $scope.addCopyTo = function (selectedUser) {
            if (selectedUser != null) {
                if (!$scope.CopyToList.indexOf(selectedUser.originalObject.Id) !== -1) {
                    $scope.CopyToList.push(selectedUser.originalObject.Id);
                }
                else {
                    console.log('User selected before');
                }
            }
        }

        $scope.removeCopyTo = function (index) {
            $scope.CopyToList.splice(index, 1);
        }

        $scope.updateMultiSend = function () {
            $scope.MultiSendInfo.AddtionalRecipients = JSON.stringify($scope.AddtionalRecipientsList);

            $scope.MultiSendInfo.CopyTo = JSON.stringify($scope.CopyToList);

            console.log($scope.MultiSendInfo);
            $timeout(function () {
                var shareWebApiCallUrl = appContext.appServiceBaseUrl + 'cms/draftmemo/updatemultisend';

                $http.post(shareWebApiCallUrl, JSON.stringify($scope.MultiSendInfo),
                        {
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        })
                    .success(function (data, status, headers, config) {
                        $scope.status = 1;
                        ngToast.create('تم الحفظ بنجاح');
                        $scope.close(true);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                        $scope.status = -1;
                        $scope.ShareInfo = data;
                    });
            });
        }

        $scope.close = function (reload) {
            
        }

    }]);

