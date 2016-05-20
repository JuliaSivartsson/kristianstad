
function UpdateAddressDistances(textboxInput, elementsToShowWhenSuccess) {

    var searchAddress = textboxInput.value;

    // define the function run after found the full address
    var successFoundFunction = function (foundAddress) {
        textboxInput.value = foundAddress;

        // loop all address distance elements
        var addressDistanceElements = document.getElementsByClassName('address-distance');
        for (var i = 0; i < addressDistanceElements.length; i++) {
            var organisationalUnitAddress = addressDistanceElements[i].getAttribute('data-address');
            var element = addressDistanceElements[i];
            CalculateAndSetDistance(organisationalUnitAddress, foundAddress, element, elementsToShowWhenSuccess);
        }
    }

    // now start by searching for the full address
    GetSearchLocation(searchAddress, successFoundFunction);
}

function GetSearchLocation(adress, success) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': adress }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            success(results[0].formatted_address);
        }
    });
}

function CalculateAndSetDistance(address1, address2, element, elementsToShowWhenSuccess) { //success, failure) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address1 }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var latitude1 = results[0].geometry.location.lat();
            var longitude1 = results[0].geometry.location.lng();
            var string = latitude1 + "," + longitude1;

            geocoder.geocode({ 'address': address2 }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var latitude2 = results[0].geometry.location.lat();
                    var longitude2 = results[0].geometry.location.lng();
                    var string2 = string + "," + latitude2 + "," + longitude2;

                    var result = GetDistanceFromLatLonInKm(string2);
                    element.innerHTML = result.toString().replace('.', ',');

                    for (var i = 0; i < elementsToShowWhenSuccess.length; i++) {
                        elementsToShowWhenSuccess[i].style.display = 'inline';
                    }
                    //success(result);
                }
            });
        } else {
            //failure("Request failed.");
        }
    });
};

function GetDistanceFromLatLonInKm(latlon) {
    var res = latlon.split(",");
    var R = 6371; // Radius of the earth in km
    var dLat = deg2rad(res[2] - res[0]);  // deg2rad below
    var dLon = deg2rad(res[3] - res[1]);
    var a =
      Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(deg2rad(res[0])) * Math.cos(deg2rad(res[2])) *
      Math.sin(dLon / 2) * Math.sin(dLon / 2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c; // Distance in km
    var distans = Math.round(d * 100) / 100;
    return distans;
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}