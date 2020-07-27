

OneHub360.controller('viewDraftLetterController', ['$scope', '$http', '$timeout', '$state', '$rootScope','Globals',
    function ($scope, $http, $timeout, $state, $rootScope, Globals) {
        
        var popOverSettings = {
            placement: 'top',
            container: 'body',
            html: true,
            selector: '[rel="IconRed"]'
        }
        $scope.isIE = Globals.isIE;

        $('body').popover(popOverSettings);
        $scope.ScanAppLink = '/components/modules/cms/webscan/OneHub360.CMS.Web.Scan.xbap?Id=' + $state.params.id + '&type=1&callBackFunction=ReloadAfterScan';

        $scope.fullScreen = false;

        $scope.toggleFullScreen = function()
        {
            $scope.fullScreen = !$scope.fullScreen;
        }

        $scope.$on('reloadAfterAction', function (event, args) { $scope.LoadLetter(true); });

        $scope.LoadLetter = function (setActionView)
        {
            $scope.status = 0;
            $scope.Id = $state.params.id;

            $scope.Load(setActionView);
        }

        $scope.Load = function (setActionView)
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftletter/view/' + $scope.Id;
            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.status = 1;
                       $scope.letterData = data;
                       $scope.GetAttachements(false);
                       $scope.ShowDocumentMainDocument();
                       if (setActionView) { $scope.ShowActions();}
                   })
                   .error(function (data, status, headers, config) {
                       $scope.status = -1;
                       console.log(data);
                   });
            });
        }

        $scope.selectAttachement = function (file) {
            
            file.title = file.name.split('.')[0];
            file.IsOneDrive = false;
            $scope.attachementFile = file;

        };

        $scope.UploadAttachement = function () {
           

            $scope.Uploading = true;

            if ($scope.attachementFile.IsOneDrive)
            {
                var uploadFormData = new FormData();

                uploadFormData.append('fileUrl', $scope.attachementFile.OneDriveUrl);
                uploadFormData.append('title', $scope.attachementFile.title);
                uploadFormData.append('memoId', $scope.memoId);
                uploadFormData.append('fileName', $scope.attachementFile.name);

                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftletter/' + $scope.Id + '/attachement/addfromonedrive';


                var securePost = $rootScope.makeSecurePost(webApiCall, uploadFormData, undefined);

                securePost.success(function (data) {
                    $scope.Uploading = false;
                    $scope.attachementFile = null;
                    $scope.GetAttachements(true);
                });

                securePost.error(function (data) {
                    console.log(data);
                    $scope.Uploading = false;
                });
            }
            else {

                var uploadFormData = new FormData();

                uploadFormData.append('file', $scope.attachementFile);
                uploadFormData.append('title', $scope.attachementFile.title);
                uploadFormData.append('memoId', $scope.memoId);
                uploadFormData.append('fileName', $scope.attachementFile.name);

                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftletter/' + $scope.Id + '/attachement/add';


                var securePost = $rootScope.makeSecurePost(webApiCall, uploadFormData, undefined);

                securePost.success(function (data) {
                    $scope.Uploading = false;
                    $scope.attachementFile = null;
                    $scope.GetAttachements(true);
                });

                securePost.error(function (data) {
                    console.log(data);
                    $scope.Uploading = false;
                });
            }
        }

        $scope.GetAttachements = function(isReload)
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftletter/attachements/' + $scope.Id;

            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.attachmentsData = data;

                       if (isReload) {
                           if ($scope.attachmentsData.length === 0) {
                               $scope.ShowDocumentMainDocument();
                           }
                           else {
                               $scope.ShowAttachement($scope.attachmentsData[0]);
                           }
                       }
                   })
                   .error(function (data, status, headers, config) {
                       console.log(data);
                   });
            });
        }

        $scope.ShowDocumentMainDocument = function()
        {
            $scope.ShowDocument({ 'ViewUrl': $scope.letterData.MainDocumentViewUrl, 'Title': 'الوثيقة الرئيسية', 'Icon': 'fa-file-word-o' });
        }

        $scope.ShowAttachement = function (document)
        {
            document.Icon = 'fa-paperclip';
            $scope.ShowDocument(document);
        }

        $scope.ShowDocument = function(document)
        {
            $scope.IsDocumentView = true;
            document.ViewUrl = document.ViewUrl + encodeURIComponent('?userId=' + HubStorage().getItem('currentUserId')); //+ '&access_token=' + sessionStorage.getItem('currentUserId'));
            $scope.CurrentDocument = document;
        }

        $scope.ShowActions = function () {
            $scope.IsDocumentView = false;
        }

        $scope.DeleteAttachement = function (id)
        {
            if (confirm('سيتم حذف المرفق, متابعة؟')) {
                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/attachement/delete/' + id;

                $timeout(function () {
                    $http.get(webApiCall)
                       .success(function (data, status, headers, config) {
                           $scope.GetAttachements(true);
                       })
                       .error(function (data, status, headers, config) {
                           $scope.status = -1;
                           console.log(data);
                       });
                });
            }
        }

        $scope.openFromOneDrive = function()
        {
            var odOptions = {
                clientId: "56109c83-a999-4b58-b43c-d6ddbabc0b5e",
                action: "download",
                multiSelect: false,
                openInNewWindow: true,
                advanced: {},
                success: function (files) {
                    var file = new Object();
                    file.title = files.value[0].name.split('.')[0];
                    file.name = files.value[0].name;
                    file.IsOneDrive = true;
                    file.OneDriveUrl = files.value[0]["@microsoft.graph.downloadUrl"];
                    $scope.attachementFile = file;
                    $scope.$apply();
                },
                cancel: function () { alert('Cancel'); },
                error: function (e) { alert(e); }
            };

            OneDrive.open(odOptions);
        }
    }
]);