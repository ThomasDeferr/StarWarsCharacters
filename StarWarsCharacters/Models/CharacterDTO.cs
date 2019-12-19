using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Models
{
    public class CharacterDTO
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string BirthYear { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Gender { get; set; }
        public PlanetDTO Homeworld { get; set; }
        public List<string> Species { get; set; }
        public decimal AverageRating { get; set; }
        public int MaxRating { get; set; }
    }
}
