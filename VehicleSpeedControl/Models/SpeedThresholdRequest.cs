using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleSpeedControl.Models
{
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