using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mod09api.Models;

namespace mod09apiv7.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MeetingRoomsController : ControllerBase
    {
        private readonly MeetingRoomContext _context;

        public MeetingRoomsController(MeetingRoomContext context)
        {
            _context = context;
        }

        // GET: api/MeetingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingRoom>>> GetMeetingRooms()
        {
          if (_context.MeetingRooms == null)
          {
              return NotFound();
          }
            return await _context.MeetingRooms.ToListAsync();
        }

        // GET: api/MeetingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingRoom>> GetMeetingRoom(int id)
        {
          if (_context.MeetingRooms == null)
          {
              return NotFound();
          }
            var meetingRoom = await _context.MeetingRooms.FindAsync(id);

            if (meetingRoom == null)
            {
                return NotFound();
            }

            return meetingRoom;
        }

        // PUT: api/MeetingRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetingRoom(int id, MeetingRoom meetingRoom)
        {
            if (id != meetingRoom.ID)
            {
                return BadRequest();
            }

            _context.Entry(meetingRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingRoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MeetingRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeetingRoom>> PostMeetingRoom(MeetingRoom meetingRoom)
        {
          if (_context.MeetingRooms == null)
          {
              return Problem("Entity set 'MeetingRoomContext.MeetingRooms'  is null.");
          }
            _context.MeetingRooms.Add(meetingRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetingRoom", new { id = meetingRoom.ID }, meetingRoom);
        }

        // DELETE: api/MeetingRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingRoom(int id)
        {
            if (_context.MeetingRooms == null)
            {
                return NotFound();
            }
            var meetingRoom = await _context.MeetingRooms.FindAsync(id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            _context.MeetingRooms.Remove(meetingRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeetingRoomExists(int id)
        {
            return (_context.MeetingRooms?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
