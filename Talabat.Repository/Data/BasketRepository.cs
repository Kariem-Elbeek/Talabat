using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redisDB;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _redisDB = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redisDB.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await _redisDB.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateOrCreateBasketAsync(CustomerBasket basket)
        {
            var resultBasket = await _redisDB.StringSetAsync(basket.Id, JsonConvert.SerializeObject(basket), TimeSpan.FromDays(30));
            if (resultBasket == false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
