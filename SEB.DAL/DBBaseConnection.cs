using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEB.DAL
{
    public class DBBaseConnection
    {
        internal IDbConnection Connection
        {
            get
            {
                // Set up configuration
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false);

                var configuration = builder.Build();

                // get connection string
                string strConn = configuration.GetConnectionString("DBConnectionString");
                return new SqlConnection(strConn);
            }
        }
    }
}
