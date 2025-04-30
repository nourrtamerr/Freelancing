using System.Security.Claims;
using Freelancing.DTOs;
using Freelancing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public ChatHub(ApplicationDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task SendMessage(ChatDto chatDto)
        {
			//var senderId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
			//  ?? Context.User.FindFirst("sub")?.Value;
			var senderId = Context.UserIdentifier;

			if (chatDto == null || string.IsNullOrEmpty(chatDto.ReceiverId))
            {
                throw new HubException("Invalid message data");
            }

            await Clients.Users(senderId, chatDto.ReceiverId)
                .SendAsync("ReceiveMessage", chatDto);
        }

        public override async Task OnConnectedAsync()
        {


			var userId = Context.UserIdentifier;
			Console.WriteLine($"Connected userId: {userId}");

            var connectionId = Context.ConnectionId;

            var existingConnection = await _context.UserConnections
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConnectionId == connectionId);

            if (existingConnection == null)
            {
                var userConnection = new UserConnection
                {
                    UserId = userId,
                    ConnectionId = connectionId,
                    ConnectedAt = DateTime.UtcNow,
                    IsConnected = true
                };
                _context.UserConnections.Add(userConnection);
                await _context.SaveChangesAsync();
            }

            await Clients.All.SendAsync("UserStatus", userId, true);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            var userConnections = await _context.UserConnections
                .Where(uc => uc.ConnectionId == connectionId && uc.UserId == userId)
                .ToListAsync();

            if (userConnections.Any())
            {
                foreach (var userConnection in userConnections)
                {
                    userConnection.IsConnected = false;
                    _context.UserConnections.Update(userConnection);
                }
                await _context.SaveChangesAsync();
            }

            await Clients.All.SendAsync("UserStatus", userId, false);
            await base.OnDisconnectedAsync(exception);
        }
    }
	public class CustomUserIdProvider : IUserIdProvider
	{
		public string GetUserId(HubConnectionContext connection)
		{
			// Assuming your JWT uses the NameIdentifier (sub or userId) claim
			return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}