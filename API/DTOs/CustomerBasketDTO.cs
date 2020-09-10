using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CustomerBasketDTO
    {
        // always have validation annotations on Data Transfer objects, not on model entities (they must remain pure entities, without dependencies)
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDTO> Items { get; set; }
    }
}