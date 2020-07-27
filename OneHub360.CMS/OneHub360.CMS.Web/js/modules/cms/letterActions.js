OneHub360.controller('letterActions',
    ['$scope', '$http', '$timeout', 'ngToast', '$uibModal','$state', function
        ($scope, $http, $timeout, ngToast, $uibModal, $state) {

        $scope.actionStatus = 1;

        // Draft Actions
        $scope.sendTitle = "إرسال";
        $scope.InitForDraft = function(Id)
        {
            $scope.letterId = Id;
            $scope.downloadUrl = cmsContext.cmsServiceBaseUrl + 'cms/draftletter/download/' + $scope.letterId;
        }

        $scope.selectedTo = '';

        $scope.forwardMemoBtnText = 'تحويل';
        $scope.createForwardMemoBtnText = 'إنشاء مسودة للرد';

        $scope.addSelectedTo = function (selected) {
            if (selected != null) { $scope.selectedTo = selected.originalObject.Id; }
        }

        $scope.InitForOutgoing = function (Id) {
            $scope.letterId = Id;
            $scope.downloadUrl = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/download/' + $scope.letterId;
        }

        $scope.InitForIncoming = function (Id) {
            $scope.letterId = Id;
            $scope.downloadUrl = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/download/' + $scope.letterId;
        }

        $scope.sendDraftMemo = function()
        {
            $scope.actionStatus = 0;
            console.log($scope);
            $scope.sendTitle = "جاري العمل";

            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftletter/send/' + $scope.letterId + '/' + HubStorage().currentUserId;

            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.sendTitle = "إرسال";
                       ngToast.create('تم إرسال الكتاب بنجاح');
                       $scope.$emit('reloadAfterAction', $scope.letterId);
                       $scope.$emit('removeFeedItemFromView', $scope.letterId);
                       $scope.actionStatus = 1;
                   })
                   .error(function (data, status, headers, config) {
                       $scope.action = 1;
                       console.log(data);
                   });
            });
        }

        $scope.PrintDraftMemo = function()
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/print/' + $scope.letterId;
            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       for (var i = 0, len = data.length; i < len; i++) {
                           

                       }
                   })
                   .error(function (data, status, headers, config) {
                       $scope.status = -1;
                       console.log(data);
                   });
            });
        }

        $scope.ShowForwardIncomingLetter = function (Id,Subject,ThreadId)
        {
            
            $scope.action = new Object();
            $scope.action.Subject = Subject;
            $scope.action.FK_IncomingletterId = Id;
            $scope.action.FK_From = HubStorage().getItem('currentUserId');
            $scope.action.SelectedAttachements = [];
            $scope.action.AttachAll = true;
            $scope.action.ThreadId = ThreadId;
            $scope.action.Title = 'تحويل كتاب واردة';
            $scope.CurrentModal = $uibModal.open({
                templateUrl: '/partials/modules/cms/forms/forwardincomingletter.html',
                scope: $scope
            });
            //$scope.GetIncomingMemoAttachements();
        }

        $scope.ShowReplyIncomingMemo = function (Id, Subject, ThreadId,toUser) {

            $scope.action = new Object();
            $scope.action.Subject = Subject;
            $scope.action.FK_IncomingletterId = Id;
            $scope.action.FK_From = HubStorage().currentUserId;
            $scope.action.SelectedAttachements = [];
            $scope.action.AttachAll = true;
            $scope.action.ThreadId = ThreadId;
            $scope.action.IsReply = true;
            $scope.action.Title = 'الرد علي مراسلة واردة';
            $scope.selectedTo = toUser;
            $scope.CurrentModal = $uibModal.open({
                templateUrl: '/partials/modules/cms/forms/forwardincomingmemo.html',
                scope: $scope
            });
            $scope.GetIncomingMemoAttachements();
        }

        $scope.PrintIncomingMemo = function(Id)
        {
            $scope.action = new Object();
            $scope.action.PrintUrl = "/components/modules/cms/pdfjs/web/viewer.html?file=" + encodeURIComponent(cmsContext.cmsServiceBaseUrl + "cms/incomingmemo/print/" + Id);
            $scope.CurrentModal = $uibModal.open({
                templateUrl: '/partials/modules/cms/forms/printpdf.html',
                scope: $scope,
                size: 'lg'
            });
        }

        $scope.SwitchAttachementMode = function()
        {
            $scope.action.AttachAll = !$scope.action.AttachAll;
        }

        $scope.GetIncomingMemoAttachements = function () {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingmemo/documents/' + $scope.letterId;

            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       data.map(function (a) { a.checked = true });
                       $scope.incomingMemoAttachements = data;
                   })
                   .error(function (data, status, headers, config) {
                       console.log(data);
                   });
            });
        }

        $scope.ForwardIncomingLetter = function () {

            
            if ($scope.selectedTo == '') {
                ngToast.create('يرجي إختيار قيمة للمرسل إليه');
                return;
            }



            $scope.action.FK_To = $scope.selectedTo;
            $scope.action.Send = true;

            //var selectAttachements = $scope.incomingMemoAttachements.filter(function (a) { return a.checked; }).map(function (a) { return a.Id; });

            //$scope.action.SelectedAttachements = selectAttachements;

            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/forward';

            $timeout(function () {
                $scope.actionStatus = 0;
                $http.post(
                    webApiCall,
                    JSON.stringify($scope.action),
                    {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }
                )
                    .success(function (data) {

                        if ($scope.action.Send)
                        {
                            ngToast.create('تمت التوجيه بنجاح');
                            $state.go('viewoutgoingmemo', { id: data });
                        }
                        else {
                            $state.go('viewdraftmemo', { id: data });
                            ngToast.create('تم إنشاء المذكرة بنجاح');
                        }

                        
                        $scope.actionStatus = 1;
                    })
                    .error(function (data) {
                        $scope.actionStatus = 1;
                        ngToast.create('حدث خطأ');
                        console.log(data);
                    });
            });
        }

        $scope.close = function (reload) {
            if (reload) {
                $state.go($state.current.name, $state.current.params, { reload: true });
                $scope.CurrentModal.close();
            }
            else {
                $scope.CurrentModal.dismiss();
            }
        }
        $scope.PrintDocument = function () {
            $("#printFrame").remove();

            var hiddenFrame = $("<iframe id='printFrame'></iframe>");
            hiddenFrame.hide();

            var url = '/components/modules/cms/webprint/OneHub360.CMS.Web.Print.xbap?Id=' + $state.params.id + '&callBackFunction=ReloadAfterScan';

            hiddenFrame.attr({ display: 'none', src: url });
            $("body").append(hiddenFrame);


        }
}]);