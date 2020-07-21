
OneHub360.controller('newItemStripController', ['$scope', '$state', '$http', function ($scope, $state, $http) {

        var messageId = $state.params.message;


        switch (messageId) {
            case '1':
                $scope.message = 'لا يوجد محتوي للعرض يرجي إنشاء محتوي من خلال إختيار نوع محتوي من القائمة';
                break;
            default:
                $scope.message = 'يمكنك إنشاء محتوي جديد من خلال إختيار نوع محتوي من القائمة';
        }

        var feedWebApiCallUrl = appContext.appServiceBaseUrl + 'api/feedtype/newitemslist';

        $scope.status = 0;

        $http.get(feedWebApiCallUrl)
            .success(function (data, status, headers, config) {
                $scope.status = 1;
                $scope.feedData = data;
            })
            .error(function (data, status, headers, config) {
                console.log(data);
            });
    }]);
