using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CustomerBasketDTO
    {
        // always have validation annotations on Data Transfer objects, not on model entities (they must remain pure entities, without dependencies)
        [Required]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string StripeClientSecret { get; set; }
        public string PaymentIntentId { get; set; } // for updating the payment intent
        public decimal ShippingPrice { get; set; }
    }
}