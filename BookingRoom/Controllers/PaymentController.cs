using AutoMapper;
using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        // GET: api/payment
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetById(Guid id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();

            return Ok(_mapper.Map<PaymentDto>(payment));
        }

        // POST: api/payment
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> Create(PaymentCreateDto dto)
        {
            var payment = _mapper.Map<Payment>(dto);
            payment.Id = Guid.NewGuid();
            payment.PaymentDate = DateTime.UtcNow;

            var created = await _paymentService.CreatePaymentAsync(payment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<PaymentDto>(created));
        }

        // PUT: api/payment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PaymentUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var payment = _mapper.Map<Payment>(dto);
            var updated = await _paymentService.UpdatePaymentAsync(payment);
            return Ok(_mapper.Map<PaymentDto>(updated));
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
