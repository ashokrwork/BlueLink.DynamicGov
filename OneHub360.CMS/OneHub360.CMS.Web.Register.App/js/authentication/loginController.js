'use strict';
OneHub360.controller
    ('loginController',
    ['$scope', '$state', '$http','$rootScope',
        function ($scope, $state, $http, $rootScope) {
            
            $scope.init = function()
            {
                $scope.isModeSelect = false;


                if ($state.params.mode == undefined || $state.params.mode == '') {
                    $state.go('login', {mode:'register',message:'401'});
                }
                else {
                    if (appContext.SupportedModes.indexOf($state.params.mode) > 0) {
                        //$state.go('login', { mode: 'user', message: '401' });
                        $scope.isModeSelect = false;
                    }
                }

                $scope.mode = $state.params.mode;

                $scope.domain = 'cpa\\';
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
            function getCookie(cname) {
                var name = cname + "=";
                var decodedCookie = decodeURIComponent(document.cookie);
                var ca = decodedCookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) == ' ') {
                        c = c.substring(1);
                    }
                    if (c.indexOf(name) == 0) {
                        return c.substring(name.length, c.length);
                    }
                }
                return "";
            }
            /////////////////////Autologin
            var cookie = true;
            var ADUsername = getCookie('ADUsername');
            if (cookie) {

                $scope.errormessage = '';
               
                    $scope.status = 1;
                    $scope.message = 'جاري تسجيل الدخول';
                    var token = '';
                    var url = window.location.href;
                    //console.log(url);
                    //console.log(url.search('user'));
                    console.log(url.search('register'));
                    if (url.search('register') < 0) {
                        //Correspondence 
                        var data = "grant_type=password&username=" + ADUsername + "#$%S$" + "2609FE8C-C597-4AC4-B751-A73D00A00C8D" ;
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

                            $state.go('incoming');
                        }
                    }).error(function (response) {
                        console.log(response);
                        $scope.status = 0;
                        $scope.message = 'حدث خطأ, يرجي المحاولة مرة أخري';
                    });
                
            }
            $scope.errormessage = '';
            $scope.doLogin = function () {
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
                    if (response.userId == '') {
                        $scope.errormessage = response.userName;
                    }
                    else {
                        HubStorage().currentUserId = response.userId;
                        HubStorage().authorizationData = response.access_token;
                        $rootScope.UpdatePeopleSource();
                        $rootScope.Mode = HubStorage().Mode;

                        $state.go('incoming');
                    }
                }).error(function (response) {
                    console.log(response);
                    $scope.status = 0;
                    $scope.message = 'حدث خطأ, يرجي المحاولة مرة أخري';
                });
            };
            $scope.doLogout = function()
            {

                bootbox.confirm({
                    message: "سيتم تسجيل الخروج من النظام, متابعة؟",
                    buttons: {
                        confirm: {
                            label: 'نعم',
                            className: 'btn-success'
                        },
                        cancel: {
                            label: 'لا',
                            className: 'btn-danger'
                        }
                    },
                    callback: function (result) {
                        if(result)
                        {
                            HubStorage().clear();
                                        $state.go('login', { message: '402', mode: '' });
                        }
                    }
                });

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