using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
      public class BasketRepository : IBasketRepository
      {
            public BasketRepository(IConnectionMultiplexer redis)
            {
                _basketDb = redis.GetDatabase();
            }

            public IDatabase _basketDb { get; }

            public async Task<bool> DeleteBasketAsync(string basketId)
            {
                return await _basketDb.KeyDeleteAsync(basketId);
            }

            // when you get data from db you deserialize it into an object
            public async Task<CustomerBasket> GetBasketAsync(string basketId)
            {
                var data = await _basketDb.StringGetAsync(basketId);
                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
            }

            // when you put data in db you first serialize it
            public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
            {
                var created = await _basketDb.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
                return created ? await GetBasketAsync(basket.Id) : null;
            }
      }
}