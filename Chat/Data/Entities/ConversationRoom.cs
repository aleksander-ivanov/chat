using System.Collections;
using System.Collections.Generic;

namespace Chat.Data.Entities
{
    public class ConversationRoom : BaseEntity<long>
    {
        public ConversationRoom()
        {
            UserRooms = new HashSet<UserRoom>();
            Messages = new HashSet<ChatMessage>();
        }
        public string RoomName { get; set; }
        public virtual ICollection<UserRoom> UserRooms { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ApplicationUser LastModifiedBy { get; set; }
    }
}
