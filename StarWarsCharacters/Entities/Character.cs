using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Entities
{
    public class Character : People
    {
        public List<CharacterRating> Ratings { get; set; }
    }
}
