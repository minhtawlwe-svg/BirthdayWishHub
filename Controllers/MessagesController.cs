using EventMessageWall.Data;
using EventMessageWall.Models;
using EventMessageWall.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EventMessageWall.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<MessageHub> _hub;

        public MessagesController(AppDbContext db, IHubContext<MessageHub> hub)
        {
            _db = db;
            _hub = hub;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Messages.OrderByDescending(m => m.CreatedAt));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] string sender, [FromForm] string text, [FromForm] IFormFile? media)
        {
            string? mediaPath = null;
            if (media != null)
            {
                var uploads = Path.Combine("wwwroot", "media");
                Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(media.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await media.CopyToAsync(stream);

                mediaPath = "/media/" + fileName;
            }

            var msg = new Message
            {
                Sender = sender,
                Text = text,
                MediaPath = mediaPath,
                CreatedAt = DateTime.UtcNow
            };

            _db.Messages.Add(msg);
            _db.SaveChanges();

            await _hub.Clients.All.SendAsync("ReceiveUpdate");
            return Ok(msg);
        }

        [HttpPost("{id}/react/{type}")]
        public async Task<IActionResult> React(int id, string type)
        {
            var msg = _db.Messages.Find(id);
            if (msg == null) return NotFound();

            switch (type)
            {
                case "love": msg.Love++; break;
                case "laugh": msg.Laugh++; break;
                case "wow": msg.Wow++; break;
                case "clap": msg.Clap++; break;
                case "celebrate": msg.Celebrate++; break;
                default: return BadRequest();
            }

            _db.SaveChanges();
            await _hub.Clients.All.SendAsync("ReceiveUpdate");
            return Ok();
        }
    }
}
