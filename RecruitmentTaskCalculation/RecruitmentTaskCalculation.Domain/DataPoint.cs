namespace RecruitmentTaskCalculation.Domain
{
    public class DataPoint
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        /// Unix epoch time.
        public int Timestamp { get; set; }
        
        public float Value { get; set; }
    }
}