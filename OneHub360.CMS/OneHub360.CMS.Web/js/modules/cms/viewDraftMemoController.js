

OneHub360.controller('viewDraftMemoController',
    ['$scope', '$http', '$timeout', '$state', '$rootScope', 'Globals', 'ngToast',
    function ($scope, $http, $timeout, $state, $rootScope, Globals, ngToast) {
        
        var popOverSettings = {
            placement: 'top',
            container: 'body',
            html: true,
            selector: '[rel="IconRed"]'
        }

        $scope.isIE = Globals.isIE;

        $scope.ScanAppLink = '/components/modules/cms/webscan/OneHub360.CMS.Web.Scan.xbap?Id=' + $state.params.id + '&callBackFunction=ReloadAfterScan';
        //$scope.PrintAppLink = '/components/modules/cms/webprint/OneHub360.CMS.Web.Print.xbap?Id=' + $state.params.id + '&callBackFunction=ReloadAfterScan';

        $('body').popover(popOverSettings);

        $scope.fullScreen = false;

        $scope.toggleFullScreen = function()
        {
            $scope.fullScreen = !$scope.fullScreen;
        }

        $scope.$on('reloadAfterAction', function (event, args) { $scope.LoadMemo(true); });

        $scope.openUpdateMultiSend = function()
        {
            $uibModal.open({
                templateUrl: '/partials/modules/cms/forms/managemultisend.html',
                resolve: {
                    id: function () {
                        return $scope.Id;
                    }
                }
            });
        }

        $scope.LoadMemo = function (setActionView)
        {
            $scope.status = 0;
            $scope.Id = $state.params.id;

            $scope.Load(setActionView);
        }

        $scope.Load = function (setActionView)
        {
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/' + Math.random() + '/draftmemo/view/' + $scope.Id;

            $timeout(function () {
                $http.get(webApiCall)
                   .success(function (data, status, headers, config) {
                       $scope.status = 1;
                       $scope.memoData = data;
                       $scope.GetAttachements(false);
                       $scope.ShowDocumentMainDocument();
                       console.log('Multi send');
                       console.log(data);
                       $scope.AddtionalToCount = $.parseJSON(data.AddtionalRecipients).length;
                       $scope.CopyToCount = $.parseJSON(data.CopyTo).length;

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

        $scope.ValidateFileName = function ($event)
        {
            var fileNameRegx = /\`|\~|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\+|\=|\[|\{|\]|\}|\||\\|\'|\<|\,|\.|\>|\?|\/|\""|\;|\:/g

            if (fileNameRegx.test($scope.attachementFile.title) || $scope.attachementFile.title === undefined) {
                $($event.target).parent().addClass('has-error');
            }
            else
            {
                $($event.target).parent().removeClass('has-error');
            }
        }

        $scope.UploadAttachement = function () {
           
           

            var fileNameRegx = /\`|\~|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\+|\=|\[|\{|\]|\}|\||\\|\'|\<|\,|\.|\>|\?|\/|\""|\;|\:/g

            if ($scope.attachementFile.title === undefined)
            {
                ngToast.create('يرجي إدخال إسم للملف');
                return;
            }

            if (fileNameRegx.test($scope.attachementFile.title))
            {
                ngToast.create('يرجي تعديل إسم الملف وإزالة الحروف الخاصة ك $ و !');
                return;
            }

            $scope.Uploading = true;

            if ($scope.attachementFile.IsOneDrive)
            {
                var uploadFormData = new FormData();

                uploadFormData.append('fileUrl', $scope.attachementFile.OneDriveUrl);
                uploadFormData.append('title', $scope.attachementFile.title);
                uploadFormData.append('memoId', $scope.memoId);
                uploadFormData.append('fileName', $scope.attachementFile.name);

                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/' + $scope.Id + '/attachement/addfromonedrive';


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
                console.log('Starting upload');
                var uploadFormData = new FormData();

                uploadFormData.append('file', $scope.attachementFile);
                uploadFormData.append('title', $scope.attachementFile.title);
                uploadFormData.append('memoId', $scope.memoId);
                uploadFormData.append('fileName', $scope.attachementFile.name);

                var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/' + $scope.Id + '/attachement/add';

                console.log(webApiCall);

                var securePost = $rootScope.makeSecurePost(webApiCall, uploadFormData, undefined);

                securePost.success(function (data) {
                    console.log('Done');
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
            var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/attachements/' + $scope.Id;

            console.log(webApiCall);

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
            $(".alert-warning").removeClass("alert-warning").addClass("alert-info");

            $scope.ShowDocument({ 'ViewUrl': $scope.memoData.MainDocumentViewUrl + '%3FuserId%3D' + HubStorage().currentUserId, 'Title': 'الوثيقة الرئيسية', 'FileIcon': 'fa fa-file-word-o', 'IsWebViewable': true, 'ViewElement': 'iframe', 'IsEditable': true, 'EditUrl': cmsContext.cmsServiceBaseUrl + 'cms/document/file/download/' + $scope.memoData.Id });
        }

        $scope.ShowAttachement = function (document)
        {
            $(".alert-warning").removeClass("alert-warning").addClass("alert-info");

            var thisId = "#" + document.Id;
            $(thisId).toggleClass("alert-info alert-warning");
            document.Icon = 'fa-paperclip';
            $scope.ShowDocument(document);
        }

        $scope.switchViews = function (isDocument) {
            if (isDocument) {
                $('#document').addClass('active');
                $('#documenttab').addClass('active');

                $('#actionstab').removeClass('active');
                $('#actions').removeClass('active');
            }
            else {
                $('#document').removeClass('active');
                $('#documenttab').removeClass('active');

                $('#actionstab').addClass('active');
                $('#actions').addClass('active');
            }
        }

        $scope.ShowDocument = function(document)
        {
            console.log(document);
            $scope.IsDocumentView = true;
            $scope.switchViews(true);
            if (document.IsWebViewable) {
                document.ViewUrl = document.ViewUrl;//+ encodeURIComponent('?userId=' + HubStorage().getItem('currentUserId')); //+ '&access_token=' + sessionStorage.getItem('currentUserId'));
            }
            $scope.CurrentDocument = document;
        }

        $scope.ShowActions = function () {
            $scope.IsDocumentView = false;
            $scope.switchViews(false)
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
                clientId: "1cc8de8a-f5a8-4533-a9ea-ec6bb811f806",
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
                cancel: function () {  },
                error: function (e) { alert(e); }
            };

            OneDrive.open(odOptions);
        }
    }
]);