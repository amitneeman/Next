function Init() {
var mapProp= {
  center: new google.maps.LatLng(51.508742,-0.120850),
  zoom:5,
};
    var map = new google.maps.Map(document.getElementById("map"), mapProp);

    var geocoder = new google.maps.Geocoder();

function getCountry(country) {
    geocoder.geocode( { 'address': country }, function(results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
           map.setCenter(results[0].geometry.location);
           var marker = new google.maps.Marker({
               map: map,
               position: results[0].geometry.location
           });
        } else {
            console.log("Invalid Country")
        }
    });
}

    getCountry($("#serverCountry").text().trim());
}



