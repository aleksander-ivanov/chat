using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chat.Data;
using Chat.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Chat.Pages
{
    [Authorize]
    public class MessangerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MessangerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ChatMessage> Messages { get;set; }
        public ConversationRoom Room { get;set; }

        public async Task OnGetAsync(long roomId)
        {
            Messages = await _context.Messages.Where(x => x.RoomId == roomId)
                .Include(c => c.Room)
                .Include(c => c.CreatedBy)
                .OrderByDescending(x => x.CreatedDate).ToListAsync();

            Room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
        }
    }
}
