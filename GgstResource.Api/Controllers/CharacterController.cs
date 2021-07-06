using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GgstResource.Api.Interfaces;
using GgstResource.Api.Models.Request;
using GgstResource.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GgstResource.Api.Controllers
{
    [ApiController]
    [Route("characters")]
    public class CharacterController
    {
        private readonly ICharacterRepository _repository;
        public CharacterController(ICharacterRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Character>))]
        public async Task<IEnumerable<Character>> GetAll()
        {
            return await _repository.GetAll();
        }
        
        [HttpGet]
        [Route("{reference}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Character> GetByReference(string reference)
        {
            return await _repository.GetByReference(reference);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<Character> Create(CharacterCreateRequest request)
        {
            return await _repository.Create(request);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public Character Update(string reference, [FromHeader] long ifMatch, CharacterUpdateRequest request)
        {
            return new Character();
        }
    }
}