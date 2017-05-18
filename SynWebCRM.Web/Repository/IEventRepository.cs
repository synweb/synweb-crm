using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SynWebCRM.Web.Data;

namespace SynWebCRM.Web.Repository
{
    public interface IEventRepository: IRepository<Event, int>
    {
    }
}
