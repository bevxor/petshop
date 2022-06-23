using PetShop.ConsoleApp.Enum;
using PetShop.ConsoleApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetShop.ConsoleApp.Repository.Interface
{
    public interface IPetShopRepository
    {
        Task<List<Pet>> GetPetsByStatus(PetStatus petStatus);
    }
}
