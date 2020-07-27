OneHub360.controller('createIncomingLetterController', [
    '$scope', '$http', '$timeout', 'ngToast', '$state', function (
        $scope, $http, $timeout, ngToast, $state) {

        $scope.outgoingnumbers = [];
        $scope.Subject = '';


        $scope.addSelectedFrom = function (selected) {

            if (selected != null) { $scope.selectedFrom = selected.originalObject.Id; }
        }

        $scope.addSelectedTo = function (selected) {
            if (selected != null) { $scope.selectedTo = selected.originalObject.Id; }
        }

       
        $scope.Init = function () {
            $scope.selectedTo = '';
            $scope.status = 0;
            $scope.CurrentUser = true;
            $scope.selectedFrom = HubStorage().currentUserId;

            var data = new Object();

            data.CreatedBy = HubStorage().currentUserId;
            data.RegisteredBy = HubStorage().currentUserId;
            data.From = HubStorage().currentUserId;
            data.Confidential = false;

            if ($state.params.id == null) {
                $scope.isNew = true;
                var data = new Object();


                data.CreatedBy = HubStorage().currentUserId;
                data.RegisteredBy = HubStorage().currentUserId;
                data.From = HubStorage().currentUserId;
                data.Confidential = false;

                $scope.letterData = data;
                $scope.status = 1;
            }
            else {
                $scope.isNew = false;
                var webApiCall = cmsContext.cmsServiceBaseUrl + "cms/incomingLetter/getforupdate/" + $state.params.id;

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
                            $scope.CurrentTo = true;
                            $scope.status = 1;
                        })
                        .error(function (result) {
                            $scope.status = 1;
                            ngToast.create('حدث خطأ');
                            console.log(result);
                        });
                });
            }



            $scope.status = 1;

            console.log('Loaded');
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

            if ($scope.isNew) {
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
                            //$state.go('incoming');
                            $scope.status = 1;
                        })
                        .error(function (data) {
                            $scope.status = 1;
                            ngToast.create('حدث خطأ');
                            console.log(data);
                        });
                });
            }
            else {
                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingLetter/update';

                //console.log($scope.letterData);
                //console.log(JSON.stringify($scope.letterData));
               // var postcontent = [$scope.letterData.Id, $scope.letterData.From, $scope.letterData.To,
                 //   $scope.letterData.Subject, $scope.letterData.OutgoingNumber];
               // console.log(postcontent);
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
                            ngToast.create('تم تحديث الكتاب بنجاح');
                            $state.go('indexincomingletter', { id: $state.params.id });
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
            $scope.CurrentFrom = false;
            $scope.letterData.Confidential = true;
        }

    }]);
