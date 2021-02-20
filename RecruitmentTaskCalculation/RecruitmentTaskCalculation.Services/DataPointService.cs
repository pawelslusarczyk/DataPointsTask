namespace RecruitmentTaskCalculation.Services
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Domain;
    using Microsoft.Extensions.Configuration;
    using Npgsql;

    public class DataPointService
    {
        private readonly IConfiguration _configuration;

        public DataPointService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataPointSummaryReadModel> GetSummary(string dataPointName, int? epochFrom = null, int? epochTo = null)
        {
            string connectionString = _configuration.GetConnectionString("PostgreSqlConnectionString");
            
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string query = $"select * from {nameof(DataPoint)}s where name = @DataPointName";
                if (epochFrom != null)
                {
                    query += " and Timestamp >= @EpochFrom";
                }
                if (epochTo != null)
                {
                    query += " and Timestamp <= @EpochTo";
                }

                var parameters = new {DataPointName = dataPointName, EpochFrom = epochFrom, EpochTo = epochTo};
                List<DataPoint> matchingDataPoints = (await db.QueryAsync<DataPoint>(query, parameters)).ToList();

                if (!matchingDataPoints.Any())
                    return null;
                
                var result = new DataPointSummaryReadModel(matchingDataPoints.Average(p => p.Value), matchingDataPoints.Sum(p => p.Value));

                return result;
            }
        }
    }
}