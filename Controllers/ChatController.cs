using AutoMapper;
using Freelancing.DTOs;
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
        private readonly IChatRepositoryService chatRepository;
        private readonly IMapper mapper;
        private readonly IHubContext<ChatHub> hubContext;
        private readonly ApplicationDbContext context;

        public ChatController(IChatRepositoryService chatRepository,IMapper mapper ,IHubContext<ChatHub> hubContext , ApplicationDbContext context)
        {
            this.chatRepository = chatRepository;
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(int id)
        { 
            var chat = await chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            var chatDto = mapper.Map<ChatDto>(chat);
            return Ok(chatDto);
        }

        [HttpGet("conversation/{userId1}/{userId2}")]
        public async Task<IActionResult> GetConversation(string userId1, string userId2)
        {

            var chats = await chatRepository.GetConversationAsync(userId1, userId2);
            if (chats == null || chats.Count == 0)
            {
                return NotFound();
            }
            var chatDtos = mapper.Map<ChatDto>(chats);
            return Ok(chatDtos);

        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto createChatDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var chat = mapper.Map<Chat>(createChatDto);
                var createdChat = await chatRepository.CreateChatAsync(chat);

                var chatDto = mapper.Map<ChatDto>(createdChat);

                // Broadcast to SignalR hub
                await hubContext.Clients.Users(chatDto.SenderId, chatDto.ReceiverId)
                    .SendAsync("ReceiveMessage", chatDto);

                return CreatedAtAction(nameof(GetChat), new { id = chatDto.Id }, chatDto);
            }
            catch (Exception ex)
            {
                // Log the error (use your logging framework, e.g., Serilog or Microsoft.Extensions.Logging)
                Console.WriteLine($"Error creating chat: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var chat = await chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            await chatRepository.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chat = await chatRepository.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            await chatRepository.DeleteChatAsync(id);
            return NoContent();
        }

        [HttpGet("online-users")]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var onlineUsers = await context.UserConnections
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
