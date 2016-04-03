using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using SynWebCRM.Helpers.Formatters;

namespace SynWebCRM
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            var index = config.Formatters.IndexOf(config.Formatters.JsonFormatter);
            var ccf = new JsonCamelCaseFormatter();
            config.Formatters[index] = ccf;


            WebApiConfigHelper.ApiRoute(config.Routes, "deals/get", "DealsApi", "GetDeals");

            WebApiConfigHelper.ApiRoute(config.Routes, "calendar/events/get", "CalendarApi", "GetEvents");
            WebApiConfigHelper.ApiRoute(config.Routes, "calendar/event/create", "CalendarApi", "CreateEvent");
            WebApiConfigHelper.ApiRoute(config.Routes, "calendar/ics/get", "CalendarApi", "GetICS");

            WebApiConfigHelper.ApiRoute(config.Routes, "customers/notes/add", "CustomersApi", "AddNote");
        }
    }
}
