using PetShop.ConsoleApp.Model;
using System.Collections.Generic;

namespace PetShop.ConsoleApp.Service.Interface
{
    public interface IPetsOrderingService
    {
        List<Pet> OrderByCategoryThenByNameDescending (List<Pet> pets);
    }
}
