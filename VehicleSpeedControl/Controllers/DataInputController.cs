using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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

	    // GET: DataInput
	    public async Task<ActionResult> Index()
        {
	        HttpResponseMessage response = await client.GetAsync("api/records/GetAll");
	        var records = await response.Content.ReadAsStringAsync();

			var objResponse =
				JsonConvert.DeserializeObject<List<Record>>(records, 
					new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy H:mm:ss" });

			return View(objResponse);
        }

        public ActionResult CreateRecordRequest()
        {
            return View("CreateRecordView");
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecordRequest(Record record)
        {
	        HttpResponseMessage response = await client.PostAsJsonAsync(
		        "api/records/CreateRecord", record);

	        response.EnsureSuccessStatusCode();
	        var result = response.Headers.Location;

	        return RedirectToAction("Index");
        }

        public ActionResult SpeedThresholdRequest()
        {
	        return View("SpeedThresholdRequestView");
        }

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

        public ActionResult MinAndMaxByDateRequest()
        {
            return View("MinAndMaxByDateRequestView");
        }

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

        // POST: DataInput/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DataInput/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DataInput/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
