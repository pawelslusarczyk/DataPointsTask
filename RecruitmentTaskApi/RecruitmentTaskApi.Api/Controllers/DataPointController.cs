using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RecruitmentTask.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using DataAccess;
    using Domain;
    using RecruitmentTaskCalculation.Services;

    [ApiController]
    [Route("api")]
    public class DataPointController : ControllerBase
    {
        private readonly ILogger<DataPointController> _logger;
        private readonly DataPointService _dataPointService;

        public DataPointController(ILogger<DataPointController> logger, DataPointService dataPointService)
        {
            _logger = logger;
            _dataPointService = dataPointService;
        }

        [HttpGet("{dataPointName}")]
        public async Task<DataPointSummaryReadModel> Get([FromRoute] string dataPointName, [FromQuery]int? epochFrom = null, [FromQuery]int? epochTo = null)
        {
            return await _dataPointService.GetSummary(dataPointName, epochFrom, epochTo);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(DataPointWriteModel[] input)
        {
            IEnumerable<DataPoint> inputDataPoints = input.Select(i => new DataPoint
            {
                Name = i.Name,
                Timestamp = i.T,
                Value = i.V
            });
            
            await _dataPointService.AddAsync(inputDataPoints); 
            
            return StatusCode((int) HttpStatusCode.Accepted);
        }
    }
}
