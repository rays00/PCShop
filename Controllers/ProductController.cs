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
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly VendorService _vendorService;

        public ProductController(ProductService productService, VendorService vendorService)
        {
            _productService = productService;
            _vendorService = vendorService;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get() =>
           _productService.Get();

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.VendorIds.Count > 0)
            {
                var tempList = new List<Vendor>();
                foreach (var vendorId in product.VendorIds) {
                    var vendor = _vendorService.Get(vendorId);
                    if (vendor != null)
                    {
                        tempList.Add(vendor);
                    }
                }
                product.Vendors = tempList;
            }

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product productIn)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return NoContent();
        }
    }
}
