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
    [Authorize(Roles = "Admin")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IMapper _mapper;

        public RoomTypeController(IRoomTypeService roomTypeService, IMapper mapper)
        {
            _roomTypeService = roomTypeService;
            _mapper = mapper;
        }

        // GET: api/roomtype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeDto>>> GetAll()
        {
            var types = await _roomTypeService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RoomTypeDto>>(types));
        }

        // GET: api/roomtype/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeDto>> GetById(Guid id)
        {
            var type = await _roomTypeService.GetByIdAsync(id);
            if (type == null) return NotFound();

            return Ok(_mapper.Map<RoomTypeDto>(type));
        }

        // POST: api/roomtype
        [HttpPost]
        public async Task<ActionResult<RoomTypeDto>> Create(RoomTypeCreateDto dto)
        {
            var entity = _mapper.Map<RoomType>(dto);
            var created = await _roomTypeService.CreateAsync(entity);
            var resultDto = _mapper.Map<RoomTypeDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        // PUT: api/roomtype/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RoomTypeDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var entity = _mapper.Map<RoomType>(dto);
            var updated = await _roomTypeService.UpdateAsync(entity);
            return Ok(_mapper.Map<RoomTypeDto>(updated));
        }

        // DELETE: api/roomtype/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _roomTypeService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
