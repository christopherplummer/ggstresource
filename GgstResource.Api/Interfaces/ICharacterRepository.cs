using System.Collections.Generic;
using System.Threading.Tasks;
using GgstResource.Api.Models.Request;
using GgstResource.Models;

namespace GgstResource.Api.Interfaces
{
    public interface ICharacterRepository
    {
        public Task<List<Character>> GetAll();
        public Task<Character> GetByReference(string reference);
        public Task<Character> Create(CharacterCreateRequest request);
        public Task<Character> Update(string reference, long version, CharacterUpdateRequest request);
    }
}