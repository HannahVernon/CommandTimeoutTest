using System;
using System.Data.SqlClient;

namespace ConnectionTimeoutTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder
            {
                InitialCatalog = "tempdb",
                IntegratedSecurity = true,
                DataSource = "Words"
            };

            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
            connection.Open();
            using SqlCommand sqlCommand = new SqlCommand("WAITFOR DELAY '00:00:31';SELECT 'Hello World!';", connection);
            sqlCommand.CommandTimeout = 60;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            };
        }
    }
}
