using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleSpeedControl.Models
{
	public class Record
	{
		public int Id { get; set; }

		[DisplayName("Дата и время регистрации")]
		[DataType(DataType.DateTime)]
		public DateTime RegistrationTime { get; set; }

		[DisplayName("Номерной знак")]
		public string VehicleLicensePlate { get; set; }

		[DisplayName("Скорость")]
		public double VehicleSpeed { get; set; }
	}
}