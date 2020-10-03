using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _config;
        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _config = config;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            // get the secret key
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            // get the basket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            var deliveryPrice = 0m;
            // get the delivery price
            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetOneAsync(basket.DeliveryMethodId ?? 1);
                deliveryPrice = deliveryMethod.Price;
            }
            // check if the price of items in the basket coming from client is the same as the in our database - don't trust the client!
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetOneAsync(item.Id);
                if(item.Price != productItem.Price) item.Price = productItem.Price;
            }
            // set up stripe payment intent
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId)) // create a new intent
            {
                var options = new PaymentIntentCreateOptions() 
                {
                    // stripe uses long, not decimal, so cents, not dollars, so we need to multiply by 100
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100) + (long) deliveryPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>(){"card"}
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.StripeClientSecret = intent.ClientSecret;
            }
            else // update an intent
            {
                var options = new PaymentIntentUpdateOptions() 
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100) + (long) deliveryPrice * 100)
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            // update basket repo with the new prices and intent
            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

    }
     
}