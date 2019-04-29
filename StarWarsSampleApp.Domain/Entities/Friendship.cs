using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsSampleApp.Domain.Entities
{
    public class Friendship
    {
        public int CharacterId { get; set; }
        public int FriendId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Character Friend { get; set; }
    }
}
