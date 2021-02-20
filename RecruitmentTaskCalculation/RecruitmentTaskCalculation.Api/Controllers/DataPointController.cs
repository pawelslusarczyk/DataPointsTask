namespace RecruitmentTaskCalculation.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services;

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
        public async Task<ActionResult> Get([FromRoute] string dataPointName, [FromQuery]int? epochFrom = null, [FromQuery]int? epochTo = null)
        {
            DataPointSummaryReadModel result = await _dataPointService.GetSummary(dataPointName, epochFrom, epochTo);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
