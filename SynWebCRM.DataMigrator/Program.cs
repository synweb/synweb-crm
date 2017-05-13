using System;
using System.Linq;

namespace SynWebCRM.DataMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            string mssqlConnectionString = "";
            string pgsqlConnectionString = "User ID=synwebcrm;Password=777888;Host=localhost;Port=5432;Database=synwebcrm;Pooling=true;";

            using (var mssqldb = new MSSQL.Model(mssqlConnectionString))
            using (var pgsqldb = new PGSQL.CRMModel(pgsqlConnectionString))
            {
                //pgsqldb.Notes.RemoveRange(pgsqldb.Notes);
                //pgsqldb.Customers.RemoveRange(pgsqldb.Customers);
                //pgsqldb.DealStates.RemoveRange(pgsqldb.DealStates);
                //pgsqldb.Employees.RemoveRange(pgsqldb.Employees);
                //pgsqldb.Events.RemoveRange(pgsqldb.Events);
                //pgsqldb.ServiceTypes.RemoveRange(pgsqldb.ServiceTypes);
                //pgsqldb.Deals.RemoveRange(pgsqldb.Deals);
                //pgsqldb.Websites.RemoveRange(pgsqldb.Websites);
                //pgsqldb.Estimates.RemoveRange(pgsqldb.Estimates);
                //pgsqldb.EstimateItems.RemoveRange(pgsqldb.EstimateItems);
                //pgsqldb.SaveChanges();
                pgsqldb.Customers.AddRange(mssqldb.Customers);
                pgsqldb.DealStates.AddRange(mssqldb.DealStates);
                pgsqldb.Employees.AddRange(mssqldb.Employees);
                pgsqldb.Events.AddRange(mssqldb.Events);
                pgsqldb.ServiceTypes.AddRange(mssqldb.ServiceTypes);
                pgsqldb.Deals.AddRange(mssqldb.Deals);
                pgsqldb.Websites.AddRange(mssqldb.Websites);
                pgsqldb.Estimates.AddRange(mssqldb.Estimates);
                pgsqldb.EstimateItems.AddRange(mssqldb.EstimateItems);
                pgsqldb.SaveChanges();
            }
        }
    }
}