using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndDeliverySpecification : BaseSpecification<Order>
    {
        // for all orders of an user
        public OrdersWithItemsAndDeliverySpecification(string email) : base(order => order.BuyerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        // for one order of user
        public OrdersWithItemsAndDeliverySpecification(int orderId, string buyerEmail) : base(o => o.Id == orderId && o.BuyerEmail == buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}