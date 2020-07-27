/// <reference path="C:\S\OneHub\OneHub360.CMS\OneHub360.CMS.Web\components/modules/cms/pdfjs/api/api.js" />

var OneHub360 = angular.module('OneHub360',[]);


OneHub360.controller('printDocumentsController',
    ['$scope', function
        ($scope) {

        var totalNum = 0;

        $scope.PrintAll = function (fileToPrint) {
            
            var canvas = document.createElement('canvas');
            canvas.id = "1234";

            PDFJS.disableWorker = true;
            PDFJS.getDocument(fileToPrint).then(function (pdfDoc) { renderPages(pdfDoc, canvas); }).then(function ()
            {
                //var pageNum = pdfDoc.numPages;

               // var ctx = canvas.getContext('2d');
               // var data = ctx.getImageData(0, 0, canvas.width, canvas.height);
               // console.log(data);

               // var uri = canvas.toDataURL();
               // console.log(uri);

               // var dataUrl = canvas.toDataURL(); //attempt to save base64 string to server using this var  
               // var windowContent = '<!DOCTYPE html>';
               // windowContent += '<html>'
               // windowContent += '<head><title>Print canvas</title></head>';
               // windowContent += '<body>'
               // windowContent += '<img src="' + uri + '">';
               // windowContent += '</body>';
               // windowContent += '</html>';
               // var printWin = window.open('', '', 'width=340,height=260');
               // printWin.document.open();
               // printWin.document.write(windowContent);
               // printWin.document.close();
               // printWin.focus();
               //// printWin.print();
               //// printWin.close();

            });
        }

        function renderPage(page, canvas) {
            var canvasContainer = document.getElementById('holder');
            var viewport = page.getViewport(1.5);
            
            var ctx = canvas.getContext('2d');
            var renderContext = {
                canvasContext: ctx,
                viewport: viewport
            };

            canvas.height = viewport.height;
            canvas.width = viewport.width;
            canvasContainer.appendChild(canvas);

            page.render(renderContext).promise.then(function () {
                totalNum--;
                console.log(totalNum);

                if(totalNum == 0)
                {
                    console.log('Adding image');
                    var dataUrl = canvas.toDataURL();
                    var img = document.createElement('img');
                    img.src = dataUrl;
                    canvasContainer.appendChild(img);
                }

                //if(totalNum == 0)
                //{
                //    var dataUrl = canvas.toDataURL(); //attempt to save base64 string to server using this var  
                //    var windowContent = '<!DOCTYPE html>';
                //    windowContent += '<html>'
                //    windowContent += '<head><title>Print canvas</title></head>';
                //    windowContent += '<body>'
                //    windowContent += '<img src="' + dataUrl + '">';
                //    windowContent += '</body>';
                //    windowContent += '</html>';
                //    var printWin = window.open('', '', 'width=340,height=260');
                //    printWin.document.open();
                //    printWin.document.write(windowContent);
                //    printWin.document.close();
                //    printWin.focus();
                //    printWin.print();
                //    printWin.close();
                //}
            });

        }

        function renderPages(pdfDoc, canvas) {
            totalNum = pdfDoc.numPages;
            for (var num = 1; num <= totalNum; num++) {
                console.log('Rendering page ' + num);
                pdfDoc.getPage(num).then(function (page) { renderPage(page, canvas); });
            }
           
        }
    }]);

