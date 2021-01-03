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
    public class VendorController : ControllerBase
    {
        private readonly VendorService _vendorService;

        public VendorController(VendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public ActionResult<List<Vendor>> Get() =>
           _vendorService.Get();

        [HttpGet("{id:length(24)}", Name = "GetVendor")]
        public ActionResult<Vendor> Get(string id)
        {
            var vendor = _vendorService.Get(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        [HttpPost]
        public ActionResult<Vendor> Create(Vendor vendor)
        {
            _vendorService.Create(vendor);

            return CreatedAtRoute("GetVendor", new { id = vendor.Id.ToString() }, vendor);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Vendor vendorIn)
        {
            var vendor = _vendorService.Get(id);

            if (vendor == null)
            {
                return NotFound();
            }

            _vendorService.Update(id, vendorIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var vendor = _vendorService.Get(id);

            if (vendor == null)
            {
                return NotFound();
            }

            _vendorService.Remove(vendor.Id);

            return NoContent();
        }
    }
}
