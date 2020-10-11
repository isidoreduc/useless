using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class OrdersController : BaseApiController
  {
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    public OrdersController(IOrderService orderService, IMapper mapper)
    {
      _mapper = mapper;
      _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
    {
      var email = HttpContext.User.ReturnEmailFromPrincipal();

      var address = _mapper.Map<AddressDTO, Address>(orderDTO.ShipToAddress);

      var order = await _orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, address);

      if (order == null) return BadRequest(new ApiErrorResponse(400, "Problem creating order"));

      return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersForUser()
    {
      var email = HttpContext.User.ReturnEmailFromPrincipal();

      var orders = await _orderService.GetOrdersForUserAsync(email);

      return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdForUser(int id)
    {
      var email = HttpContext.User.ReturnEmailFromPrincipal();

      var order = await _orderService.GetOrderByIdAsync(id, email);

      if (order == null) return NotFound(new ApiErrorResponse(404));

      return _mapper.Map<Order, OrderToReturnDTO>(order);
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
    {
      return Ok(await _orderService.GetDeliveryMethodsAsync());
    }
  }
}