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

		public ChatController(
            IChatRepositoryService chatRepository,
            IMapper mapper,
            IHubContext<ChatHub> hubContext,
            IHubContext<NotificationHub> NotifhubContext,
            ApplicationDbContext context,
            CloudinaryService cloudinaryService,
            UserManager<AppUser> usermanager)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _context = context;
            _cloudinaryService = cloudinaryService;
            _usermanager = usermanager;
            _NotifhubContext = NotifhubContext;
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
				await _NotifhubContext.Clients.Users(chatDto.ReceiverId).SendAsync("ReceiveNotification", 
                    new NotificationDto()
                    {
                        Id=1,
                        IsRead=false,
                        Message=chatDto.Message+"has been sent to you",
                        UserId=chatDto.ReceiverId
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
    }
}
