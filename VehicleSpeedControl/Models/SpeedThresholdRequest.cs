using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleSpeedControl.Models
{
	/// <summary>
	/// Represents request for the speed threshold on the specified date.
	/// </summary>
	public class SpeedThresholdRequest
	{
		[Required(ErrorMessage = "Поле обязательно для заполнения!")]
		[DataType(DataType.DateTime)]
		[DisplayName("Дата и время регистрации")]
		public DateTime RegistrationDate { get; set; }

		[Required(ErrorMessage = "Поле обязательно для заполнения!")]
		[DisplayName("Скорость")]
		public double Speed { get; set; }
	}
}