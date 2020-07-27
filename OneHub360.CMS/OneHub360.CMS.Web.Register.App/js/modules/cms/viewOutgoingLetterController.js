

OneHub360.controller('viewOutgoingLetterController', ['$scope', '$http', '$timeout', '$state', '$rootScope',
    function ($scope, $http, $timeout, $state, $rootScope) {
        
        $scope.fullScreen = false;
        $scope.complete = function () {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/complete/' + $scope.Id + "/" + 33;
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
                        console.log(webApiCall);
                        $state.go('outgoing');

                    })
                    .error(function (data) {
                    });
            });
        }
        $scope.toggleFullScreen = function()
        {
            $scope.fullScreen = !$scope.fullScreen;
        }

        $scope.$on('reloadAfterAction', function (event, args) { $scope.LoadLetter(true); });
        $scope.Reject = function () {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/Reject/';
           
            var to = $scope.letterData.To;
            $scope.letterData.To = $scope.letterData.From;
            $scope.letterData.RegisteredBy = HubStorage().currentUserId;
            $scope.letterData.CreatedBy = HubStorage().currentUserId;

            $scope.letterData.From = to;
            //$scope.letterData.Confidential = true;
            $scope.letterData.Subject = "رفض : " + $scope.letterData.Subject;

            if (confirm('سيتم رفض الوثيقة, متابعة؟')) {

                var reason = prompt("من فضلك اكتب سبب الرفض", "الرفض:");

                if (reason != null) {
                    $scope.letterData.RejectedReason = reason;
                    
                    $http.post(
                     webApiCall,
                     JSON.stringify($scope.letterData),
                     {
                         headers: {
                             'Content-Type': 'application/json'
                         }
                     }
                 ).success(function (data) {
                     $state.go('outgoing');
                 })
                     .error(function (data) {
                         console.log(data);
                     });
                  
                }
                   
              

             

            }
        }
        $scope.LoadLetter = function (setActionView)
        {
            $scope.status = 0;
            $scope.Id = $state.params.id;

            $scope.Load(setActionView);
        }

        $scope.Load = function (setActionView)
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/view/' + $scope.Id;
            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.status = 1;
                       $scope.letterData = data;
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
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/attachements/' + $scope.Id;

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
                       //$scope.status = -1;
                       console.log(data);
                   });
            });
        }

        $scope.ShowDocumentMainDocument = function()
        {
            $scope.ShowDocument({ 'ViewUrl': $scope.letterData.MainDocumentViewUrl, 'Title': 'الوثيقة الرئيسية', 'Icon': 'fa-file-pdf-o' });
        }

        $scope.ShowAttachement = function (document)
        {
            document.Icon = 'fa-paperclip';
            $scope.ShowDocument(document);
        }

        $scope.ShowDocument = function(document)
        {
            $scope.IsDocumentView = true;
            document.ViewUrl = document.ViewUrl + encodeURIComponent('?userId=' + HubStorage().getItem('currentUserId') + '&access_token=' + sessionStorage.getItem('currentUserId'));
            $scope.CurrentDocument = document;
        }

        $scope.ShowActions = function () {
            $scope.IsDocumentView = false;
        }
        $scope.PrintDocument = function () {


            $("#printFrame").remove();

            var hiddenFrame = $("<iframe id='printFrame'></iframe>");
            hiddenFrame.hide();

            var url = '/components/modules/cms/webprint/OneHub360.CMS.Web.Print.xbap?Id=' + $state.params.id + '&callBackFunction=ReloadAfterScan';

            hiddenFrame.attr({ display: 'none', src: url });
            $("body").append(hiddenFrame);


        }
        //using the right call
        //var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingLetter/register';
        //to do 
      
    }
    
]);