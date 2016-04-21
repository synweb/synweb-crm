using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SynWebCRM.ApiControllers;
using SynWebCRM.ApiControllers.Models;
using SynWebCRM.Data;

namespace SynWebCRM
{
    public static class MapperConfig
    {
        public static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Customer, DealsApiController.CustomerModel>();

                cfg.CreateMap<Deal, DealModel>()
                    .ForMember(x => x.CreationDate, x => x.MapFrom(y => y.CreationDate.ToString("yyyy-MM-dd HH:mm")))
                    .ForMember(x => x.Customer, x => x.MapFrom(y => y.Customer.Name))
                    .ForMember(x => x.DealState, x => x.MapFrom(y => y.DealState.Name))
                    .ForMember(x => x.Type, x => x.MapFrom(y => y.Type == DealType.Incoming ? "Входящая" : "Исходящая"))
                    .ForMember(x => x.Customer, x => x.MapFrom(y => new CustomerModel {CustomerId = y.Customer.CustomerId, Name = y.Customer.Name} ))
                    ;

                cfg.CreateMap<Estimate, EstimateModel>();

            });
        }
    }
}