using System.ComponentModel.DataAnnotations;

namespace Freelancing.DTOs
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }

    public class CreateChatDto
    {
        [Required(ErrorMessage = "Receiver ID is required")]
        public string ReceiverId { get; set; }

        [MaxLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        public string Message { get; set; }

        [FileValidation(MaxFileSizeMb = 5, AllowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile Image { get; set; }
    }
}
