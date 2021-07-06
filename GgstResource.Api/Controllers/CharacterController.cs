using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Character>))]
        public IEnumerable<Character> GetAll()
        {
            return new List<Character>();
        }
        
        [HttpGet]
        [Route("{reference}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Character GetByReference(string reference)
        {
            return new Character();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public Character Create(CharacterCreateRequest request)
        {
            return new Character();
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