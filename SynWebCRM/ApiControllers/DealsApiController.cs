using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using SynWebCRM.Data;

namespace SynWebCRM.ApiControllers
{
    public class DealsApiController: ApiController
    {
        private Model db = new Model();

        [HttpGet]
        public ICollection<DealModel> GetDeals()
        {
            List<Deal> dataRes = db.Deals.ToList();
            var res = Mapper.Map<ICollection<DealModel>>(dataRes);
            return res;
        }

        public class DealModel
        {
            public int DealId { get; set; }

            public string CreationDate { get; set; }

            public decimal? Sum { get; set; }

            public CustomerModel Customer { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string Type { get; set; }

            public string DealState { get; set; }
        }

        public class CustomerModel
        {
            public int CustomerId { get; set; }
            public string Name { get; set; }
        }
    }
}