namespace Freelancing.DTOs
{
    public class CreateChatDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
 