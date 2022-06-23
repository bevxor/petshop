using PetShop.ConsoleApp.Model;
using PetShop.ConsoleApp.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.ConsoleApp.Service
{
    public class PetsOrderingService : IPetsOrderingService
    {
        public List<Pet> OrderByCategoryThenByNameDescending(List<Pet> pets)
        {
            return pets.OrderBy(x => x.Category?.Id)
                .ThenByDescending(x => x.Name)
                .ToList();
        }
    }
}
