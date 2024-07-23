namespace trade.api.Models.Common
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public List<string> Message { get; set; }
    }
}
