using PetShop.ConsoleApp.Constant;
using PetShop.ConsoleApp.Enum;
using PetShop.ConsoleApp.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using PetShop.ConsoleApp.Repository.Interface;

namespace PetShop.ConsoleApp.Repository
{
    public class PetShopRepository : IPetShopRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PetShopRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Pet>> GetPetsByStatus(PetStatus petStatus)
        {
            var query = new Dictionary<string, string>()
            {
                [PetShopRepositoryConstant.StatusQueryParameter] = petStatus.ToString().ToLower(),
            };

            var uri = QueryHelpers.AddQueryString(PetShopRepositoryConstant.FindByStatus, query);

            var httpClient = _httpClientFactory.CreateClient(PetShopRepositoryConstant.PetShopApiClientName);
            var httpResponseMessage = await httpClient.GetAsync(uri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                return await JsonSerializer.DeserializeAsync<List<Pet>>(contentStream, options);
            }

            return new List<Pet>();

        }
    }
}
