using AutoMapper;
using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(Guid id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customer = _mapper.Map<Customer>(dto);
            var created = await _service.CreateAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<CustomerDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerDto dto)
        {
            if (id != dto.Id) return BadRequest("Mismatched ID.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(dto);
            var updated = await _service.UpdateAsync(customer);
            return Ok(_mapper.Map<CustomerDto>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
