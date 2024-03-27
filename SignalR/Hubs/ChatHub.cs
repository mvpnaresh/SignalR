using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using SignalR.Models;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    { 
        public async Task SendMessage(UserMessage userMessage)
        {  
            await Clients.Others.SendAsync("ReceiveMessage", userMessage);
        }
    }
}
