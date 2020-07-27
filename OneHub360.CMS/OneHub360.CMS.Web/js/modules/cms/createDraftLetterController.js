OneHub360.controller('createDraftLetterController', [
    '$scope', '$http', '$timeout', 'ngToast', '$state', function (
        $scope, $http, $timeout, ngToast, $state) {
        $scope.PersonTitle = '';
        $scope.addSelectedFrom = function (selected) {

            if (selected != null) { $scope.selectedFrom = selected.originalObject.Id; }
        }

        $scope.addSelectedTo = function (selected) {
            if (selected != null) {
                $scope.selectedTo = selected.originalObject.Id;
                $scope.PersonTitle = selected.originalObject.PersonTitle
            }
        }

        $scope.Init = function () {

            $scope.selectedTo = '';
            $scope.status = 0;
            $scope.CurrentUser = true;
            $scope.selectedFrom = HubStorage().currentUserId;
            var data = new Object();

            if ($state.params.threadid != null) {
                data.ThreadId = $state.params.threadid;
            }

            if ($state.params.id == null) {
                $scope.isNew = true;
                var data = new Object();

                data.CreatedBy = HubStorage().currentUserId;
                data.From = HubStorage().currentUserId;
                data.Confidential = false;

                $scope.letterData = data;
                $scope.status = 1;
            }
            else
            {
                $scope.isNew = false;
                var webApiCall = cmsContext.cmsServiceBaseUrl + "cms/draftletter/getforupdate/" + $state.params.id;

                $timeout(function () {
                    $scope.status = 0;
                    $http.get(
                        webApiCall
                    )
                        .success(function (data) {
                            $scope.letterData = data;
                            $scope.selectedFrom = data.From;
                            $scope.CurrentFrom = true;
                            $scope.selectedTo = data.To;
                            console.log($scope.letterData);
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



            $scope.letterData.To = $scope.selectedTo;
            $scope.letterData.From = $scope.selectedFrom;
            $scope.letterData.PersonTitle = $scope.PersonTitle;

            if ($scope.isNew) {
                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftLetter/create';



                $timeout(function () {
                    console.log($scope.letterData);
                    $scope.status = 0;
                    $http.post(
                        webApiCall,
                        JSON.stringify($scope.letterData),
                        {
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }
                    )
                        .success(function (data) {
                            ngToast.create('تم إنشاء الكتاب بنجاح');
                            $state.go('viewdraftletter', { id: data });
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
                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftLetter/update';



                $timeout(function () {
                    $scope.status = 0;
                    $http.post(
                        webApiCall,
                        JSON.stringify($scope.letterData),
                        {
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }
                    )
                        .success(function (data) {
                            ngToast.create('تم تحديث المذكرة بنجاح');
                            $state.go('viewdraftletter', { id: $state.params.id });
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


           
        
        $scope.AllowSelectTo = function () {
            $scope.selectedTo = '';
            $scope.CurrentTo = false;
        }
        $scope.AllowSelectFrom = function () {
            $scope.selectedFrom = '';
            $scope.CurrentUser = false;
            $scope.letterData.Confidential = true;
        }

    }]);
