namespace RecruitmentTaskCalculation.Api.Controllers
{
    public class DataPointWriteModel
    {
        public string Name { get; set; }
        
        /// Timestamp (Unix epoch).
        public int T { get; set; }
        
        /// Value.
        public float V { get; set; }
    }
}