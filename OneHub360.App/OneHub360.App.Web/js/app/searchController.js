OneHub360.controller('searchController',
    [
        '$scope', '$http', '$timeout', '$state', function ($scope, $http, $timeout, $state) {


            $scope.date = {
                startDate: moment().subtract(1, "days"),
                endDate: moment()
            };

            $scope.init = function () {

                console.log('Search loaded...');

                


                

            }

        }]);