using Freelancing.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Freelancing.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatDto chatDto)
        {
                await Clients.Users(chatDto.SenderId,chatDto.ReceiverId).SendAsync("ReceiveMessage",chatDto);
        }

        public async override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            var userConnection = new UserConnection
            {
                UserId = userId,
                ConnectionId = connectionId,
                ConnectedAt = DateTime.UtcNow,
                IsConnected = true

            };

        }

        
    }
    
}
