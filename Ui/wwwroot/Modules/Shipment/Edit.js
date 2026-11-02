$('form.steps').on('submit', function (e) {
    e.preventDefault();

    if (!ShipmentService.FormIds.Id) {
        alert("Shipment data is still loading, please wait.");
        return;
    }

    ShipmentService.EditShippment();
});