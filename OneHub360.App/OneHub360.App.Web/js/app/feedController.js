
OneHub360.controller('feedController',
    ['$scope', '$rootScope', '$http', '$timeout', 'ngToast', '$state', '$notification', '$uibModal', function
        ($scope, $rootScope, $http, $timeout, ngToast, $state, $notification, $uibModal) {


        $scope.status = 0;

        switch ($state.params.option) {
            case '0':
                $scope.pageTitle = 'المسودات';
                break;
            case '1':
                $scope.pageTitle = 'الوارد';
                break;
            case '2':
                $scope.pageTitle = 'الصادر';
                break;
        }


        var feedWebApiCallUrl = appContext.appServiceBaseUrl + 'api/feed/filter/';
        var feedWebApiCountCallUrl = appContext.appServiceBaseUrl + 'api/feed/getcount/';

        var feedSearchWebApiCallUrl = appContext.appServiceBaseUrl + 'api/feed/search/';

        function createNotification() {
            if (window.Notification && Notification.permission !== "denied") {
                Notification.requestPermission(function (status) {  // status is "granted", if accepted by user
                    var n = new Notification('مذكرة واردة', {
                        body: 'بخصوص مشروع التراسل الإلكتروني',
                        icon: 'http://localhost/img/app/OneHub360_icon.png', // optional,
                        lang: 'ar-kw',
                        dir: 'rtl'
                    });

                    n.onclick = function () {
                        $state.go('viewoutgoingmemo', { id: '34c94e5c-dcbf-49a4-8728-beec94c0115b' });
                        n.close();
                    };
                });
            }
        }

        $scope.ShowNotification = function () {
            $timeout(createNotification, 2000);

        }

        $scope.clock = "loading clock..."; // initialise the time variable
        $scope.tickInterval = 1000; //ms

        var tick = function () {
            $scope.clock = moment().format('Do MMMM YYYY, mm:h a'); // get the current time
            $timeout(tick, $scope.tickInterval); // reset the timer
        }

        // Start the timer
        $timeout(tick, $scope.tickInterval);

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
        $scope.datacount = 1;
        $scope.Pages = [];
        $scope.pagecount = 1;
        $scope.filteroption = {
            filtertype: '',
            isfirstload: true,
            statustext: '',
            status: 0,
            datatype: $state.params.option, PageLength: appContext.PageLength,
            pageNumber: 1,
            Fromdate: null, Todate: null, Fromuserobject: undefined, Touserobject: undefined, Fromuser: '', Touser: ''
        }
        $scope.filteroption.filtertype = 'All';

        $scope.reset = function () {
            $scope.filteroption.filtertype = 'All'
            $scope.filteroption.isfirstload= true;
            $scope.filteroption.statustext= '';
            $scope.filteroption.status= 0;
            $scope.filteroption.datatype= $state.params.option;
            $scope.filteroption.PageLength= appContext.PageLength,
            $scope.filteroption.pageNumber= 1;
            $scope.filteroption.Fromdate= null;
            $scope.filteroption.Todate= null;
            $scope.filteroption.Fromuserobject= undefined;
            $scope.filteroption.Touserobject= undefined;
            $scope.filteroption.Fromuser = '';
            $scope.filteroption.Touser = '';
            $scope.filter();
        }
        $scope.filter = function () {
            $timeout(function () {
             
                console.log($scope.filteroption.filtertype);

                if ($scope.filteroption.statustext == "new")
                    $scope.filteroption.status = 1;

                if ($scope.filteroption.statustext == "completed")
                    $scope.filteroption.status = 2;

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
                  $scope.feedData = data;

              });

                $rootScope.makeSecurePost(feedWebApiCallUrl, JSON.stringify($scope.filteroption), 'application/json')
                .success(function (data, status, headers, config) {
                    if (data.length == 0) {
                        $scope.noData = true;
                    }
                    $scope.feedData = data;

                    $scope.ShowNotification();
                })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.feedData = data;
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
                    $scope.feedData = data;
                    //$scope.ShowNotification();
                })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.feedData = data;
                    });
            });

        }
        $scope.LoadFeeds = function (isReload) {

            $timeout(function () {

                $rootScope.makeSecurePost(feedWebApiCallUrl, JSON.stringify($scope.filteroption), 'application/json')
                .success(function (data, status, headers, config) {
                    if (data.length == 0) {
                        $scope.noData = true;
                    }
                    $scope.status = 1;
                    $scope.feedData = data;

                    

                    //$(".card").flip();

                    if (isReload) {
                        ngToast.create('تم إعادة تحميل البيانات بنجاح');
                    }

                    //$scope.ShowNotification();
                })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.feedData = data;
                        if (status == 401) {
                            HubStorage().clear();
                            $state.go('login', { message: '401', mode: '' });
                        }
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
                       $scope.feedData = data;
                       if (status == 401) {
                           HubStorage().clear();
                           $state.go('login', { message: '401', mode: '' });
                       }
                   });

            });
        }

        $scope.initSearch = function () {
            $scope.criteria = new Object();
            $scope.criteria.Keyword = $state.params.term;
            $scope.SearchFeeds($scope.criteria);
        }

        $scope.ReSearchFeeds = function () {
            $scope.noData = false;
            $scope.SearchFeeds($scope.criteria);
        }

        $scope.search = function()
        {
            $state.go('search');
        }

        $scope.SearchFeeds = function (criteria) {

            $timeout(function () {

                $rootScope.makeSecurePost(feedSearchWebApiCallUrl, JSON.stringify($scope.criteria), 'application/json')
                    .success(function (data, status, headers, config) {
                        if (data.length == 0) {
                            $scope.noData = true;
                        }
                        $scope.status = 1;
                        $scope.feedData = data;


                        //$scope.ShowNotification();
                    })
                    .error(function (data, status, headers, config) {
                        $scope.status = -1;
                        $scope.feedData = data;
                    });


            });
        }

        $scope.SlideCategory = function ($event, category) {
            if (category != 'Today') {
                $($event.currentTarget).next().toggle();
            }
        }

        $scope.$on('removeFeedItemFromView', function (event, args) { $scope.RemoveFeedItem(args.id); });

        $scope.RemoveFeedItem = function (id) {
            var removeIndex = $scope.feedData.map(function (item) { return item.id; }).indexOf(id);
            $scope.feedData.splice(removeIndex, 1);
        }

        $scope.openPreviewDialog = function (feedItem, controllerName, templatePath) {
            $uibModal.open({
                controller: controllerName,
                templateUrl: templatePath,
                resolve: {
                    itemData: function () {
                        return feedItem;
                    }
                }
            });
        }

    }]);

OneHub360.directive('feeditem', function () {
    return {
        link: function (scope, element, attrs) {
            scope.getContentUrl = function () {
                return (attrs.template + '.' + HubStorage().Mode + '.html');
            }
        },
        template: '<div  ng-include="getContentUrl()"></div>'
    }
});
