using System.Text.Json.Serialization;

namespace Application.Responses.Configuration
{
    public class ConfigResponse
    {
        [JsonPropertyName("publishableKey")]
        public string PublishableKey { get; set; }

        [JsonPropertyName("proPrice")]
        public string ProPrice { get; set; }
    }
}
