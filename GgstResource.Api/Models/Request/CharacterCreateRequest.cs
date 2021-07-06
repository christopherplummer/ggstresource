using System.ComponentModel.DataAnnotations;

namespace GgstResource.Api.Models.Request
{
    public class CharacterCreateRequest
    {
        [Required]
        public string Reference { get; set; }
        [Required]
        public string Name { get; set; }
    }
}