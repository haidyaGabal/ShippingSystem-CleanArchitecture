const ShipmentService = {

    FormIds: {},

    EMPTY_GUID: "00000000-0000-0000-0000-000000000000",

    GetModel: function () {

        const shipDate = $('input[name="ShippingDate"]').val();
        const delDate = $('input[name="DeliveryDate"]').val();

        const shipmentDto = {

            ShippingDate: shipDate
                ? new Date(shipDate).toISOString()
                : new Date().toISOString(),
            DeliveryDate: delDate
                ? new Date(delDate).toISOString()
                : new Date(Date.now() + 3 * 86400000).toISOString(),

            SenderId: this.EMPTY_GUID,
            ReceiverId: this.EMPTY_GUID,

            // property names match ShipmentDTO exactly
            Sender: {
                Id: this.EMPTY_GUID,
                UserId: this.EMPTY_GUID,
                SenderName: $('input[name="SenderName"]').val(),
                Email: $('input[name="Email"]').val(),
                Phone: $('input[name="Phone"]').val(),
                CityId: $('select[name="SenderCityId"]').val() || null,
                Address: $('input[name="Address"]').val(),
                Contact: $('input[name="Contact"]').val(),
                PostalCode: $('input[name="PostalCode"]').val(),
                OtherAddress: $('input[name="OtherAddress"]').val()
            },

            Receiver: {
                Id: this.EMPTY_GUID,
                UserId: this.EMPTY_GUID,
                ReceiverName: $('input[name="ReceiverName"]').val(),
                Email: $('input[name="ReceiverEmail"]').val(),
                Phone: $('input[name="ReceiverPhone"]').val(),
                CityId: $('select[name="ReceiverCity"]').val() || null,
                Address: $('input[name="ReceiverAddress"]').val(),
                Contact: $('input[name="ReceiverContact"]').val(),
                PostalCode: $('input[name="ReceiverPostalCode"]').val(),
                OtherAddress: $('input[name="ReceiverOtherAddress"]').val()
            },

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

    // waits until the option exists (dropdowns load from the API), then selects it
    setSelectWhenReady: function (selector, value, tries = 20) {
        if (!value) return;
        const $el = $(selector);
        if ($el.find('option[value="' + value + '"]').length) {
            $el.val(value);
        } else if (tries > 0) {
            setTimeout(() => this.setSelectWhenReady(selector, value, tries - 1), 250);
        }
    },

    FillShipmentForm: function (data) {

        if (!data) return;

        const s = data.Sender || {};
        const r = data.Receiver || {};
        const toDate = v => (!v || String(v).startsWith("0001")) ? "" : String(v).split("T")[0];

        this.FormIds = {
            Id: data.Id,
            SenderId: data.SenderId,
            ReceiverId: data.ReceiverId,
            TrackingNumber: data.TrackingNumber,
            ShipingRate: data.ShipingRate
        };

        // Sender
        $('input[name="SenderName"]').val(s.SenderName || "");
        $('input[name="Email"]').val(s.Email || "");
        $('input[name="Phone"]').val(s.Phone || "");
        $('input[name="Address"]').val(s.Address || "");
        $('input[name="Contact"]').val(s.Contact || "");
        $('input[name="PostalCode"]').val(s.PostalCode || "");
        $('input[name="OtherAddress"]').val(s.OtherAddress || "");

        // Receiver
        $('input[name="ReceiverName"]').val(r.ReceiverName || "");
        $('input[name="ReceiverEmail"]').val(r.Email || "");
        $('input[name="ReceiverPhone"]').val(r.Phone || "");
        $('input[name="ReceiverAddress"]').val(r.Address || "");
        $('input[name="ReceiverContact"]').val(r.Contact || "");
        $('input[name="ReceiverPostalCode"]').val(r.PostalCode || "");
        $('input[name="ReceiverOtherAddress"]').val(r.OtherAddress || "");

        // Country + City
        this.setSelectWhenReady('select[name="Sendercountry"]', s.CountryId);
        ManagePageControls.fillCityDropdown('select[name="SenderCityId"]', s.CountryId, s.CityId);

        this.setSelectWhenReady('select[name="ReceiverCountry"]', r.CountryId);
        ManagePageControls.fillCityDropdown('select[name="ReceiverCity"]', r.CountryId, r.CityId);

        // Shipment
        this.setSelectWhenReady('select[name="ShippingType"]', data.ShipingTypeId);
        this.setSelectWhenReady('select[name="PackageType"]', data.ShipingPackagesId);

        $('input[name="Width"]').val(data.Width);
        $('input[name="Height"]').val(data.Height);
        $('input[name="Weight"]').val(data.Weight);
        $('input[name="Length"]').val(data.Length);
        $('input[name="PackageValue"]').val(data.PackageValue);
        $('input[name="TrackingNumber"]').val(data.TrackingNumber ?? "");
        $('input[name="ShippingDate"]').val(toDate(data.ShippingDate));
        $('input[name="DeliveryDate"]').val(toDate(data.DeliveryDate));
    },

    SaveShippment: function () {

        const data = this.GetModel();

        ApiClient.post("/api/Shippment/Create", data,
            function (res) {
                console.log("Created successfully", res);
            },
            function (xhr) {
                console.error("API Error:", xhr.status, xhr.responseText);
            });
    },

    EditShippment: function () {

        const data = this.GetModel();

        data.Id = this.FormIds.Id;
        data.SenderId = this.FormIds.SenderId;
        data.ReceiverId = this.FormIds.ReceiverId;
        data.Sender.Id = this.FormIds.SenderId;
        data.Receiver.Id = this.FormIds.ReceiverId;
        data.TrackingNumber = this.FormIds.TrackingNumber;
        data.ShipingRate = this.FormIds.ShipingRate;

        console.log("Before Edit:", data);

        ApiClient.post("/api/Shippment/Edit", data,
            function (res) {
                console.log("Updated successfully", res);
            },
            function (xhr) {
                console.error("API Error:", xhr.status, xhr.responseText);
            });
    },

    GetShipments: function (onSuccess, onError) {
        ApiClient.get(`/api/Shippment/shipments`, onSuccess, onError, true);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/Shippment/${id}`, onSuccess, onError, true);
    }
};