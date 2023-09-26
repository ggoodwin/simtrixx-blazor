namespace Application.Requests.Messaging
{
    public class MultipleMailRequest
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? HtmlBody { get; set; }
        public List<string>? To { get; set; }
        public string? ToName { get; set; }
    }
}
