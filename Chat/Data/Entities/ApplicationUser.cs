using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Chat.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ChatMessage> Messages { get; set; }
        public virtual ICollection<UserRoom> UserRooms { get; set; }
    }
}
