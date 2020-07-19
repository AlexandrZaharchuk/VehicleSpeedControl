using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace VehicleSpeedControl.Attributes
{
	/// <summary>
	/// Custom attribute for validation of allowed time of service.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class RequestTimeValidation : AuthorizeAttribute
	{
		private readonly string _startServiceSettings = ConfigurationManager.AppSettings["StartRequestService"];
		private readonly string _finishServiceSettings = ConfigurationManager.AppSettings["FinishRequestService"];

		protected override bool IsAuthorized(HttpActionContext actionContext)
		{
			base.IsAuthorized(actionContext);
			if (IsServiceTime()) return true;
			return false;
		}

		protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
		{
			base.HandleUnauthorizedRequest(actionContext);
			actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, 
				$"Доступ запрещен! Выполнение запросов к системе возможно только в промежуток времени с {_startServiceSettings} до {_finishServiceSettings}");
		}

		private bool IsServiceTime()
		{
			var startServiceTime = DateTime.ParseExact(_startServiceSettings, "H:mm", CultureInfo.InvariantCulture);
			var finishServiceTime = DateTime.ParseExact(_finishServiceSettings, "H:mm", CultureInfo.InvariantCulture);

			var currentTime = DateTime.Now;

			var a = DateTime.Compare(startServiceTime, currentTime);
			var b = DateTime.Compare(currentTime, finishServiceTime);

			if ( a < 0 && b < 0) return true;
			return false;
		}
	}
}