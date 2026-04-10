using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Ui.Models;

namespace Ui.Controllers
{

    

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShipingType _shippingType;
        private readonly IShipment _shipment1;


        public HomeController(ILogger<HomeController> logger, IShipingType shippingType, IShipment shipment1)
        {
            _logger = logger;
            _shippingType = shippingType;
            _shipment1= shipment1;
        }

        public IActionResult Index()
        {
            //var type = _shippingType.GetAll();
            shipment();
            return View();
        }

            public async Task shipment()
        {
            Guid cityId = Guid.Parse("0522c52c-b1f2-4445-989c-a20882ed8af6");
            Guid shipmentTypeId = Guid.Parse("79e3cdab-70df-408b-ba77-effaa45c6334");

            Guid shipingPackageId = Guid.Parse("6ecf05c2-98f9-4512-9903-57eeaea80e13");

            var shipment = new ShipmentDTO
            {
                // Basic Info - NOTE: "ShipingDate" not "ShippingDate"
                ShippingDate = DateTime.Now,  // ? Fixed: single 'p'
                DeliveryDate = DateTime.Now.AddDays(3),

                // IDs
                SenderId = Guid.Empty,
                ReceiverId = Guid.Empty,
                ShipingTypeId = shipmentTypeId,
                ShipingPackagesId = shipingPackageId,  // ? Using valid package ID

                // Dimensions
                Width = 30.5,
                Height = 20.0,
                Weight = 5.5,
                Length = 40.0,

                // Financial
                PackageValue = 150.00m,
                ShipingRate = 45.00m,

                // Other
                PaymentMethodId = null,
                UserSubscriptionId = null,
                TrackingNumber = 1234567890,
                ReferenceId = Guid.NewGuid(),

                // Sender Object
                userSender = new UserSenderDTO
                {
                    UserId = Guid.NewGuid(),
                    SenderName = "Ahmed Mohammed Al-Otaibi",
                    Email = "ahmed@example.com",
                    Phone = "+966501234567",
                    CityId = cityId,
                    Address = "King Fahd Road, Al Olaya District",
                    PostalCode = "12345",
                    OtherAddress = "Building 123, Floor 4",
                    Contact = "Ahmed Mohammed",
                    IsDefault = true
                },

                // Receiver Object
                userReceiver = new UserReceiverDTO
                {
                    UserId = Guid.NewGuid(),
                    ReceiverName = "Mohammed Saeed Al-Ghamdi",
                    Email = "mohammed@example.com",
                    Phone = "+966549876543",
                    CityId = cityId,
                    Address = "Al-Madinah Road, Al-Balad District",
                    PostalCode = "54321",
                    OtherAddress = "Commercial Center, Shop 45",
                    Contact = "Mohammed Saeed"
                }
            };

            await _shipment1.Create(shipment);  // Add 'await' if method is async
        }
        

        //public iactionresult privacy()
        //{
        //    return view();
        //}

        //[responsecache(duration = 0, location = responsecachelocation.none, nostore = true)]
        //public iactionresult error()
        //{
        //    return view(new errorviewmodel { requestid = activity.current?.id ?? httpcontext.traceidentifier });
        //}
    }
}
