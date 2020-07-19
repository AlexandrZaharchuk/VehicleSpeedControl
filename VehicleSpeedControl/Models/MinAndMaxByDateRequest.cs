using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleSpeedControl.Models
{
	public class MinAndMaxByDateRequest
	{
		[Required(ErrorMessage = "Поле обязательно для заполнения!")]
		[DataType(DataType.DateTime)]
		[DisplayName("Дата и время регистрации")]
		public DateTime RegistrationDate { get; set; }
	}
}