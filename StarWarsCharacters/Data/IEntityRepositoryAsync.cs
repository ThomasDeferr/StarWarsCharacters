using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsCharacters.Data
{
    public interface IEntityRepositoryAsync<T>
    {
        Task<T> GetById(int Id);
    }
}
