using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsSampleApp.Domain.Entities
{
    public class Character
    {
        public Character()
        {
            Friends = new HashSet<Friendship>();
            Episodes = new HashSet<Episode>();
        }

        public int Id { get; set; }
        public int Name { get; set; }

        public ICollection<Friendship> Friends { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}
