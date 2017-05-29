using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SynWebCRM.Contract.Repositories;
using SynWebCRM.Data.EF;
using SynWebCRM.Data.EF.Models;
//using SynWebCRM.Contract.Models;
//using SynWebCRM.Contract.Repositories;
//using SynWebCRM.Data.EF;
//using SynWebCRM.Data.EF.Models;
//using SynWebCRM.Web.ApiControllers.Models;
using SynWebCRM.Web.Services;

namespace SynWebCRM.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            // Add framework services.
            services.AddDbContext<CRMModel>(options =>
                options.UseNpgsql(
                sqlConnectionString,
                b => b.MigrationsAssembly("SynWebCRM.Data.EF")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CRMModel>()
                .AddDefaultTokenProviders();


            // Uncomment to use mock storage
            services.AddScoped(typeof(IStorage), typeof(CRMModel));
            // Uncomment to use SQLite storage
            //services.AddScoped(typeof(IStorage), typeof(AspNetCoreStorage.Data.Sqlite.Storage));


            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDealRepository, DealRepository>();
            services.AddTransient<IDealStateRepository, DealStateRepository>();
            services.AddTransient<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddTransient<IWebsiteRepository, WebsiteRepository>();
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            ConfigureMapper();
        }


        public static void ConfigureMapper()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    //cfg.CreateMap<Customer, DealsApiController.CustomerModel>();

            //    cfg.CreateMap<Deal, DealModel>()
            //        .ForMember(x => x.CreationDate, x => x.MapFrom(y => y.CreationDate.ToString("yyyy-MM-dd HH:mm")))
            //        .ForMember(x => x.Customer, x => x.MapFrom(y => y.Customer.Name))
            //        .ForMember(x => x.DealState, x => x.MapFrom(y => y.DealState.Name))
            //        .ForMember(x => x.Type, x => x.MapFrom(y => y.Type == DealType.Incoming ? "Входящая" : "Исходящая"))
            //        .ForMember(x => x.Customer, x => x.MapFrom(y => new CustomerModel { CustomerId = y.Customer.CustomerId, Name = y.Customer.Name }));
            //});
            //Mapper.AssertConfigurationIsValid();
        }

        //public class DecimalModelBinder : Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder
        //{
        //    public async Task BindModelAsync(ModelBindingContext bindingContext)
        //    {
        //        object result = null;

        //        // Don't do this here!
        //        // It might do bindingContext.ModelState.AddModelError
        //        // and there is no RemoveModelError!
        //        // 
        //        // result = base.BindModel(controllerContext, bindingContext);

        //        string modelName = bindingContext.ModelName;
        //        string attemptedValue =
        //            bindingContext.ValueProvider.GetValue(modelName).FirstValue;

        //        // Depending on CultureInfo, the NumberDecimalSeparator can be "," or "."
        //        // Both "." and "," should be accepted, but aren't.
        //        string wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        //        string alternateSeperator = (wantedSeperator == "," ? "." : ",");

        //        if (attemptedValue.IndexOf(wantedSeperator) == -1
        //            && attemptedValue.IndexOf(alternateSeperator) != -1)
        //        {
        //            attemptedValue =
        //                attemptedValue.Replace(alternateSeperator, wantedSeperator);
        //        }

        //        try
        //        {
        //            if (bindingContext.ModelMetadata.IsNullableValueType
        //                && string.IsNullOrWhiteSpace(attemptedValue))
        //            {
        //                return;
        //            }

        //            result = decimal.Parse(attemptedValue, NumberStyles.Any);
        //        }
        //        catch (FormatException e)
        //        {
        //            bindingContext.ModelState.AddModelError(modelName, e.ToString());
        //        }
        //        //bindingContext.
        //    }
        //}
    }
}
