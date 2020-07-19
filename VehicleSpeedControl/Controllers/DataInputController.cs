using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VehicleSpeedControl.Models;

namespace VehicleSpeedControl.Controllers
{
	

    public class DataInputController : Controller
    {
	    HttpClient client = new HttpClient()
	    {
		    BaseAddress = new Uri("http://localhost:55777/")
	    };

	    public DataInputController()
	    {
		    client.DefaultRequestHeaders.Accept.Clear();
		    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	    }

	    /// <summary>
        /// Action for the list of database records.
        /// </summary>
        /// <returns>List of database records.</returns>
	    public async Task<ActionResult> Index()
        {
	        HttpResponseMessage response = await client.GetAsync("api/records/GetAll");
	        var records = await response.Content.ReadAsStringAsync();

			var objResponse =
				JsonConvert.DeserializeObject<List<Record>>(records, 
					new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy H:mm:ss" });

			return View(objResponse);
        }

        /// <summary>
        /// Action for the new database record creation.
        /// </summary>
        /// <returns>Form for new record creation.</returns>
	    public ActionResult CreateRecordRequest()
        {
            return View("CreateRecordView");
        }

        /// <summary>
        /// Action posts new record to the database.
        /// </summary>
        /// <param name="record">New database record.</param>
        /// <returns>Redirects to the list of database records.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateRecordRequest(Record record)
        {
	        HttpResponseMessage response = await client.PostAsJsonAsync(
		        "api/records/CreateRecord", record);

	        response.EnsureSuccessStatusCode();
	        var result = response.Headers.Location;

	        return RedirectToAction("Index");
        }

        /// <summary>
        /// Action opens form for the speed threshold request.
        /// </summary>
        /// <returns>Form for the speed threshold request.</returns>
        public ActionResult SpeedThresholdRequest()
        {
	        return View("SpeedThresholdRequestView");
        }

        /// <summary>
        /// Action sends request for the speed threshold to the service.
        /// </summary>
        /// <param name="speedThreshold">Specific entity which contains speed threshold value.</param>
        /// <returns>A list of database records that match the criteria.</returns>
        [HttpPost]
        public async Task<ActionResult> SpeedThresholdRequest(SpeedThresholdRequest speedThreshold)
        {
	        var threshold = double.Parse(speedThreshold.Speed.ToString("F1"));
	        HttpResponseMessage response = await client.GetAsync(
		        $"api/records/SpeedThreshold?date={speedThreshold.RegistrationDate:yyyy-MM-dd H:mm:ss}&speed={threshold}");
	        var records = await response.Content.ReadAsStringAsync();

	        var objResponse =
		        JsonConvert.DeserializeObject<List<Record>>(records, 
			        new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy H:mm:ss" });

	        return View("SpeedThresholdView",objResponse);
        }

        /// <summary>
        /// Action for the minimal and maximal speed request.
        /// </summary>
        /// <returns>Form for the minimal and maximal request.</returns>
        public ActionResult MinAndMaxByDateRequest()
        {
            return View("MinAndMaxByDateRequestView");
        }

        /// <summary>
        /// Action sends request for minimal and maximal speed for the specified day.
        /// </summary>
        /// <param name="minAndMaxByDateRequest">Specific entity, which contains specified date.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> MinAndMaxByDateRequest([Bind(Include="RegistrationDate")]MinAndMaxByDateRequest minAndMaxByDateRequest)
        {
	        HttpResponseMessage response = await client.GetAsync(
		        $"api/records/GetMinAndMaxByDate?date={minAndMaxByDateRequest.RegistrationDate:yyyy-MM-dd H:mm:ss}");
	        var records = await response.Content.ReadAsStringAsync();

	        var objResponse =
		        JsonConvert.DeserializeObject<List<Record>>(records, 
			        new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy H:mm:ss" });

	        return View("MinAndMaxByDateView",objResponse);
        }
    }
}
