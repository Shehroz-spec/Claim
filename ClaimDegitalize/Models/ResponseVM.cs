namespace ClaimDigitalize.Models
{
    public class ResponseVM<T> where T : class, new()
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public double ClaimIntimationAmount { get; set; }
        public double ClaimPaidAmount { get; set; }
    }
}