using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWarsCharacters.Contexts;
using StarWarsCharacters.Data;
using StarWarsCharacters.Entities;
using StarWarsCharacters.Models;

namespace StarWarsCharacters.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEntityRepositoryAsync<Character> _repositoryCharacter;
        private readonly ICharacterRatingRepository _repositoryCharacterRating;

        public CharacterController(IMapper mapper, IEntityRepositoryAsync<Character> repositoryCharacter, ICharacterRatingRepository repositoryCharacterRating)
        {
            this._mapper = mapper;
            this._repositoryCharacter = repositoryCharacter;
            this._repositoryCharacterRating = repositoryCharacterRating;
        }

        // GET character
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hola");
        }

        // GET character/5
        [HttpGet("{id}", Name = "GetCharacter")]
        public async Task<ActionResult<CharacterDTO>> Get([FromRoute] int id)
        {
            try
            {
                var character = await _repositoryCharacter.GetById(id);
                if (character == null) { return NotFound(); }

                var characterDTO = _mapper.Map<CharacterDTO>(character);
                return characterDTO;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET character/5/rating
        [HttpGet("{idCharacter}/rating", Name = "GetCharacterRatings")]
        public async Task<ActionResult<CharacterRatingDTO>> GetRatings([FromRoute] int idCharacter)
        {
            try
            {
                var characterRatings = await _repositoryCharacterRating.GetByCharacter(idCharacter);
                if (characterRatings == null || !characterRatings.Any()) { return NotFound(); }

                var characterRatingDTO = _mapper.Map<List<CharacterRatingDTO>>(characterRatings);
                return Ok(characterRatingDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST character/5/rating
        [HttpPost("{id}/rating", Name = "AddRatingCharacter")]
        public async Task<ActionResult<CharacterRatingDTO>> Post([FromRoute] int id, [FromBody] CharacterRatingCreationDTO characterRatingCreation)
        {
            try
            {
                var characterRating = _mapper.Map<CharacterRating>(characterRatingCreation);
                characterRating.IdCharacter = id;

                await _repositoryCharacterRating.Insert(characterRating);

                var characterRatingDTO = _mapper.Map<CharacterRatingDTO>(characterRating);
                return new CreatedAtRouteResult("GetCharacter", new { id = characterRatingDTO.Id }, characterRatingDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT character/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE character/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
