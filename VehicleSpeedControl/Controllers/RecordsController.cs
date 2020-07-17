using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Http;
using LiteDB;
using VehicleSpeedControl.Models;

namespace VehicleSpeedControl.Controllers
{
    public class RecordsController : ApiController
    {
	    private static readonly string ApplicationBaseDir = $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\";
	    private static readonly string DbPath  = ApplicationBaseDir + ConfigurationManager.AppSettings["dbName"];
	    private static readonly LiteRepository LiteDb = new LiteRepository(DbPath);

	    public RecordsController()
	    {
			if (!LiteDb.Database.CollectionExists("Record"))
				Seed(LiteDb);
		}
	    
	    [HttpGet, ActionName("GetMinAndMaxByDate")]
		public IEnumerable<Record> GetMinAndMaxByDate(DateTime date)
		{
			var recordsByDate = LiteDb.Query<Record>()
				.Where(x => x.RegistrationTime.Date == date.Date)
				.OrderBy(y => y.VehicleSpeed)
				.ToList();
			return new List<Record> {recordsByDate.First(), recordsByDate.Last()};
		}

        [HttpPost, ActionName("CreateRecord")]
        public void CreateRecord([FromBody]Record record)
        {
	        LiteDb.Insert(record);
        }

        [HttpGet, ActionName("SpeedThreshold")]
        public IEnumerable<Record> SpeedThreshold([FromBody]SpeedThreshold speedThreshold)
        {
	        return LiteDb.Query<Record>()
		        .Where(x => x.RegistrationTime.Date == speedThreshold.RegistrationDate.Date && 
		                    x.VehicleSpeed > speedThreshold.Speed)
		        .ToEnumerable();
        }

        [HttpGet, ActionName("GetAll")]
        public IEnumerable<Record> GetAll()
        {
	        if(IsServiceTime())
		        return LiteDb.Query<Record>().ToEnumerable();
	        return BadRequest("В данное время запросы не могут быть обработаны!");

        }

        private void Seed(LiteRepository db)
        {
	        var records = new List<Record>()
	        {
		        new Record(){RegistrationTime = new DateTime(2019, 12, 20, 14, 11, 25), VehicleLicensePlate = "2763 AB-7", VehicleSpeed = 75.8},
		        new Record(){RegistrationTime = new DateTime(2019, 12, 20, 9, 36, 50), VehicleLicensePlate = "1768 BI-5", VehicleSpeed = 80.4},
		        new Record(){RegistrationTime = new DateTime(2019, 12, 21, 18, 25, 36), VehicleLicensePlate = "8163 IO-6", VehicleSpeed = 60.7},
		        new Record(){RegistrationTime = new DateTime(2019, 12, 22, 23, 47, 51), VehicleLicensePlate = "1770 MP-4", VehicleSpeed = 65.4},
		        new Record(){RegistrationTime = new DateTime(2019, 12, 22, 17, 15, 49), VehicleLicensePlate = "4782 KO-3", VehicleSpeed = 90.4},

		        //new Record(){RegistrationTime = "20.12.2019 20:45:15", VehicleLicensePlate = "9372 OI-5", VehicleSpeed = 65.8},
		        //new Record(){RegistrationTime = "21.12.2019 12:46:11", VehicleLicensePlate = "8372 OE-2", VehicleSpeed = 82.8},
		        //new Record(){RegistrationTime = "22.12.2019 9:23:18", VehicleLicensePlate = "8261 MP-4", VehicleSpeed = 90.3},
		        //new Record(){RegistrationTime = "22.12.2019 14:31:25", VehicleLicensePlate = "8293 MI-1", VehicleSpeed = 51.7}
	        };

	        db.Insert<Record>(records);
        }

        private bool IsServiceTime()
        {
			var startServiceSettings = ConfigurationManager.AppSettings["StartRequestService"];
			var finishServiceSettings = ConfigurationManager.AppSettings["FinishRequestService"];

			var startServiceTime = DateTime.ParseExact(startServiceSettings, "H:mm", CultureInfo.InvariantCulture);
			var finishServiceTime = DateTime.ParseExact(finishServiceSettings, "H:mm", CultureInfo.InvariantCulture);

			var currentTime = DateTime.Now;

			var a = DateTime.Compare(startServiceTime, currentTime);
			var b = DateTime.Compare(currentTime, finishServiceTime);

			if ( a < 0 && b < 0) return true;
			return false;
        }
    }
}
