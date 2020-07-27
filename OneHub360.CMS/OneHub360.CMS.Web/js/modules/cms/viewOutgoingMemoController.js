

OneHub360.controller('viewOutgoingMemoController', ['$scope', '$http', '$timeout', '$state', '$rootScope',
    function ($scope, $http, $timeout, $state, $rootScope) {
        
        $scope.fullScreen = false;

        $scope.toggleFullScreen = function()
        {
            $scope.fullScreen = !$scope.fullScreen;
        }

        $scope.$on('reloadAfterAction', function (event, args) { $scope.LoadMemo(true); });

        $scope.LoadMemo = function (setActionView)
        {
            $scope.status = 0;
            $scope.Id = $state.params.id;

            $scope.Load(setActionView);
        }

        $scope.Load = function (setActionView)
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/outgoingmemo/view/' + $scope.Id;
            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.status = 1;
                       $scope.memoData = data;
                       $scope.GetAttachements(false);
                       $scope.ShowDocumentMainDocument();
                       if (setActionView) { $scope.ShowActions(); }
                   })
                   .error(function (data, status, headers, config) {
                       $scope.status = -1;
                       console.log(data);
                   });
            });
        }

        $scope.selectAttachement = function (file) {

            file.title = file.name.split('.')[0];
            $scope.attachementFile = file;

        };

        $scope.GetAttachements = function(isReload)
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/attachements/' + $scope.Id;

            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.status = 1;
                       $scope.attachmentsData = data;

                       if (isReload) {
                           if ($scope.attachmentsData.length == 0) {
                               $scope.ShowDocumentMainDocument();
                           }
                           else {
                               $scope.ShowAttachement($scope.attachmentsData[0]);
                           }
                       }
                   })
                   .error(function (data, status, headers, config) {
                       $scope.status = -1;
                       console.log(data);
                   });
            });
        }

        $scope.ShowDocumentMainDocument = function () {
            $(".alert-warning").removeClass("alert-warning").addClass("alert-info");
            $scope.ShowDocument({ 'ViewUrl': $scope.memoData.MainDocumentViewUrl, 'Title': 'الوثيقة الرئيسية', 'Icon': 'fa-file-pdf-o', 'IsWebViewable': true, 'ViewElement': 'iframe' });
        }

        $scope.ShowAttachement = function (document) {
            $(".alert-warning").removeClass("alert-warning").addClass("alert-info");

            var thisId = "#" + document.Id;
            $(thisId).toggleClass("alert-info alert-warning");
            document.Icon = 'fa-paperclip';
            $scope.ShowDocument(document);
        }

        $scope.ShowDocument = function (document) {
            console.log(document);
            $scope.IsDocumentView = true;
            if (document.IsWebViewable) {
                document.ViewUrl = document.ViewUrl;//+ encodeURIComponent('?userId=' + HubStorage().getItem('currentUserId')); //+ '&access_token=' + sessionStorage.getItem('currentUserId'));
            }
            $scope.CurrentDocument = document;
        }

        $scope.ShowActions = function () {
            $scope.IsDocumentView = false;
        }

        $scope.ShowActions = function () {
            $scope.IsDocumentView = false;
        }
    }
]);