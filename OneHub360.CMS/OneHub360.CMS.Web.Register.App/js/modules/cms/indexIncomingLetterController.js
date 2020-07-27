OneHub360.controller('indexIncomingLetterController', ['$scope', '$http', '$timeout', '$state', '$rootScope', '$window',
function ($scope, $http, $timeout, $state, $rootScope, $window) {

    $scope.fullScreen = false;

    $scope.toggleFullScreen = function () {
        $scope.fullScreen = !$scope.fullScreen;
    }
    $scope.ScanAppLink = '/components/modules/cms/webscan/OneHub360.CMS.Web.Scan.xbap?Id=' + $state.params.id + '&type=2&callBackFunction=ReloadAfterScan';
    $scope.ScanAppLinkAttachment = '/components/modules/cms/webscan/OneHub360.CMS.Web.Scan.xbap?Id=' + $state.params.id + '&type=1&callBackFunction=ReloadAfterScan';


    $scope.$on('reloadAfterAction', function (event, args) { $scope.LoadLetter(true); });

    $scope.Replace = function () {
        changestatus();
    }

    $scope.Edit = function () {
        $state.go('');
    }
  

    changestatus = function () {
        var webApiCallupdatestatus = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/complete/' + $scope.Id + "/" + 31;
        //var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/draftmemo/attachement/delete/' + $scope.Id;

        if (confirm('سيتم حذف الوثيقة و استبدالها, متابعة؟')) {
            var securePost1 = $rootScope.makeSecurePost(webApiCallupdatestatus, undefined, undefined);
           // var securePost2 = $rootScope.makeSecurePost(webApiCallupdatestatus, undefined, undefined);

            securePost1.success(function (data) {
                $scope.letterData.Status = 31;
            });
            securePost1.error(function (data) {
            });
           // securePost2.success(function (data) {
              //  $scope.letterData.Status = 31;
                //window.location.reload(false);
           // });

            //securePost2.error(function (data) {
            //});

        }





    }
    $scope.Delete = function () {
        var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/delete/' + $scope.Id;

        if (confirm('سيتم حذف الكتاب, متابعة؟')) {
            var securePost = $rootScope.makeSecurePost(webApiCall, undefined, undefined);

            securePost.success(function (data) {
                $state.go('incoming');
            });
            securePost.error(function (data) {
            });

        }
    }

    $scope.LoadLetter = function (setActionView) {
        $scope.status = 0;
        $scope.Id = $state.params.id;

        $scope.Load(setActionView);
    }

    $scope.Load = function (setActionView) {
        var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/view/' + $scope.Id;
        $timeout(function () {
            $http.get(webApiCall)
               .success(function (data, status, headers, config) {
                   $scope.status = 1;
                   $scope.letterData = data;
                   //console.log(data);
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

    $scope.selectmainAttachement = function (file) {

        file.title = file.name.split('.')[0];
        $scope.UploadMainDocument(file);
        $scope.LoadLetter(false);


    };

    $scope.GetAttachements = function (isReload) {
        var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingletter/attachements/' + $scope.Id;

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

    $scope.ShowDocumentMainDocument = function () {
        $scope.ShowDocument({ 'ViewUrl': $scope.letterData.MainDocumentViewUrl, 'Title': 'الوثيقة الرئيسية', 'Icon': 'fa-file-pdf-o' });
    }

    $scope.ShowAttachement = function (document) {
        document.Icon = 'fa-paperclip';
        $scope.ShowDocument(document);
    }

    $scope.ShowDocument = function (document) {
        $scope.IsDocumentView = true;
        document.ViewUrl = document.ViewUrl;
        $scope.CurrentDocument = document;
    }

    $scope.ShowActions = function () {
        $scope.IsDocumentView = false;
    }
    $scope.DeleteAttachement = function (id) {
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
    $scope.UploadAttachement = function () {


        $scope.Uploading = true;

        if ($scope.attachementFile.IsOneDrive) {
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

    $scope.UploadMainDocument = function (file) {


        $scope.Uploading = true;


        console.log('Starting upload');
        var uploadFormData = new FormData();

        uploadFormData.append('file', file);
        uploadFormData.append('title', "الوثيقة الرئيسية");
        uploadFormData.append('memoId', $state.params.id);
        uploadFormData.append('fileName', "الوثيقة الرئيسية.pdf");

        var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/incomingmemo/' + $state.params.id + '/main/add';

        console.log(webApiCall);

        var securePost = $rootScope.makeSecurePost(webApiCall, uploadFormData, undefined);

        securePost.success(function (data) {
            console.log('Done');
            $scope.Uploading = false;
            $scope.attachementFile = null;

            window.location.reload(false);

            //$scope.GetAttachements(true);
        });

        securePost.error(function (data) {
            console.log(data);
            $scope.Uploading = false;
        });
    }

    $scope.Forward = function () {
        var webApiCall = cmsContext.cmsServiceBaseUrl + 'cms/reg/incomingletter/forward/' + $scope.Id;
        $timeout(function () {
            $http.get(webApiCall)
               .success(function (data, status, headers, config) {
                   $scope.status = 1;
                   console.log(data);
                   alert('تم الإرسال بنجاح');
                   $state.go('incoming');
               })
               .error(function (data, status, headers, config) {
                   $scope.status = -1;
                   console.log(data);
               });
        });
    }
}
]);