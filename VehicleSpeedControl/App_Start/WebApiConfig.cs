using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VehicleSpeedControl
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DateRequestApi",
				routeTemplate: "api/{controller}/{action}/{date}"
			);

			config.Routes.MapHttpRoute(
				name: "GetAllApi",
				routeTemplate: "api/{controller}/{action}"
			);

			config.Routes.MapHttpRoute(
				name: "CreateRecordApi",
				routeTemplate: "api/{controller}/{action}/{record}"
			);

			config.Routes.MapHttpRoute(
				name: "SpeedRequestApi",
				routeTemplate: "api/{controller}/{action}/{speedThreshold}"
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter()
			{
				DateTimeFormat = "dd.MM.yyyy H:mm:ss",
				DateTimeStyles = DateTimeStyles.AssumeLocal,
				Culture = CultureInfo.CurrentCulture
			});

			//config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new FormattedDecimalConverter(CultureInfo.CurrentCulture));

			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
		}
	}
}
