using System.ComponentModel.DataAnnotations;

namespace EventMessageWall.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        public int MessageId { get; set; }        // Original wish ID
        public int? ParentReplyId { get; set; }   // Null = top-level reply, otherwise nested

        public string Sender { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
