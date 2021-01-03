using PCShop.Models;
using PCShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimatedShippingController : ControllerBase
    {
        private readonly EstimatedShippingService _estimatedShippingService;

        public EstimatedShippingController(EstimatedShippingService estimatedShippingService)
        {
            _estimatedShippingService = estimatedShippingService;
        }

        [HttpGet]
        public ActionResult<List<EstimatedShipping>> Get() =>
           _estimatedShippingService.Get();

        [HttpGet("{id:length(24)}", Name = "GetEstimatedShipping")]
        public ActionResult<EstimatedShipping> Get(string id)
        {
            var estimatedShipping = _estimatedShippingService.Get(id);

            if (estimatedShipping == null)
            {
                return NotFound();
            }

            return estimatedShipping;
        }

        [HttpPost]
        public ActionResult<EstimatedShipping> Create(EstimatedShipping estimatedShipping)
        {
            estimatedShipping.EstimatedDate = DateTime.Now.Date;

            estimatedShipping.EstimatedDate = estimatedShipping.EstimatedDate.AddDays(4);

            _estimatedShippingService.Create(estimatedShipping);

            return CreatedAtRoute("GetEstimatedShipping", new { id = estimatedShipping.Id.ToString() }, estimatedShipping);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, EstimatedShipping estimatedShippingIn)
        {
            var estimatedShipping = _estimatedShippingService.Get(id);

            if (estimatedShipping == null)
            {
                return NotFound();
            }

            _estimatedShippingService.Update(id, estimatedShippingIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var estimatedShipping = _estimatedShippingService.Get(id);

            if (estimatedShipping == null)
            {
                return NotFound();
            }

            _estimatedShippingService.Remove(estimatedShipping.Id);

            return NoContent();
        }
    }
}
