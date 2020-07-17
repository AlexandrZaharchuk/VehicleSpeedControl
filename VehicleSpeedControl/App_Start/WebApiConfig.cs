﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VehicleSpeedControl
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
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

			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
		}
	}
}