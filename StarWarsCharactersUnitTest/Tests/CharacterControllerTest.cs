using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarWarsCharacters.Profiles;
using StarWarsCharacters.Contexts;
using StarWarsCharacters.Controllers;
using StarWarsCharacters.Data;
using StarWarsCharacters.Entities;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using StarWarsCharacters.Models;
using System.Collections.Generic;
using FluentAssertions;
using StarWarsCharactersUnitTest.Contexts;
using Microsoft.Extensions.Configuration;

namespace StarWarsCharactersUnitTest
{
    [TestClass]
    public class CharacterControllerTest
    {
        private readonly ApplicationDBContext _dbContext;

        public CharacterControllerTest()
        {
            _dbContext = new InMemoryDbContextFactory().GetApplicationDbContext();
        }

        [TestMethod]
        public void Get_CharacterNotExist_ReturnNotFound()
        {
            //Arrange}
            int idCharacter = 1;

            #region AutoMapper configuration
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new CharacterProfile());
                config.AddProfile(new CharacterRatingProfile());
                config.AddProfile(new PlanetProfile());
            });
            IMapper autoMapper = mapperConfiguration.CreateMapper();
            #endregion

            var mockCharacterRepository = new Mock<IEntityRepositoryAsync<Character>>();
            mockCharacterRepository.Setup(x => x.GetById(idCharacter)).ReturnsAsync(default(Character));

            var mockCharacterRatingRepository = new Mock<ICharacterRatingRepository>();

            CharacterController characterController = new CharacterController(autoMapper, mockCharacterRepository.Object, mockCharacterRatingRepository.Object);

            //Act
            var resultado = characterController.Get(idCharacter);

            //Assert
            Assert.IsInstanceOfType(resultado.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Get_CharacterExist_ReturnCharacterDTO()
        {
            //Arrange
            int idCharacter = 1;

            Character character = new Character()
            {
                Name = "Character 1",
                Height = "101",
                Mass = "51",
                BirthYear = "2001",
                SkinColor = "Green",
                EyeColor = "Black",
                HairColor = "White",
                Gender = "Male",
                PlanetHomeworld = new Planet()
                {
                    Name = "Earth",
                    Population = "4.000.000.000",
                    Residents = new List<string>() { "", "" }
                },
                KindSpecies = new List<Kind>()
                {
                    new Kind() { Name = "Human" },
                    new Kind() { Name = "Naboo" }
                },
                Ratings = new List<CharacterRating>()
                {
                    new CharacterRating()
                    {
                        Id = 1,
                        IdCharacter = idCharacter,
                        Rating = 4
                    },
                    new CharacterRating()
                    {
                        Id = 2,
                        IdCharacter = idCharacter,
                        Rating = 2
                    },
                    new CharacterRating()
                    {
                        Id = 3,
                        IdCharacter = idCharacter,
                        Rating = 3
                    }
                }
            };
            CharacterDTO characterDTO = new CharacterDTO()
            {
                Name = "Character 1",
                Height = "101",
                Mass = "51",
                BirthYear = "2001",
                SkinColor = "Green",
                EyeColor = "Black",
                HairColor = "White",
                Gender = "Male",
                Homeworld = new PlanetDTO()
                {
                    Name = "Earth",
                    Population = "4.000.000.000",
                    KnowResidentsCount = 2
                },
                Species = new List<string>() { "Human", "Naboo" },
                AverageRating = 3M,
                MaxRating = 4
            };

            #region AutoMapper configuration
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new CharacterProfile());
                config.AddProfile(new CharacterRatingProfile());
                config.AddProfile(new PlanetProfile());
            });
            IMapper autoMapper = mapperConfiguration.CreateMapper();
            #endregion

            var mockCharacterRepository = new Mock<IEntityRepositoryAsync<Character>>();
            mockCharacterRepository.Setup(x => x.GetById(idCharacter)).ReturnsAsync(character);
            
            var mockCharacterRatingRepository = new Mock<ICharacterRatingRepository>();

            CharacterController characterController = new CharacterController(autoMapper, mockCharacterRepository.Object, mockCharacterRatingRepository.Object);

            //Act
            var resultado = characterController.Get(idCharacter);

            //Assert
            resultado.Result.Value.Should().BeEquivalentTo(characterDTO);
        }

        [TestMethod]
        public void Post_SendRating_ReturnOkWithCharacterRatingDTO()
        {
            //Arrange}
            int idCharacterRating = 1;
            int idCharacter = 1;
            CharacterRatingCreationDTO characterRatingCreationDTO = new CharacterRatingCreationDTO()
            {
                Rating = 4
            };
            CharacterRatingDTO characterRatingDTO = new CharacterRatingDTO()
            {
                Id = idCharacterRating,
                IdCharacter = idCharacter,
                Rating = 4
            };
            CharacterRating characterRating = new CharacterRating()
            {
                Id = idCharacterRating,
                IdCharacter = idCharacter,
                Rating = 4
            };

            #region AutoMapper configuration
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new CharacterRatingProfile());
            });
            IMapper autoMapper = mapperConfiguration.CreateMapper();
            #endregion

            var mockCharacterRepository = new Mock<IEntityRepositoryAsync<Character>>();

            ICharacterRatingRepository mockCharacterRatingRepository = new CharacterRatingRepository(_dbContext);

            CharacterController characterController = new CharacterController(autoMapper, mockCharacterRepository.Object, mockCharacterRatingRepository);

            //Act
            var resultado = characterController.Post(idCharacter, characterRatingCreationDTO);

            //Assert
            Assert.IsInstanceOfType(resultado.Result.Result, typeof(CreatedAtRouteResult));
            ((CreatedAtRouteResult)resultado.Result.Result).Value.Should().BeEquivalentTo(characterRatingDTO);
        }
    }
}
