using Chat.Data;
using Chat.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(long roomId, string text)
        {
            var newMessage = new ChatMessage { Text = text, RoomId = roomId };
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            var message = await _context.Messages.Where(x => x.Id == newMessage.Id)
                .Include(c => c.Room)
                .Include(c => c.CreatedBy)
                .FirstOrDefaultAsync();

            var msg = new { text = message.Text, createdBy = message.CreatedBy.Email, createdDate = message.CreatedDate };
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", msg);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var roomId = httpContext.Request.Query["roomId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await base.OnConnectedAsync();

            await Clients.GroupExcept(roomId.ToString(), Context.ConnectionId).SendAsync("newUserConnected", $"User {Context.User.Identity.Name} connected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var roomId = httpContext.Request.Query["roomId"];

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
