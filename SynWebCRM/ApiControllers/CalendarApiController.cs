using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using SynWebCRM.Data;
using SynWebCRM.Models;

namespace SynWebCRM.ApiControllers
{
    [Authorize(Roles = "Admin")]
    public class CalendarApiController : ApiController
    {
        Model db = new Model();

        private string ServerUrl => "http://" + Request.RequestUri.Authority + "/";

        public ICollection<EventJsonItem> GetEvents([FromUri] GetEventsData data)
        {
            var hostings = GetHostingEvents(data.Start, data.End).Select(x => new EventJsonItem(x) {color = "#257e4a"});
            var dbEvents = GetDbEvents(data.Start, data.End).Select(x => new EventJsonItem(x) { color = "#9BB845" });
            return hostings.Concat(dbEvents).ToList();
        }

        [HttpPost]
        public ResultModel CreateEvent(Event ev)
        {
            ev.CreationDate = DateTime.Now;
            var @event = db.Events.Add(ev);
            db.SaveChanges();
            var res = new EventJsonItem(@event) {url = ServerUrl + "Calendar/EventDetails/" + ev.EventId};
            return new ResultModel(true, res);
        }
        
        public class GetEventsData
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetICS()
        {
            DateTime start = DateTime.Parse($"{DateTime.Now.AddYears(-1).Year}-01-01");
            DateTime end = start.AddYears(2);

            string calTemplate =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "ics-template.txt"));
            string eventTemplate =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "ics-event-template.txt"));
            var hostings = GetHostingEvents(start,end);
            var dbEvents = GetDbEvents(start, end);
            var events = hostings.Concat(dbEvents);

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


            var stream = new MemoryStream();

            byte[] bytes = Encoding.UTF8.GetBytes(calString);
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.GetBuffer())
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "SynwebCRM.ics"
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/calendar");
            return result;
        }

        private ICollection<EventItem> GetHostingEvents(DateTime start, DateTime end)
        {
            var url = "http://"+Request.RequestUri.Authority+"/";
            var hostings = db.Websites.Where(x => x.IsActive
                                                  && x.HostingEndingDate.HasValue
                                                  && x.HostingEndingDate.Value >= start
                                                  && x.HostingEndingDate.Value <= end)
                .ToList().Select(x => new EventItem()
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
            var url = "http://" + Request.RequestUri.Authority + "/";
            var events = db.Events.Where(x => x.StartDate >= start
                                              && x.StartDate <= end
                                              || x.EndDate.HasValue 
                                                && x.EndDate >= start
                                                && x.EndDate <= end)
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
            id = "Event"+ev.EventId;
            title = ev.Name;
            start = ev.StartDate;
            end = ev.EndDate.HasValue?ev.EndDate.Value : ev.StartDate.AddDays(1);
        }

        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string color { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
    }
}