namespace trade.api.Models.DTOs.LoginDTOs
{
    public class LoginGetDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public decimal Deposit { get; set; }
    }
}
