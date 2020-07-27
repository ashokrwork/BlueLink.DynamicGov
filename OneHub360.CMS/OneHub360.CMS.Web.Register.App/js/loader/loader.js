/// <reference path="../jquery/jquery-3.1.0.min.js" />
$.ajaxSetup({cache: false});

InitApp();

var peopleSource = [];

function InitApp() {

    SessionTransfer();

    BrowserFixes();

    var peoplePickerCacheServiceUrl = appContext.appServiceBaseUrl + 'api/user/autocomplete';

    $.ajax({
        url: peoplePickerCacheServiceUrl,
        type: 'GET',
        success: function (data) {
            peopleSource = data;
            angular.bootstrap(document, ['OneHub360']);
            $('#mainLoadingContainer').remove();
            document.title = 'نظام التراسل الإلكتروني';
        },
        error: function (data) {
            console.log('Error');
            console.log(data);
        }
    });
}

function BrowserFixes()
{
    if (!String.prototype.includes) {
        console.log('IE');
        String.prototype.includes = function () {
            'use strict';
            return String.prototype.indexOf.apply(this, arguments) !== -1;
        };
    }
}

function SessionTransfer()
{
    var sessionStorage_transfer = function (event) {
        if (!event) { event = window.event; } // ie suq
        if (!event.newValue) return;          // do nothing if no value to work with
        if (event.key == 'getSessionStorage') {
            // another tab asked for the sessionStorage -> send it
            localStorage.setItem('sessionStorage', JSON.stringify(sessionStorage));
            // the other tab should now have it, so we're done with it.
            localStorage.removeItem('sessionStorage'); // <- could do short timeout as well.
        } else if (event.key == 'sessionStorage' && !sessionStorage.length) {
            // another tab sent data <- get it
            var data = JSON.parse(event.newValue);
            for (var key in data) {
                sessionStorage.setItem(key, data[key]);
            }
        }
    };

    // listen for changes to localStorage
    if (window.addEventListener) {
        window.addEventListener("storage", sessionStorage_transfer, false);
    } else {
        window.attachEvent("onstorage", sessionStorage_transfer);
    };


    // Ask other tabs for session storage (this is ONLY to trigger event)
    if (!sessionStorage.length) {
        localStorage.setItem('getSessionStorage', 'foobar');
        localStorage.removeItem('getSessionStorage', 'foobar');
    };
}