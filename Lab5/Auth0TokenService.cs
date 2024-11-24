using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class Auth0TokenService
{
    private readonly IConfiguration _configuration;

    public Auth0TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GetManagementApiTokenAsync()
    {
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];
        var domain = _configuration["Auth0:Domain"];
        var audience = _configuration["Auth0:Audience"];
        var tokenUrl = $"https://{domain}/oauth/token";

        using (var client = new HttpClient())
        {
            var requestData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("audience", audience),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

            var response = await client.PostAsync(tokenUrl, requestData);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Не вдалося отримати токен. Відповідь: {errorMessage}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {responseString}");

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
            if (tokenResponse == null)
            {
                throw new Exception("Failed to deserialize token response.");
            }

            Console.WriteLine($"Received token: {tokenResponse?.AccessToken}");
            return tokenResponse?.AccessToken;
        }
    }

    private class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

}
