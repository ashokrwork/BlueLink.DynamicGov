OneHub360.controller('printPDFController',
    ['$scope','$state', function 
        ($scope, $state)
    {
        $scope.Init()
        {
            $scope.PrintUrl = "/components/modules/cms/pdfjs/web/viewer.html?file=" + $state.params.url;
        }
    }]);