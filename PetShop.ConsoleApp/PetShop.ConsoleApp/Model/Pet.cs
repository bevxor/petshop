using PetShop.ConsoleApp.Enum;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PetShop.ConsoleApp.Model
{
    public class Pet
    {
        public long Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<Tag> Tags { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetStatus Status { get; set; }
    }
}
