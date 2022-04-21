using System;
using Loyalty.DataLayer;
using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Loyalty.Shared;
using System.Data.SqlClient;

namespace FundaClear.Business
{
    public static class Helper
    {
        public static string GetConnectionString()
        {
            return "Data Source=164.52.195.234; Initial Catalog=Loyalty_dev; Integrated Security=False; User ID=Loyalty_dev;Password=Dell@2022; MultipleActiveResultSets=True";
        }
            
    }
}
