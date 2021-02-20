namespace RecruitmentTaskCalculation.Services
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using RecruitmentTask.DataAccess;
    using RecruitmentTask.Domain;
    using RestSharp;

    public class DataPointService
    {
        private readonly IConfiguration _configuration;
        private IRestClient _restClient;

        public DataPointService(IConfiguration configuration)
        {
            _configuration = configuration;

            string calculationServiceUrl = "http://recruitment_task_calculation:5000";// todo get from configuration
            
            _restClient = new RestClient(calculationServiceUrl); // todo: inject by DI
        }

        public virtual async Task AddAsync(IEnumerable<DataPoint> input)
        {
            string connectionString = _configuration.GetConnectionString("PostgreSqlConnectionString");
            
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string query = $"insert into {nameof(DataPoint)}s (name, timestamp, value) values (@Name, @Timestamp, @Value)"; // todo: perform bulk insert
                await db.ExecuteAsync(query, input);
            }
        }

        public async Task<DataPointSummaryReadModel> GetSummary(string dataPointName, int? epochFrom = null, int? epochTo = null)
        {
            var request = new RestRequest("api/" + dataPointName);
            if (epochFrom != null)
            {
                request.AddParameter("epochFrom", epochFrom);
            }
            if (epochTo != null)
            {
                request.AddParameter("epochTo", epochTo);
            }

            DataPointSummaryReadModel result = await _restClient.GetAsync<DataPointSummaryReadModel>(request);

            return result;
        }
    }
}