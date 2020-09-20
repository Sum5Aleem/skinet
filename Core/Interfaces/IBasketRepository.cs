using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasket(string basketId);
        Task<Basket> UpdateBasket(Basket basket);
        Task<bool> DeleteBasket(string basketId);
    }
}
