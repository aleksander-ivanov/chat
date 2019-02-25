using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chat.Data;
using Chat.Data.Entities;

namespace Chat.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Chat.Data.ApplicationDbContext _context;

        public DeleteModel(Chat.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ConversationRoom Room { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.Include(x => x.CreatedBy).FirstOrDefaultAsync(m => m.Id == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.FindAsync(id);

            if (Room != null)
            {
                _context.Rooms.Remove(Room);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
