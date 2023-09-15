﻿using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace FoodDeliveryApi.contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

		public DapperContext()
		{
            _connectionString = "server=DESKTOP-DGU940A\\SQLEXPRESS; database=FoodDelivery; Integrated Security=true; Encrypt=false;";
		}

		public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("FoodDeliveryConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
