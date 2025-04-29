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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // GET: api/booking
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(bookingDtos);
        }

        // GET: api/booking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetById(Guid id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            return Ok(_mapper.Map<BookingDto>(booking));
        }

        // POST: api/booking
        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create([FromBody] BookingCreateDto dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            booking.Id = Guid.NewGuid();
            booking.Status = "Confirmed";

            var created = await _bookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<BookingDto>(created));
        }

        // PUT: api/booking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookingUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var booking = _mapper.Map<Booking>(dto);
            var updated = await _bookingService.UpdateBookingAsync(booking);
            return Ok(_mapper.Map<BookingDto>(updated));
        }

        // DELETE: api/booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _bookingService.CancelBookingAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
