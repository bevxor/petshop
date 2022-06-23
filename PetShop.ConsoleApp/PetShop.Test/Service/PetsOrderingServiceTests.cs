using AutoFixture;
using DeepEqual.Syntax;
using NUnit.Framework;
using PetShop.ConsoleApp.Model;
using PetShop.ConsoleApp.Service;
using System.Linq;

namespace PetShop.Test.Service
{
    public class PetsOrderingServiceTests
    {
        private PetsOrderingService _sut;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _sut = new PetsOrderingService();
            _fixture = new Fixture();
        }

        [Test]
        public void Given_A_List_Of_Pets_Is_Given_Then_The_List_Should_Be_Ordered_By_Category_Then_By_Name_Descending()
        {
            //Arrage
            var pets = _fixture.CreateMany<Pet>(10).ToList();

            //Act
            var orderedPets = _sut.OrderByCategoryThenByNameDescending(pets);

            //Assert
            var correctOrder = pets.OrderBy(x => x.Category?.Id)
                .ThenByDescending(x => x.Name)
                .ToList();

            Assert.IsTrue(orderedPets.IsDeepEqual(correctOrder));
        }

        [Test]
        public void Given_A_List_Of_Pets_And_A_Pet_Has_No_Category_Then_The_Pet_With_No_Category_Should_Be_First()
        {
            //Arrage
            var pets = _fixture.CreateMany<Pet>(10).ToList();
            var petWithNoCategory = _fixture.Create<Pet>();
            petWithNoCategory.Category = null;

            pets.Add(petWithNoCategory);

            //Act
            var orderedPets = _sut.OrderByCategoryThenByNameDescending(pets);

            //Assert
            var correctOrder = pets.OrderBy(x => x.Category?.Id)
                .ThenByDescending(x => x.Name)
                .ToList();

            Assert.IsTrue(orderedPets.First().IsDeepEqual(petWithNoCategory));
            Assert.IsTrue(orderedPets.IsDeepEqual(correctOrder));
        }
    }
}