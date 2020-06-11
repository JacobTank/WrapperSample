using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Platform.Clients.PowerService;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private IPowerService _api;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            
            // Initialize the PowerServiceClientFactory using configuration data saved in application
            _api = PowerServiceClientFactory.CreateAsync(new Uri(_configuration["mp.ApiBaseUrl"]), _configuration["mp.client"], _configuration["mp.secret"]).Result;            
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetRecords()
        {
            //var count = await _api.GetRecordsAsync(
            //    table: "Addresses",
            //    filter: "Postal_Code IS NOT NULL",
            //    select: "COUNT(Addresses.Postal_Code) AS RecordCount, Addresses.Postal_Code AS Zip",
            //    groupBy: "Postal_Code",
            //    having: "COUNT(Addresses.Postal_Code) > 1000"
            //    );

            //var contactData = await _api.GetRecordAsync(
            //    table: "Contacts",
            //    id: 1,
            //    select: "Contacts.*, Household_ID_Table.*, Household_ID_Table_Address_ID_Table.*");

            // Get Top 5 Addresses and return as model
            return View(
                await _api.GetRecordsAsync<AddressModel>(
                table: "Addresses",
                top: 5,
                skip: 10,
                filter: "Postal_Code IS NOT NULL AND [State/Region] IS NOT NULL",
                select: "Addresses.*",
                orderBy: "Postal_Code"
                )
            );
        }

        public async Task<IActionResult> CreateRecords()
        {
            AddressModel model = new AddressModel()
            {
                Address_Line_1 = "Test Address Line 1",
                City = "Test City",
                State_Region = "AK"
            };

            List<AddressModel> records = new List<AddressModel>();
            records.Add(model);

            return View(await _api.CreateRecordsAsync<AddressModel>("Addresses", records));
        }

        public async Task<IActionResult> UpdateRecords()
        {
            var model = await _api.GetRecordAsync<AddressModel>("Addresses", 935752);

            model.Address_Line_1 = "Updated Address Line 1";

            List<AddressModel> recordUpdates = new List<AddressModel>();
            recordUpdates.Add(model);

            var addressUpdates = await _api.UpdateRecordsAsync<AddressModel>("Addresses", recordUpdates);

            return View(addressUpdates);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
