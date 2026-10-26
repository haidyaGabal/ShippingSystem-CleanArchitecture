$(document).ready(function () {
    const shipmentId = AppHelper.getQueryParam("id") || AppHelper.getIdFromPath();
    if (shipmentId) {
        LoadShipmentById(shipmentId);
    }
});


function LoadShipmentById(id) {
    ShipmentService.GetById(
        id,
        function (response) {
           
            console.log("response:", response);
            const data = response.Data || response.data || response;
            ShipmentService.FillShipmentForm(data);
        },
        function (error) {
            console.error("API Error", error.status, error.responseText);
            alert("Failed to load shipment data (" + error.status + ")");
        }
    );
}

//function LoadShipmentById(id) {
//    ShipmentService.GetById(
//        id,
//        function (response) {

//            console.log("data return success")
//            ShipmentService.FillShipmentForm(response.Data);

//        },
//        function (error) {
//            console.error("API Error", error);
//            alert("فشل في تحميل بيانات الشحنة");
//        }
//    );
//}