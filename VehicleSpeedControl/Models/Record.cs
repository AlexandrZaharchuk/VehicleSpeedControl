using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace VehicleSpeedControl.Models
{
	public class Record
	{
		public int Id { get; set; }

		//private DateTime _registrationTime;
		//public DateTime RegistrationTime
		//{
		//	get => _registrationTime.ToLocalTime();
		//	set => _registrationTime = Convert.ToDateTime(value).ToLocalTime();
		//}

		[DisplayFormat(DataFormatString = "dd.MM.yyyy")]
		public DateTime RegistrationTime { get; set; }

		public string VehicleLicensePlate { get; set; }
		public double VehicleSpeed { get; set; }

		//public DateTime GetRegistrationTimeString()
		//{
		//	return DateTime.ParseExact(_registrationTime.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
		//}
	}
}

//public string RegistrationTime
//{
//get => _registrationTime.ToString();
//set => _registrationTime = DateTime.ParseExact(value, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
//}