using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersForCurrentUser() =>
            Ok(await _orderService.GetOrdersForUserAsync(HttpContext.User.ReturnEmailFromPrincipal()));
        // {
        //     var email = HttpContext.User.ReturnEmailFromPrincipal();
        //     var orders = await _orderService.GetOrdersForUserAsync(email)
        //     return Ok(orders);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var email = HttpContext.User.ReturnEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if(order == null) 
                return NotFound(new ApiErrorResponse(404));
            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetAllDeliveryMethods() =>
            Ok(await _orderService.GetDeliveryMethodsAsync());

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync(OrderDTO orderDTO)
        {
            var email = HttpContext.User.ReturnEmailFromPrincipal();
            var address = _mapper.Map<AddressDTO, Address>(orderDTO.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, address);
            if (order == null)
                return BadRequest(new ApiErrorResponse(400, "Error! Order was not created."));
            return Ok(order);
        }
    }
}