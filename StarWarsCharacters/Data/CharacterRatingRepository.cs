using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StarWarsCharacters.Contexts;
using StarWarsCharacters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Data
{
    public class CharacterRatingRepository : ICharacterRatingRepository
    {
        private readonly ApplicationDBContext _context;

        public CharacterRatingRepository(ApplicationDBContext context)
        {
            this._context = context;
        }

        public async Task<CharacterRating> GetById(int Id)
        {
            var characterRating = await _context.CharactersRatings.FirstOrDefaultAsync(x => x.Id == Id);
            return characterRating;
        }

        public async Task<List<CharacterRating>> GetByCharacter(int IdCharacter)
        {
            var characterRatings = await _context.CharactersRatings.Where(x => x.IdCharacter == IdCharacter).ToListAsync();
            return characterRatings;
        }

        public async Task Insert(CharacterRating characterRating)
        {
            _context.CharactersRatings.Add(characterRating);
            await _context.SaveChangesAsync();
        }
    }
}
