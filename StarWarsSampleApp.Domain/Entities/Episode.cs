using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsSampleApp.Domain.Entities
{
    public class Episode
    {
        public Episode()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
