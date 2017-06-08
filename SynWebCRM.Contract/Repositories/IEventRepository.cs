using System;
using System.Collections.Generic;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Contract.Repositories
{
    public interface IEventRepository: IRepository<Event, int>
    {
        ICollection<Event> GetByDates(DateTime start, DateTime end);
    }
}
