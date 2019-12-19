using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Models
{
    public class PlanetDTO
    {
        public string Name { get; set; }
        public string Population { get; set; }
        public int KnowResidentsCount { get; set; }
    }
}
