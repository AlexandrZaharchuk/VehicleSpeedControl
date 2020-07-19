using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleSpeedControl.Models
{
	/// <summary>
	/// Entity represents request of the minimal and maximal speed on the specified date.
	/// </summary>
	public class MinAndMaxByDateRequest
	{
		[Required(ErrorMessage = "Поле обязательно для заполнения!")]
		[DataType(DataType.DateTime)]
		[DisplayName("Дата и время регистрации")]
		public DateTime RegistrationDate { get; set; }
	}
}