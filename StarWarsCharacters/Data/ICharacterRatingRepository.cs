using StarWarsCharacters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Data
{
    public interface ICharacterRatingRepository : IEntityRepositoryAsync<CharacterRating>
    {
        Task<List<CharacterRating>> GetByCharacter(int IdCharacter);
        Task Insert(CharacterRating characterRating);
    }
}
