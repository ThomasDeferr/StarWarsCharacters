using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Entities
{
    [JsonObject(MemberSerialization=MemberSerialization.OptOut)]
    public class Planet
    {
        public Planet()
        {
        }

        public string Name { get; set; }
        public string Population { get; set; }
        public List<string> Residents { get; set; }
    }
}
