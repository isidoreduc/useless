using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> deliveryMethodRepo, IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get the basket from repo to recheck on server the prices are correct (in case some tampering on the client side has happened)
            var basket = await _basketRepo.GetBasketAsync(basketId);
            // get the basket items from product repo
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                // get product item form db
                var productItem = await _productRepo.GetOneAsync(item.Id);
                // get the basket item product info from db
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                // create an order item with the trusted price info from the db, not from client
                var finalOrderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                // finally, save the item in the orderItems List
                orderItems.Add(finalOrderItem);
            }
            // get the deliveryMethod from repo (we only have the delivery id at this stage)
            var deliveryMethod = await _deliveryMethodRepo.GetOneAsync(deliveryMethodId);
            // calculate subtotal from the prices we get from repo
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            // create order
            var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            // save order to the db
            //TODO
            // return order
            return order;
        }

        public Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}