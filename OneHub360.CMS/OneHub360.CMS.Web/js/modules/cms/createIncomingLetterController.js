OneHub360.controller('createIncomingLetterController', [
    '$scope', '$http', '$timeout', 'ngToast', '$state', function (
        $scope, $http, $timeout, ngToast, $state) {

        $scope.outgoingnumbers = [];



        $scope.addSelectedFrom = function (selected) {

            if (selected != null) { $scope.selectedFrom = selected.originalObject.Id; }
        }

        $scope.addSelectedTo = function (selected) {
            if (selected != null) { $scope.selectedTo = selected.originalObject.Id; }
        }

        $scope.Init = function () {

            $scope.selectedTo = '';
            $scope.status = 0;
            $scope.CurrentUser = false;
            $scope.selectedFrom = HubStorage().currentUserId;

            var data = new Object();

            data.CreatedBy = HubStorage().currentUserId;
            data.RegisteredBy = HubStorage().currentUserId;
            data.From = HubStorage().currentUserId;
            data.Confidential = false;

            $scope.letterData = data;
            $scope.status = 1;
            //var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/outgoingnumbers';

            //$timeout(function () {
                
            //    $http.get(
            //        webApiCall,
            //        {
            //            headers: {
            //                'Content-Type': 'application/json'
            //            }
            //        }
            //    )
            //        .success(function (data) {
            //            $scope.outgoingnumbers = data;
            //        })
            //        .error(function (data) {
            //            console.log(data);
            //        });
            //});
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



            $scope.letterData.To = $scope.selectedFrom;
            $scope.letterData.From = $scope.selectedTo;
            $scope.letterData.Confidential = true;
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingLetter/register';



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
                        ngToast.create('تم إنشاء الكتاب بنجاح');
                        $state.go('viewincomingletter', { id: data });
                        $scope.status = 1;
                    })
                    .error(function (data) {
                        $scope.status = 1;
                        ngToast.create('حدث خطأ');
                        console.log(data);
                    });
            });
        }

        $scope.AllowSelectFrom = function () {
            $scope.selectedFrom = '';
            $scope.CurrentUser = false;
            $scope.letterData.Confidential = true;
        }

    }]);
