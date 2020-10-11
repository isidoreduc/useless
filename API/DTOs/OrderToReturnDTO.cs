using System;
using System.Collections.Generic;
using Core.Entities.OrderAggregate;

namespace API.DTOs
{
  // used for flattening the order object(object in an object in an object structure) to send to client side
  public class OrderToReturnDTO
  {
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTime OrderDate { get; set; }
    public Address ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal DeliveryPrice { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
  }
}