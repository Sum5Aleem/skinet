using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasket(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(Basket basket)
        {
            var updatedBasket = await _basketRepository.UpdateBasket(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task<bool> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasket(id);
        }
    }
}
