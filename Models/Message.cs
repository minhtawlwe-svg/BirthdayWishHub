using System.ComponentModel.DataAnnotations;

namespace EventMessageWall.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Sender { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? MediaPath { get; set; }
        public DateTime CreatedAt { get; set; }

        // Reactions
        public int Love { get; set; }
        public int Laugh { get; set; }
        public int Wow { get; set; }
        public int Clap { get; set; }
        public int Celebrate { get; set; }
    }
}
