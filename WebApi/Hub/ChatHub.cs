using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hub
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.ReceiveMessage(message);
            //Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
