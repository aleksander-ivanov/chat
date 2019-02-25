using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Entities
{
    public class UserRoom
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long RoomId { get; set; }
        public ConversationRoom Room { get; set; }
    }
}
