using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;

namespace Infrastructure.Services {
    public class OrderService : IOrderService {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService (IUnitOfWork unitOfWork, IBasketRepository basketRepo) {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync (string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress) {
            // get the basket from repo to recheck on server the prices are correct (in case some tampering on the client side has happened)
            var basket = await _basketRepo.GetBasketAsync (basketId);
            // get the basket items from product repo
            var orderItems = new List<OrderItem> ();
            foreach (var item in basket.Items) {
                // get product item form db
                var productItem = await _unitOfWork.Repository<Product> ().GetOneAsync (item.Id);
                // get the basket item product info from db
                var itemOrdered = new ProductItemOrdered (productItem.Id, productItem.Name, productItem.PictureUrl);
                // create an order item with the trusted price info from the db, not from client
                var finalOrderItem = new OrderItem (itemOrdered, productItem.Price, item.Quantity);
                // finally, save the item in the orderItems List
                orderItems.Add (finalOrderItem);
            }
            // get the deliveryMethod from repo (we only have the delivery id at this stage)
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod> ().GetOneAsync (deliveryMethodId);
            // calculate subtotal from the prices we get from repo
            var subtotal = orderItems.Sum (item => item.Price * item.Quantity);
            // create order and add it to context
            var order = new Order (orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order> ().Add (order);
            // save order to the db
            var result = await _unitOfWork.Complete ();
            // check if changes were persisted to db. If not, a rollback of all changes occurs
            if (result <= 0) return null;

            // delete basket
            await _basketRepo.DeleteBasketAsync (basketId);
            // return order
            return order;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync () =>
            await _unitOfWork.Repository<DeliveryMethod> ().GetAllAsync ();

        public async Task<Order> GetOrderByIdAsync (int id, string buyerEmail) =>
            await _unitOfWork.Repository<Order> ().GetEntityWithSpec (new OrdersWithItemsAndDeliverySpecification (id, buyerEmail));
        // {
        //     var spec = new OrdersWithItemsAndDeliverySpecification(id, buyerEmail);
        //     return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        // }

        public async Task<IEnumerable<Order>> GetOrdersForUserAsync (string buyerEmail) =>
            await _unitOfWork.Repository<Order> ().GetListAsync (new OrdersWithItemsAndDeliverySpecification (buyerEmail));
        // {
        //     var spec = new OrdersWithItemsAndDeliverySpecification(buyerEmail);
        //     return await _unitOfWork.Repository<Order>().GetListAsync(spec);
        // }
    }
}