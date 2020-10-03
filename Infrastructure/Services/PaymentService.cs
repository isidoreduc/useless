using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            throw new System.NotImplementedException();
        }
    }
}