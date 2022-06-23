using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetShop.ConsoleApp.Constant;
using PetShop.ConsoleApp.Repository;
using PetShop.ConsoleApp.Repository.Interface;
using PetShop.ConsoleApp.Service;
using PetShop.ConsoleApp.Service.Interface;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetShop.ConsoleApp
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var petShoprepo = host.Services.GetRequiredService<IPetShopRepository>();
            var petsOrderingService = host.Services.GetRequiredService<IPetsOrderingService>();

            var pets = await petShoprepo.GetPetsByStatus(Enum.PetStatus.available);

            var orderPets = petsOrderingService.OrderByCategoryThenByNameDescending(pets);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var petsString = JsonSerializer.Serialize(orderPets, options);

            Console.WriteLine(petsString);

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHttpClient(PetShopRepositoryConstant.PetShopApiClientName, httpClient =>
                    {
                        httpClient.BaseAddress = new Uri(PetShopRepositoryConstant.BaseUrl);
                    });

                    services.AddTransient<IPetShopRepository, PetShopRepository>();
                    services.AddTransient<IPetsOrderingService, PetsOrderingService>();
                });
        }
    }
}
