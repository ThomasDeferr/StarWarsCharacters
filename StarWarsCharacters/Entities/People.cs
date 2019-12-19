using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Entities
{
    [JsonObject(MemberSerialization.OptOut)]
    public class People
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string BirthYear { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Gender { get; set; }
        public Planet PlanetHomeworld { get; set; }
        [JsonProperty("Homeworld")]
        public string HomeworldURL { get; set; }
        public List<Kind> KindSpecies { get; set; }
        [JsonProperty("Species")]
        public List<string> SpeciesURLs { get; set; }
    }
}
