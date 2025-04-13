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
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await base.OnConnectedAsync();
        }
    }
    
}
