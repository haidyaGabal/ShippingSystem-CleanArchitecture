const ShipmentService = {

    FormIds: {},

    GetModel: function () {

        const shipmentDto = {

            // ✅ DTO: ShipmentDTO
            ShippingDate: new Date().toISOString(),
            DeliveryDate: new Date(Date.now() + 3 * 86400000).toISOString(),

            SenderId: "00000000-0000-0000-0000-000000000000",
            ReceiverId: "00000000-0000-0000-0000-000000000000",

            // ✅ MUST be lowercase (match DTO exactly)
            userSender: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                SenderName: $('input[name="SenderName"]').val(),
                Email: $('input[name="Email"]').val(),
                Phone: $('input[name="Phone"]').val(),
                CityId: $('select[name="SenderCityId"]').val() || null,
                Address: $('input[name="Address"]').val(),
                Contact: $('input[name="Contact"]').val(),
                PostalCode: $('input[name="PostalCode"]').val(),
                OtherAddress: $('input[name="OtherAddress"]').val()
            },

            userReceiver: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                ReceiverName: $('input[name="ReceiverName"]').val(),
                Email: $('input[name="ReceiverEmail"]').val(),
                Phone: $('input[name="ReceiverPhone"]').val(),
                CityId: $('select[name="ReceiverCity"]').val(),
                Address: $('input[name="ReceiverAddress"]').val(),
                Contact: $('input[name="ReceiverContact"]').val(),
                PostalCode: $('input[name="ReceiverPostalCode"]').val(),
                OtherAddress: $('input[name="ReceiverOtherAddress"]').val()
            },
         

            
            // ✅ EXACT DTO NAMES
            ShipingTypeId: $('select[name="ShippingType"]').val() || null,
            ShipingPackagesId: $('select[name="PackageType"]').val() || null,

            Width: this.parseNumber($('input[name="Width"]').val()),
            Height: this.parseNumber($('input[name="Height"]').val()),
            Weight: this.parseNumber($('input[name="Weight"]').val()),
            Length: this.parseNumber($('input[name="Length"]').val()),

            PackageValue: parseFloat($('input[name="PackageValue"]').val()) || 0,
            ShipingRate: 0,

            PaymentMethodId: null,
            UserSubscriptionId: null,
            TrackingNumber: null,
            ReferenceId: null
        };

        console.log("Shipment DTO:", shipmentDto);
        return shipmentDto;
    },

    parseNumber: function (value) {
        const num = parseFloat(value);
        return isNaN(num) ? null : num;
    },

    FillShipmentForm: function (data) {

        this.FormIds = {
            Id: data.Id,
            SenderId: data.SenderId,
            ReceiverId: data.ReceiverId,
            TrackingNumber: data.TrackingNumber,
            ShipingRate: data.ShipingRate
        };

        // ================= SENDER =================
        $('input[name="SenderName"]').val(data.userSender?.SenderName || "");
        $('input[name="Email"]').val(data.userSender?.Email || "");
        $('input[name="Phone"]').val(data.userSender?.Phone || "");

        $('select[name="Sendercountry"]').val(data.userSender?.CountryId || "");
        ManagePageControls.fillCityDropdown(
            'select[name="SenderCityId"]',
            data.userSender?.CountryId,
            data.userSender?.CityId
        );

        $('input[name="Address"]').val(data.userSender?.Address || "");
        $('input[name="Contact"]').val(data.userSender?.Contact || "");
        $('input[name="PostalCode"]').val(data.userSender?.PostalCode || "");
        $('input[name="OtherAddress"]').val(data.userSender?.OtherAddress || "");

        // ================= RECEIVER =================
        $('input[name="ReceiverName"]').val(data.userReceiver?.ReceiverName || "");
        $('input[name="ReceiverEmail"]').val(data.userReceiver?.Email || "");
        $('input[name="ReceiverPhone"]').val(data.userReceiver?.Phone || "");

        $('select[name="ReceiverCountry"]').val(data.userReceiver?.CountryId || "");
        ManagePageControls.fillCityDropdown(
            'select[name="ReceiverCity"]',
            data.userReceiver?.CountryId,
            data.userReceiver?.CityId
        );

        $('input[name="ReceiverAddress"]').val(data.userReceiver?.Address || "");
        $('input[name="ReceiverContact"]').val(data.userReceiver?.Contact || "");
        $('input[name="ReceiverPostalCode"]').val(data.userReceiver?.PostalCode || "");
        $('input[name="ReceiverOtherAddress"]').val(data.userReceiver?.OtherAddress || "");

        // ================= SHIPMENT =================
        $('select[name="ShippingType"]').val(data.ShipingTypeId || "");
        $('select[name="PackageType"]').val(data.ShipingPackagesId || "");


        $('input[name="Width"]').val(data.Width);
        $('input[name="Height"]').val(data.Height);
        $('input[name="Weight"]').val(data.Weight);
        $('input[name="Length"]').val(data.Length);
        $('input[name="PackageValue"]').val(data.PackageValue);

        $('input[name="TrackingNumber"]').val(data.TrackingNumber ?? "");

        // Dates
        $('input[name="ShippingDate"]').val(
            data.ShippingDate ? new Date(data.ShippingDate).toISOString().split("T")[0] : ""
        );

        $('input[name="DeliveryDate"]').val(
            data.DeliveryDate ? new Date(data.DeliveryDate).toISOString().split("T")[0] : ""
        );
    },

    SaveShippment: function () {

        let data = this.GetModel();

        console.log("Before إرسال:", data);

        ApiClient.post("/api/Shippment/Create", data,
            function (res) {
                console.log("Created successfully", res);
            },
            function (xhr) {
                console.error("API Error:", xhr.responseJSON);
            });
    },


    EditShippment: function () {

        let data = this.GetModel();

        data.Id = this.FormIds.Id;
        data.SenderId = this.FormIds.SenderId;
        data.ReceiverId = this.FormIds.ReceiverId;
        data.TrackingNumber = this.FormIds.TrackingNumber;
        data.ShipingRate = this.FormIds.ShipingRate;

        console.log("Before Edit:", data);

        ApiClient.post("/api/Shippment/Edit", data,
            function (res) {
                console.log("Updated successfully", res);
            },
            function (xhr) {
                console.error("API Error:", xhr.responseJSON);
            });
    },


    GetShipments: function (onSuccess, onError) {
        ApiClient.get(`/api/Shippment/shipments`, onSuccess, onError, true);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/Shippment/${id}`, onSuccess, onError, true);
    }
};