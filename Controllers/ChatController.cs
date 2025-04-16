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
        private readonly ApplicationDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public ChatController(
            IChatRepositoryService chatRepository,
            IMapper mapper,
            IHubContext<ChatHub> hubContext,
            ApplicationDbContext context,
            CloudinaryService cloudinaryService)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _context = context;
            _cloudinaryService = cloudinaryService;
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
            var chats = await _chatRepository.GetConversationAsync(userId1, userId2);
            if (chats == null || !chats.Any())
            {
                return NotFound();
            }
            var chatDtos = _mapper.Map<List<ChatDto>>(chats);
            return Ok(chatDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto createChatDto)
        {
            if (createChatDto.Image == "")
            {
                createChatDto.Image = null;
            }

            try
            {
                if (string.IsNullOrEmpty(createChatDto.Message) && string.IsNullOrEmpty(createChatDto.Image))
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
                if (!string.IsNullOrEmpty(createChatDto.Image))
                {
                    try
                    {
                        imageUrl = await _cloudinaryService.UploadBase64ImageAsync(createChatDto.Image);
                    }
                    catch
                    {
                        return BadRequest(new { error = "Invalid image format." });
                    }
                }

                var chat = _mapper.Map<Chat>(createChatDto);
                chat.SenderId = senderId;
                chat.SentAt = DateTime.UtcNow;
                chat.isRead = false;
                chat.ImageUrl = imageUrl;

                var createdChat = await _chatRepository.CreateChatAsync(chat);
                var chatDto = _mapper.Map<ChatDto>(createdChat);

                await _hubContext.Clients.Users(chatDto.SenderId, chatDto.ReceiverId)
                    .SendAsync("ReceiveMessage", chatDto);

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
