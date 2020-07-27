OneHub360.controller('outgoingLettersController',
    ['$scope', '$rootScope', '$http', '$timeout', 'ngToast', '$state', '$notification', function
        ($scope, $rootScope, $http, $timeout, ngToast, $state, $notification) {

        var feedWebApiCallUrl = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/filter/';
        var feedWebApiCountCallUrl = cmsContext.cmsServiceBaseUrl + 'cms/outgoingletter/getcount/';

        //to do
        $scope.LoadLetters = function () {
            $timeout(function () {

                $rootScope.makeSecurePost(feedWebApiCallUrl, JSON.stringify($scope.filteroption), 'application/json')
                .success(function (data, status, headers, config) {
                    if (data.length == 0) {
                        $scope.noData = true;
                    }
                    $scope.status = 1;
                    $scope.lettersData = data;

                    //if (isReload) {
                    //    ngToast.create('تم إعادة تحميل البيانات بنجاح');
                    //}

                    //$scope.ShowNotification();
                })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.lettersData = data;
                    });
                //Load records count
                $rootScope.makeSecurePost(feedWebApiCountCallUrl, JSON.stringify($scope.filteroption), 'application/json')
               .success(function (data, status, headers, config) {
                   if (data.length == 0) {
                       $scope.noData = true;
                   }
                   $scope.Pages = [];
                   $scope.pagecount = data;
                   if ($scope.pagecount > 0)
                       for (var i = 0; i < $scope.pagecount; i++) {
                           $scope.Pages.push(i + 1);
                       }
                   else $scope.Pages = [];
               })
                   .error(function (data, status, headers, config) {
                       $scope.status = -1;
                       $scope.lettersData = data;
                   });

            });
        }

        $scope.datacount = 1;
        $scope.Pages = [];
        $scope.pagecount = 1;
        $scope.filteroption = {
            filtertype: '',
            isfirstload: true,
            statustext: '',
            status: 0,
            datatype: $state.params.option, PageLength: cmsContext.PageLength,
            pageNumber: 1,
            Fromdate: null, Todate: null, Fromuserobject: undefined, Touserobject: undefined, Fromuser: '', Touser: ''
        }
        $scope.filteroption.filtertype = 'All';

        $scope.reset = function () {
            $scope.filteroption.filtertype = 'All'
            $scope.filteroption.isfirstload = true;
            $scope.filteroption.statustext = '';
            $scope.filteroption.status = 0;
            $scope.filteroption.datatype = $state.params.option;
            $scope.filteroption.PageLength = cmsContext.PageLength,
            $scope.filteroption.pageNumber = 1;
            $scope.filteroption.Fromdate = null;
            $scope.filteroption.Todate = null;
            $scope.filteroption.Fromuserobject = undefined;
            $scope.filteroption.Touserobject = undefined;
            $scope.filteroption.Fromuser = '';
            $scope.filteroption.Touser = '';
            $scope.LoadLetters();
        }
        $scope.filter = function () {
            $timeout(function () {

                console.log($scope.filteroption);
                $scope.filteroption.status = $scope.filteroption.statustext;



                if ($scope.filteroption.Fromuserobject != undefined) {
                    $scope.filteroption.Fromuser = $scope.filteroption.Fromuserobject.originalObject.Id

                }
                if ($scope.filteroption.Touserobject != undefined) {
                    $scope.filteroption.Touser = $scope.filteroption.Touserobject.originalObject.Id
                }
                $scope.filteroption.isfirstload = false;
                $scope.filteroption.pageNumber = 1;

                $rootScope.makeSecurePost(feedWebApiCountCallUrl, JSON.stringify($scope.filteroption), 'application/json')
          .success(function (data, status, headers, config) {
              if (data.length == 0) {
                  $scope.noData = true;
              }
              $scope.Pages.length = 0;
              $scope.pagecount = data;
              if ($scope.pagecount > 0)
                  for (var i = 0; i < $scope.pagecount; i++) {
                      $scope.Pages.push(i + 1);
                  }
              else $scope.Pages.length = 0;
          })
              .error(function (data, status, headers, config) {
                  $scope.status = -1;
                  $scope.lettersData = data;

              });

                $rootScope.makeSecurePost(feedWebApiCallUrl, JSON.stringify($scope.filteroption), 'application/json')
                .success(function (data, status, headers, config) {
                    if (data.length == 0) {
                        $scope.noData = true;
                    }
                    $scope.lettersData = data;

                    //$scope.ShowNotification();
                })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.lettersData = data;
                    });

            });


        }
        $scope.Getnextpage = function (page) {

            $timeout(function () {
                $scope.filteroption.pageNumber = page;
                $rootScope.makeSecurePost(feedWebApiCallUrl, JSON.stringify($scope.filteroption), 'application/json')
                .success(function (data, status, headers, config) {
                    if (data.length == 0) {
                        $scope.noData = true;
                    }
                    $scope.lettersData = data;
                    //$scope.ShowNotification();
                })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.lettersData = data;
                    });
            });

        }
        //$scope.LoadLetters = function()
        //{
        //    var incomingLettersApiCallUrl = appContext.appServiceBaseUrl + 'cms/outgoingletter/get/' + IncomingLetterStatus.All;

        //    $http.get(incomingLettersApiCallUrl)
        //        .success(function (data, status, headers, config) {
        //            if (data.length == 0) {
        //                $scope.noData = true;
        //            }
        //            $scope.status = 1;
        //            $scope.lettersData = data;
        //        })
        //            .error(function (data, status, headers, config) {
        //                $scope.status = -1;
        //                $scope.lettersData = data;
        //            });
        //}

    
        $scope.map = function (history) {

            if (history.isMapped)
                return history;

            var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
            //var creationDay = new Date(history.CreationDate);
            //creationDay.setHours(0,0,0,0);
            //history.CreationDay = creationDay.toLocaleDateString('ar-kw', options);

            var today = moment().range(moment().startOf('day'), moment().endOf('day'));
            //var today = moment();
            var creationDay = moment(history.CreationDate).startOf('day');

            var thisWeek = moment().range(moment().startOf('week').add(1, 'days'), moment().endOf('week').add(-1, 'days'));
            var thisMonth = moment().range(moment().startOf('month'), moment().endOf('month'));

            history.duration = moment.duration(moment(history.CreationDate).diff(moment())).humanize(true);

            if (today.contains(creationDay)) {
                history.Category = 'Today';
            }
            else if (thisWeek.contains(creationDay)) {
                history.Category = 'Week';
            }
                //else if (thisMonth.contains(creationDay))
                //{
                //    history.Category = 'Month';
                //}
            else {
                history.Category = 'Old';
            }

            history.isMapped = true;

            return history;
        };
    }]);