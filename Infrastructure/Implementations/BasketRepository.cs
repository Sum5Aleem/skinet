using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;
        public BasketRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteBasket(string basketId)
        {
            var data = await _context.CustomerBaskets.FirstOrDefaultAsync(p => p.Id == basketId);
            if (data != null)
            {
                _context.CustomerBaskets.Remove(data);
                var deleted = await _context.SaveChangesAsync();
                if (deleted > 0) return true;
            }
            return false;
        }

        public async Task<Basket> GetBasket(string basketId)
        {
            var data = await _context.CustomerBaskets.FirstOrDefaultAsync(p => p.Id == basketId);
            if (data != null)
            {
                int value = DateTime.Compare(data.Expire, DateTime.Now);
                if (value > 0)
                {
                    return string.IsNullOrEmpty(data.Id) ? null : new Basket
                    {
                        Id = basketId,
                        Items = JsonSerializer.Deserialize<List<BasketItem>>(data.BasketItems)
                    };
                }
            }
            return new Basket { Id = basketId };
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            var data = await _context.CustomerBaskets.FirstOrDefaultAsync(p => p.Id == basket.Id);
            if (data != null)
            {
                data.BasketItems = JsonSerializer.Serialize(basket.Items);
                data.Expire = DateTime.Now.AddMinutes(10);

                _context.CustomerBaskets.Update(data);
                var updated = await _context.SaveChangesAsync();

                if (updated == 0) return null;
            }
            else
            {
                var customerContainer = new CustomerBasket()
                {
                    Id = basket.Id,
                    BasketItems = JsonSerializer.Serialize(basket.Items),
                    Expire = DateTime.Now.AddMinutes(10)
                };
                _context.CustomerBaskets.Add(customerContainer);
                var created = await _context.SaveChangesAsync();

                if (created == 0) return null;
            }
            return basket;
        }
    }
}
