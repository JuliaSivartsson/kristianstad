var cookieName = "grundskola";
var cookieSaveDays = 2;
initialize();

var buttons = document.getElementsByClassName("compareButton");
for (i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", function () {
        
        // Creates a cookie to add.
        var cookie = this.href.split("#")[1];
        
        // Gets the cookie collection.
        var cookieCollection = getCookie(cookieName);

        // If the collection does not exist, we create a new collection.
        if (!Array.isArray(cookieCollection))
        {
            cookieCollection = Array();
        }

        // We check if the created cookie already exists in the collection.
        var alreadyAdded = false;
        for (var value in cookieCollection) {
            if (cookie == cookieCollection[value])
            {
                alreadyAdded = true;
            }
        }

        if (alreadyAdded)
        {
            newCookieCollection = Array();
            for (var value in cookieCollection) {
                if (cookie != cookieCollection[value])
                {
                    newCookieCollection.push(cookieCollection[value]);
                }
            }

            cookieCollection = newCookieCollection;
        }
        else {
            cookieCollection.push(cookie);
        }
        setCookie(cookieName, cookieCollection, cookieSaveDays);
    });
}

// Set a Cookie.
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + JSON.stringify(cvalue) + "; " + expires + ";path=/";
    location.reload();
}

// Get a Cookie.
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return JSON.parse(c.substring(name.length, c.length));
        }
    }
    return "";
}

function initialize() {
    var buttons = document.getElementsByClassName("compareButton");

    for (j = 0; j < buttons.length; j++) {
        if (getCookie(cookieName).length > 0) {
            var cookieCollection = getCookie(cookieName);
            for (var value in cookieCollection) {
                if (buttons[j].href.split("#")[1] == cookieCollection[value]) {
                    buttons[j].innerHTML = "Ta bort";
                    buttons[j].className += " alert";
                }
            }
        }
    }
}