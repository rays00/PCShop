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
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly TokenService _tokenService;

        public OrderController(OrderService orderService, ProductService productService, TokenService tokenService)
        {
            _orderService = orderService;
            _productService = productService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
           _orderService.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("byuser/{token:length(8)}", Name = "GetOrderByUserId")]
        public ActionResult<List<Order>> GetByUser(string token)
        {

            var tokens = _tokenService.GetTokens();

            var userId = tokens.FirstOrDefault(x => x.Value == token).Key;

            var orders = _orderService.GetByUserId(userId);

            foreach (Order order in orders) 
            {
                order.ProductNames = new List<string>();
                foreach (String productId in order.ProductIds)
                {
                    var product = _productService.Get(productId);
                    order.ProductNames.Add(product.Name);
                }
            }

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpPost]
        public ActionResult<Order> Create(PostOrderRequest request)
        {
            var results = new List<String>();
            var tokens = this._tokenService.GetTokens();

            String userId = tokens.FirstOrDefault(x => x.Value == request.UserToken).Key;
            request.Order.UserId = userId;

            if (userId != null)
            {
                _orderService.Create(request.Order);
                return CreatedAtRoute("GetOrder", new { id = request.Order.Id.ToString() }, request.Order);
            } else
            {
                return Unauthorized();
            }

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order orderIn)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Update(id, orderIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Remove(order.Id);

            return NoContent();
        }
    }
}

public class PostOrderRequest
{
    public Order Order { get; set; }
    public string UserToken { get; set; }
}