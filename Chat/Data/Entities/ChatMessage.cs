﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Entities
{
    public class ChatMessage: BaseEntity<long>
    {
        public string Text { get; set; }
        public long RoomId { get; set; }
        public ConversationRoom Room { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
    }
}
