using Freelancing.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace Freelancing.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext context;

        public ChatHub(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task SendMessage(ChatDto chatDto)
        {
                await Clients.Users(chatDto.SenderId,chatDto.ReceiverId).SendAsync("ReceiveMessage",chatDto);
        }

        public async override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;


            var existingConnection = await context.UserConnections
        .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConnectionId == connectionId);


            if (existingConnection == null) {
                var userConnection = new UserConnection
                {
                    UserId = userId,
                    ConnectionId = connectionId,
                    ConnectedAt = DateTime.UtcNow,
                    IsConnected = true
                };
            context.UserConnections.Add(userConnection);
            await context.SaveChangesAsync();
            }

            // Add to group for user-specific messaging
            await Groups.AddToGroupAsync(connectionId, userId); //userId == groupName

            // Notify clients about online status
            await Clients.All.SendAsync("UserStatus", userId, true);
            await base.OnConnectedAsync();

        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;
    
            var userConnections = await context.UserConnections
                .Where(uc => uc.ConnectionId == connectionId && uc.UserId == userId).ToListAsync();
            if (userConnections.Any())
            {
                foreach (var userConnection in userConnections)
                {
                    userConnection.IsConnected = false;
                    context.UserConnections.Update(userConnection);
                }
                await context.SaveChangesAsync();
            }

            await Clients.All.SendAsync("UserStatus", userId, false);
            await Groups.RemoveFromGroupAsync(connectionId, userId);

            await base.OnDisconnectedAsync(exception);
        }
    }
    
}
