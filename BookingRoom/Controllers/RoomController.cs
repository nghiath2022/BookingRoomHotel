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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        // GET: api/room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            var roomDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);
            return Ok(roomDtos);
        }

        // GET: api/room/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoomById(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            var roomDto = _mapper.Map<RoomDto>(room);
            return Ok(roomDto);
        }

        // POST: api/room
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] RoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            var createdRoom = await _roomService.CreateRoomAsync(room);
            var roomDto = _mapper.Map<RoomDto>(createdRoom);
            return CreatedAtAction(nameof(GetRoomById), new { id = roomDto.Id }, roomDto);
        }

        // PUT: api/room/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var room = _mapper.Map<Room>(dto);
            var updatedRoom = await _roomService.UpdateRoomAsync(room);
            return Ok(_mapper.Map<RoomDto>(updatedRoom));
        }

        // DELETE: api/room/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
