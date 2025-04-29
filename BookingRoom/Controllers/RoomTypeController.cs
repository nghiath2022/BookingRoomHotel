using AutoMapper;
using BookingRoom.Data;
using BookingRoom.DTOs;
using BookingRoom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoomTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoomTypeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/roomtype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeDto>>> GetAll()
        {
            var types = await _context.RoomTypes.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RoomTypeDto>>(types));
        }

        // GET: api/roomtype/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeDto>> GetById(Guid id)
        {
            var type = await _context.RoomTypes.FindAsync(id);
            if (type == null) return NotFound();

            return Ok(_mapper.Map<RoomTypeDto>(type));
        }

        // POST: api/roomtype
        [HttpPost]
        public async Task<ActionResult<RoomTypeDto>> Create(RoomTypeDto dto)
        {
            var type = _mapper.Map<RoomType>(dto);
            type.Id = Guid.NewGuid();

            _context.RoomTypes.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = type.Id }, _mapper.Map<RoomTypeDto>(type));
        }

        // PUT: api/roomtype/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RoomTypeDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var type = _mapper.Map<RoomType>(dto);
            _context.RoomTypes.Update(type);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<RoomTypeDto>(type));
        }

        // DELETE: api/roomtype/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var type = await _context.RoomTypes.FindAsync(id);
            if (type == null) return NotFound();

            _context.RoomTypes.Remove(type);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
