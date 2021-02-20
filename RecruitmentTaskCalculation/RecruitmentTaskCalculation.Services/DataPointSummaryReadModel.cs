namespace RecruitmentTaskCalculation.Services
{
    public class DataPointSummaryReadModel
    {
        /// For ASP.NET
        public DataPointSummaryReadModel()
        {
            
        }
        
        public DataPointSummaryReadModel(double avg, double sum)
        {
            Avg = avg;
            Sum = sum;
        }

        public double Avg { get; set; }
        public double Sum { get; set; }
    }
}