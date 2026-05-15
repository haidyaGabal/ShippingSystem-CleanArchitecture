$(document).ready(function () {

    // Sender country
    ManagePageControls.fillCountryDropdown('select[name="Sendercountry"]');

    //ReceiverCountry 
    ManagePageControls.fillCountryDropdown('select[name="ReceiverCountry"]');

    // Shipping types
    ManagePageControls.fillShippingTypesDropdown('select[name="ShippingType"]');

    // Package types
    ManagePageControls.fillShippingPackgingDropdown('select[name="PackageType"]');

    // Sender cities
    $('select[name="Sendercountry"]').on('change', function () {
        const countryId = $(this).val();
        ManagePageControls.fillCityDropdown('select[name="SenderCityId"]', countryId, null);
    });


    $('select[name="ReceiverCountry"]').on('change', function () {
        const countryId = $(this).val();
        ManagePageControls.fillCityDropdown('select[name="ReceiverCity"]', countryId, null);
    });

});