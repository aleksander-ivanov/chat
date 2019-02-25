using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chat.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Chat.Data;

namespace Chat.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ConversationRoom> Room { get;set; }

        public async Task OnGetAsync()
        {
            Room = await _context.Rooms.Include(x => x.CreatedBy).ToListAsync();
        }
    }
}
