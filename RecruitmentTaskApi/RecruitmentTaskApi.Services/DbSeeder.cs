namespace RecruitmentTaskCalculation.Services
{
    using System.Data;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Npgsql;

    public class DbSeeder
    {
        public static void Seed(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("PostgreSqlConnectionString");
            
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string query = $"create table if not exists DataPoints (" 
                               + $"id SERIAL primary key not null, " 
                               + $"name text not null, " 
                               + $"timestamp int not null, " 
                               + $"value real not null" 
                               + $")";
                db.Execute(query);
            }
        }
    }
}