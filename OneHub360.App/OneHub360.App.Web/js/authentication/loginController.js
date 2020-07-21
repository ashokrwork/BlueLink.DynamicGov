'use strict';
OneHub360.controller
    ('loginController',
    ['$scope', '$state', '$http','$rootScope','$cookies',
        function ($scope, $state, $http, $rootScope, $cookies) {
            
            $scope.init = function()
            {
                $scope.isModeSelect = false;


                if ($state.params.mode == undefined || $state.params.mode == '') {
                    $state.go('login', { mode: 'user', message: '401' });
                }
                else {
                    if (appContext.SupportedModes.indexOf($state.params.mode) > 0) {
                        //$state.go('login', { mode: 'user', message: '401' });
                        $scope.isModeSelect = false;
                    }
                }

                $scope.mode = $state.params.mode;

                $scope.domain = 'mof\\';
                var currentWindowsUser = "";

                var messageId = $state.params.message;

                HubStorage().Mode = $state.params.mode;


                switch (messageId) {
                    case '401':
                        $scope.message = 'يرجي إدخال بيانات الدخول';
                        break;
                    case '402':
                        $scope.message = 'تم تسجيل الخروج';
                        break;
                    default:
                        $scope.message = 'يرجي ';
                }


                $scope.loginData = {
                    userName: currentWindowsUser,
                    password: ""
                };

                $scope.status = 0;
            }
            /////////////////////Autologin
            var cookie = true;
            var ADUsername = $cookies.get('ADUsername');
            //console.log(ADUsername);
            //var ADUsername = "yousif@onehub360.demo"
            if (cookie)
            {
                $scope.status = 1;
                $scope.message = 'جاري تسجيل الدخول';
                var token = '';
                var url = window.location.href;
                //console.log(url);
                //console.log(url.search('user'));
                //console.log(url.search('register'));
                if (url.search('register') < 0) {
                    //Correspondence 
                    var data = "grant_type=password&username=" + ADUsername + "#$%S$" + "2609FE8C-C597-4AC4-B751-A73D00A00C8D";
                }
                else {
                    //register
                    var data = "grant_type=password&username=" + ADUsername + "#$%S$" + "5A3464A2-4031-47FD-BE09-A75700B77826";
                }
                $http.post(appContext.appServiceBaseUrl + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                    if (response.userId == '') {
                        $scope.errormessage = response.userName;
                    }
                    else {
                        HubStorage().currentUserId = response.userId;
                        HubStorage().authorizationData = response.access_token;
                        $rootScope.UpdatePeopleSource();
                        $rootScope.Mode = HubStorage().Mode;

                        $state.go('feed', { option: '1' });
                    }
                }).error(function (response) {
                    console.log(response);
                    $scope.status = 0;
                    $scope.message = 'حدث خطأ, يرجي المحاولة مرة أخري';
                });
            }

            //////////////////////
            $scope.errormessage = '';
            $scope.doLogin = function () {
                console.log('Logging in');
                $scope.status = 1;
                $scope.message = 'جاري تسجيل الدخول';
                var token = '';
                var url = window.location.href;
                //console.log(url);
                //console.log(url.search('user'));
                //console.log(url.search('register'));
                if (url.search('register') < 0) {
                    //Correspondence 
                    var data = "grant_type=password&username=" + $scope.domain + $scope.loginData.userName + "#$%S$" + "2609FE8C-C597-4AC4-B751-A73D00A00C8D" + "&password=" + $scope.loginData.password;
                }
                else {
                    //register
                    var data = "grant_type=password&username=" + $scope.domain + $scope.loginData.userName + "#$%S$" + "5A3464A2-4031-47FD-BE09-A75700B77826" + "&password=" + $scope.loginData.password;
                }
                $http.post(appContext.appServiceBaseUrl + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                    if (response.userId == '')
                    {
                        $scope.errormessage = response.userName;
                        console.log(response);
                    }
                    else
                    {
                        HubStorage().currentUserId = response.userId;
                        HubStorage().authorizationData = response.access_token;
                        $rootScope.UpdatePeopleSource();
                        $rootScope.Mode = HubStorage().Mode;

                        $state.go('feed', { option: '1' });
                    }
                }).error(function (response) {
                    console.log(response);
                    $scope.status = 0;
                    $scope.message = 'حدث خطأ, يرجي المحاولة مرة أخري';
                });
            };
            $scope.doLogout = function()
            {
                swal({
                    title: "هل انت متأكد؟",
                    text: "سيتم تسجيل الخروج من النظام",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "نعم",
                    cancelButtonText: "لا",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        HubStorage().clear();
                        $state.go('login', { message: '402', mode: '' });
                        swal({ title: "تمت", text: "تم تسجيل الخروج", type:"success",timer:3000 });
                    } else {
                        
                        swal({ title: "تمت", text: "لم يتم تسجيل الخروج يمكنك المتابعة", type: "success", timer: 1000 });

                    }
                });

                //bootbox.confirm({
                //    message: "سيتم تسجيل الخروج من النظام, متابعة؟",
                //    buttons: {
                //        confirm: {
                //            label: 'نعم',
                //            className: 'btn-success'
                //        },
                //        cancel: {
                //            label: 'لا',
                //            className: 'btn-danger'
                //        }
                //    },
                //    callback: function (result) {
                //        if(result)
                //        {
                //            HubStorage().clear();
                //                        $state.go('login', { message: '402', mode: '' });
                //        }
                //    }
                //});

                //$.prompt("سيتم تسجيل الخروج من النظام, إستمرار؟", {
                //    title: "تسجيل الخروج",
                //    buttons: { "نعم, قم بتسجيل الخروج": true, "لا": false },
                //    submit: function (e, v, m, f) {
                //        // use e.preventDefault() to prevent closing when needed or return false.
                //        // e.preventDefault();

                //        if(v)
                //        {
                //            HubStorage().clear();
                //            console.log(appContext.Mode);
                //            $state.go('login', { message: '402', mode: appContext.Mode });
                //        }
                //    }
                //});
            }
            $scope.EnableModeSelect = function()
            {
                $state.go('login', { mode: '', message: '401' });
            }

            $scope.SelectMode = function(option)
            {
               
                $scope.ModeOption = option;
                if(option == 1)
                {
                    $state.go('login', { mode: 'user', message: '401' });
                }
                else
                {
                    $state.go('login', { mode: 'register', message: '401' });
                }
            }
}]);