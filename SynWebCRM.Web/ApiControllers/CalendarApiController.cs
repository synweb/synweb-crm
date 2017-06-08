using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Web.Models;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;
using Microsoft.AspNetCore.Http.Extensions;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Web.ApiControllers
{
    [Authorize(Roles = "Admin")]
    public class CalendarApiController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IWebsiteRepository _websiteRepository;

        public CalendarApiController(IEventRepository eventRepository, IWebsiteRepository websiteRepository)
        {
            _eventRepository = eventRepository;
            _websiteRepository = websiteRepository;
        }


        private string ServerUrl => "http://" + Request.GetUri().Authority + "/";

        [HttpGet]
        [Route("/api/calendar/events/get")]
        public ICollection<EventJsonItem> GetEvents(GetEventsData data)
        {
            var hostings = GetHostingEvents(data.Start, data.End).Select(x => new EventJsonItem(x) { color = "#257e4a" });
            var domains = GetDomainEvents(data.Start, data.End).Select(x => new EventJsonItem(x) { color = "#6546c5" });
            var dbEvents = GetDbEvents(data.Start, data.End).Select(x => new EventJsonItem(x) { color = "#9BB845" });
            return hostings.Concat(dbEvents).Concat(domains).ToList();
        }

        [HttpPost]
        [Route("/api/calendar/event/create")]
        public ResultModel CreateEvent(Event ev)
        {
            ev.CreationDate = DateTime.Now;
            _eventRepository.Add(ev);
            var res = new EventJsonItem(ev) { url = ServerUrl + "Calendar/EventDetails/" + ev.EventId };
            return new ResultModel(true, res);
        }

        public class GetEventsData
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/calendar/ics/get")]
        public ActionResult GetICS()
        {
            DateTime start = DateTime.Parse($"{DateTime.Now.AddYears(-1).Year}-01-01");
            DateTime end = start.AddYears(2);
            string calTemplate =
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ics-template.txt"));
            string eventTemplate =
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ics-event-template.txt"));
            var hostings = GetHostingEvents(start, end);
            var domains = GetDomainEvents(start, end);
            var dbEvents = GetDbEvents(start, end);
            var events = hostings.Concat(dbEvents).Concat(domains);

            var eventsStrings = events.Select(x => eventTemplate
                .Replace("%summary%", x.Title)
                .Replace("%description%", x.Url)
                .Replace("%startDate%", x.Start.ToString("yyyyMMdd"))
                .Replace("%endDate%", x.End?.ToString("yyyyMMdd") ?? x.Start.AddDays(1).ToString("yyyyMMdd"))
                ).ToArray();
            for (int i = 0; i < eventsStrings.Length; i++)
            {
                eventsStrings[i] = eventsStrings[i].Replace("%uid%", (i + 1).ToString());
            }
            var calString = calTemplate
                .Replace("%calendarName%", "SynWeb CRM Calendar")
                .Replace("%events%", string.Join(Environment.NewLine, eventsStrings)
                );
            byte[] bytes = Encoding.UTF8.GetBytes(calString);
            var res = new FileContentResult(bytes, "text/calendar") {FileDownloadName = "SynwebCRM.ics"};
            return res;
        }

        private ICollection<EventItem> GetDomainEvents(DateTime start, DateTime end)
        {
            var url = "http://" + Request.GetUri().Authority + "/";
            var websites = _websiteRepository.GetEndingByDomain(start, end);
            
            var hostings = websites.Select(x => new EventItem()
                {
                    Id = "Website" + x.WebsiteId,
                    Title = "Домен " + x.Domain,
                    Start = x.DomainEndingDate.Value.Date,
                    Url = url + "Websites/Details/" + x.WebsiteId
                }).ToList();


            return hostings;
        }

        private ICollection<EventItem> GetHostingEvents(DateTime start, DateTime end)
        {
            var url = "http://" + Request.GetUri().Authority + "/";
            var websites = _websiteRepository.GetEndingByHosting(start, end);
            var hostings = websites.Select(x => new EventItem()
                {
                    Id = "Website" + x.WebsiteId,
                    Title = "Хостинг " + x.Domain,
                    Start = x.HostingEndingDate.Value.Date,
                    Url = url + "Websites/Details/" + x.WebsiteId
                }).ToList();


            return hostings;
        }

        private ICollection<EventItem> GetDbEvents(DateTime start, DateTime end)
        {
            var url = "http://" + Request.GetUri().Authority + "/";
            var events = _eventRepository.GetByDates(start, end)
                .Select(x => new EventItem()
                {
                    Id = "Event" + x.EventId,
                    Title = x.Name,
                    Start = x.StartDate,
                    Url = url + "Calendar/EventDetails/" + x.EventId,
                    End = x.EndDate
                }).ToList();
            return events;
        }
    }

    public class EventItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }

    public class EventJsonItem
    {
        public EventJsonItem(EventItem item)
        {
            id = item.Id;
            title = item.Title;
            url = item.Url;
            start = item.Start;
            end = item.End;
        }

        public EventJsonItem(Event ev)
        {
            id = "Event" + ev.EventId;
            title = ev.Name;
            start = ev.StartDate;
            end = ev.EndDate.HasValue ? ev.EndDate.Value : ev.StartDate.AddDays(1);
        }

        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string color { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
    }
}