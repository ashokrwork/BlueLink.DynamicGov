window.onbeforeunload = function () {
    
    //var activeTabs = localStorage.getItem('activeTabs');

    //if (typeof activeTabs !== 'undefined' && activeTabs !== null && activeTabs !== "NaN")
    //{
    //    activeTabsInt = parseInt(activeTabs);
    //    localStorage.activeTabs = (activeTabsInt - 1);
    //    if (activeTabsInt <= 0)
    //        localStorage.lastSessionClose = Date.now();
    //}

    return undefined;
};

window.onload = function () {
    
    //console.log(localStorage.lastSessionClose);
    //console.log(localStorage.activeTabs);

    //var activeTabs = localStorage.getItem('activeTabs');

    //if (typeof activeTabs !== 'undefined' && activeTabs !== null && activeTabs !== "NaN") {

    //    activeTabsInt = parseInt(activeTabs);

    //    if (activeTabsInt <= 0) {

    //        console.log('Found tabs = 0');

    //        var lastSavedSessionDate = localStorage.getItem('lastSessionClose');

    //        if (typeof lastSavedSessionDate !== 'undefined' && lastSavedSessionDate !== null) {

    //            var dif = ((Date.now() - lastSavedSessionDate) / 1000);
    //            console.log(dif);

    //            if (dif > 5) {
    //                console.log('Clearing');
    //                localStorage.clear();
    //            }
    //        }

    //        activeTabsInt = 0;
    //    }
    //    console.log('Adding 1');
    //    localStorage.activeTabs = (activeTabsInt + 1);

    //}
    //else {
    //    console.log('Setting to 1');
    //    localStorage.activeTabs = 1;
    //}

    //console.log(localStorage.lastSessionClose);
    //console.log(localStorage.activeTabs);
};