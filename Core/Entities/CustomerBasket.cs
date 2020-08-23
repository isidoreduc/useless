using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string id) 
        {
            this.Id = id;
        }

        public CustomerBasket()
        {
            
        }

        public string Id { get; set; }
        public IEnumerable<BasketItem> BasketItems { get; set; } = new List<BasketItem>();


    }
}