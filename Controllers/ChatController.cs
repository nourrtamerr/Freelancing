using System.Security.Claims;
using AutoMapper;
using Freelancing.DTOs;
using Freelancing.Services;
using Freelancing.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepositoryService _chatRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IHubContext<NotificationHub> _NotifhubContext;
        private readonly ApplicationDbContext _context;
        private readonly CloudinaryService _cloudinaryService;
        private readonly UserManager<AppUser> _usermanager;
        private readonly INotificationRepositoryService _notifserivcee;

		public ChatController(
            IChatRepositoryService chatRepository,
            IMapper mapper,
            IHubContext<ChatHub> hubContext,
            IHubContext<NotificationHub> NotifhubContext,
            ApplicationDbContext context,
            CloudinaryService cloudinaryService,
            UserManager<AppUser> usermanager,
            INotificationRepositoryService notifserivcee)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _context = context;
            _cloudinaryService = cloudinaryService;
            _usermanager = usermanager;
            _NotifhubContext = NotifhubContext;
            _notifserivcee = notifserivcee;
		}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(int id)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            var chatDto = _mapper.Map<ChatDto>(chat);
            return Ok(chatDto);
        }

        [HttpGet("conversation/{userId1}/{userId2}")]
        public async Task<IActionResult> GetConversation(string userId1, string userId2)
        {
			var user1 = await _usermanager.FindByNameAsync(userId1);
			var user2 = await _usermanager.FindByNameAsync(userId2);
            if(user1==null||user2==null)
            {
                return BadRequest(new { error = "invalidusers" });
            }
			var chats = await _chatRepository.GetConversationAsync(user1.Id, user2.Id);
            if (chats == null || !chats.Any())
            {
                return NotFound();
            }
            var chatDtos = _mapper.Map<List<ChatDto>>(chats);
            return Ok(chatDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromForm] CreateChatDto createChatDto)
        {
            //if (createChatDto.Image == "")
            //{
            //    createChatDto.Image = null;
            //}

            try
            {
                if (string.IsNullOrEmpty(createChatDto.Message) && createChatDto.Image==null)
                {
                    return BadRequest(new { error = "Either a message or an image is required." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(senderId))
                {
                    return Unauthorized("User ID could not be determined from token.");
                }

                string imageUrl = null;
                if (createChatDto.Image!=null)
                {
                    try
                    {
                        imageUrl = createChatDto.Image.Save();
                    }
                    catch
                    {
                        return BadRequest(new { error = "Invalid image format." });
                    }
                }
                var user = await _usermanager.FindByNameAsync(createChatDto.ReceiverId);
                if(user is null)
                {
                    return BadRequest(new { error = "user not found" });
                }    

                var chat = _mapper.Map<Chat>(createChatDto);
                chat.SenderId = senderId;
                chat.ReceiverId = user.Id;
                chat.SentAt = DateTime.UtcNow;
                chat.isRead = false;
                chat.ImageUrl = imageUrl;

                var createdChat = await _chatRepository.CreateChatAsync(chat);
                var chatDto = _mapper.Map<ChatDto>(createdChat);

                await _hubContext.Clients.Users(chatDto.SenderId, chatDto.ReceiverId)
                    .SendAsync("ReceiveMessage", chatDto);

                var name=(await _usermanager.FindByIdAsync(chatDto.SenderId))?.UserName;
                await _notifserivcee.CreateNotificationAsync(new()
                {
                    isRead = false,
                    Message = $"You received a message from {name ?? "undefined user"}",
                    UserId = chatDto.ReceiverId,
                });
				



				return CreatedAtAction(nameof(GetChat), new { id = chatDto.Id }, chatDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error: " + ex.Message });
            }
        }

        [HttpPut("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            await _chatRepository.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(chat.ImageUrl))
            {
                var publicId = _cloudinaryService.ExtractPublicId(chat.ImageUrl);
                if (!string.IsNullOrEmpty(publicId))
                {
                    await _cloudinaryService.DeleteImageAsync(publicId);
                }
            }

            await _chatRepository.DeleteChatAsync(id);
            return NoContent();
        }

        [HttpGet("online-users")]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var onlineUsers = await _context.UserConnections
                .Where(uc => uc.IsConnected)
                .Include(uc => uc.User)
                .Select(uc => new
                {
                    UserId = uc.UserId,
                    UserName = uc.User.UserName
                })
                .Distinct()
                .ToListAsync();

            return Ok(onlineUsers);
        }

        [HttpGet("conversations/{username}")]
        public async Task<IActionResult> GetConversations(string username)
        {
            // Find the user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return NotFound($"User {username} not found");
            }

            // Get all messages where the user is either the sender or receiver
            var messages = await _context.Chats
                .Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();

            // Group messages by the other participant's ID and take the latest message
            var conversations = messages
                .GroupBy(m => m.SenderId == user.Id ? m.ReceiverId : m.SenderId)
                .Select(g => g.OrderByDescending(m => m.SentAt).First())
                .Select(m => _mapper.Map<ChatDto>(m)) // Map to ChatDto
                .ToList();

            return Ok(conversations);
        }
    }
}
