using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}", Name = nameof(GetDiscountAsync))]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscountAsync(string productName)
        {
            var coupon = await _repository.GetDiscountAsync(productName);

            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscountAsync([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscountAsync(coupon);

            return CreatedAtRoute(nameof(GetDiscountAsync), new { productName = coupon.ProductName } ,coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscountAsync([FromBody] Coupon coupon)
        {
            return Ok(await _repository.UpdateDiscountAsync(coupon));
        }

        [HttpDelete("{productName}", Name = nameof(DeleteDiscountAsync))]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscountAsync(string productName)
        {
            return Ok(await _repository.DeleteDiscountAsync(productName));
        }
    }
}
