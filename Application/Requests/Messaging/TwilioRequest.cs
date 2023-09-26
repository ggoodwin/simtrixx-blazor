namespace Application.Requests.Messaging
{
    public class TwilioRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}
