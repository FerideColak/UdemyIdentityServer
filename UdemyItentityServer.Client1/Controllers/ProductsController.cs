using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyItentityServer.Client1.Models;

namespace UdemyItentityServer.Client1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<Product>? products = new List<Product>();
            HttpClient httpClient = new HttpClient();

            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7211"); // auth server'ın discovery endpoint'inde listelenen endpointleri sınıfın parametreleri olarak doner

            if (discovery.IsError)
            {
                //log
            }

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();
            clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            clientCredentialsTokenRequest.Address = discovery.TokenEndpoint;

            var token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if(token.IsError)
            {
                //log
            }

            httpClient.SetBearerToken(token.AccessToken);  // alınan access token değerini ilgili isteğin header'ına ekler

            var response = await httpClient.GetAsync("https://localhost:7235/api/products/getproducts");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(content);                
            }
            else
            {
                //log
            }

            return View(products);
        }
    }
}
