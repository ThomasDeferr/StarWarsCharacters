using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Models
{
    public class CharacterRatingCreationDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
