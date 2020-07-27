


OneHub360.controller('createDraftMemoController', [
    '$scope', '$http', '$timeout', 'ngToast','$state', function (
        $scope, $http, $timeout, ngToast,$state) {

        $scope.addSelectedFrom = function (selected) {

            if (selected != null) {
                $scope.selectedFrom = selected.originalObject.Id;
            }
        }

        $scope.addSelectedTo = function(selected)
        {
            if (selected != null) { $scope.selectedTo = selected.originalObject.Id; }
        }

        $scope.removeSelectedTo = function (index) {
            $scope.selectedTo.splice(index, 1);
        }

        $scope.Init = function () {

            $scope.selectedTo = [];
            $scope.status = 0;
            $scope.CurrentUser = true;
            $scope.selectedFrom = HubStorage().currentUserId;

            if ($state.params.id == null) {
                $scope.isNew = true;
                var data = new Object();

                data.CreatedBy = HubStorage().currentUserId;
                data.From = HubStorage().currentUserId;
                data.Confidential = false;

                $scope.memoData = data;
                $scope.status = 1;
            }
            else
            {
                $scope.isNew = false;
                var webApiCall = cmsContext.cmsServiceBaseUrl + "cms/draftmemo/getforupdate/" + $state.params.id;

                $timeout(function () {
                    $scope.status = 0;
                    $http.get(
                        webApiCall
                    )
                        .success(function (data) {
                            $scope.memoData = data;
                            $scope.selectedFrom = data.From;
                            $scope.CurrentFrom = true;
                            $scope.selectedTo = data.To;
                            $scope.CurrentTo = true;
                            $scope.status = 1;
                        })
                        .error(function (data) {
                            $scope.status = 1;
                            ngToast.create('حدث خطأ');
                            console.log(data);
                        });
                });
            }
        }

        $scope.PostData = function () {

            if ($scope.selectedFrom == '') {
                ngToast.create('يرجي إختيار قيمة للمرسل');
                return;
            }

            if ($scope.selectedTo == '') {
                ngToast.create('يرجي إختيار قيمة للمرسل إليه');
                return;
            }

            

            $scope.memoData.To = $scope.selectedTo;
            $scope.memoData.From = $scope.selectedFrom;

            if ($scope.isNew) {
                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftMemo/create';


                $timeout(function () {
                    $scope.status = 0;
                    $http.post(
                        webApiCall,
                        JSON.stringify($scope.memoData),
                        {
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }
                    )
                        .success(function (data) {
                            ngToast.create('تم إنشاء المذكرة بنجاح');
                            $state.go('viewdraftmemo', { id: data });
                            $scope.status = 1;
                        })
                        .error(function (data) {
                            $scope.status = 1;
                            ngToast.create('حدث خطأ');
                            console.log(data);
                        });
                });
            }
            else
            {
                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftMemo/update';



                $timeout(function () {
                    $scope.status = 0;
                    $http.post(
                        webApiCall,
                        JSON.stringify($scope.memoData),
                        {
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }
                    )
                        .success(function (data) {
                            ngToast.create('تم تحديث المذكرة بنجاح');
                            $state.go('viewdraftmemo', { id: $state.params.id });
                            $scope.status = 1;
                        })
                        .error(function (data) {
                            $scope.status = 1;
                            ngToast.create('حدث خطأ');
                            console.log(data);
                        });
                });
            }
        }

        $scope.AllowSelectFrom = function()
        {
            $scope.selectedFrom = '';
            $scope.CurrentUser = false;
            if ($scope.isNew) {
                $scope.memoData.Confidential = true;
            }
            $scope.CurrentFrom = false;
        }

        $scope.AllowSelectTo = function () {
            $scope.selectedTo = '';
            $scope.CurrentTo = false;
        }

    }]);
