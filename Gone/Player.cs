using System;

namespace Gone
{
    public class Player : IPlayer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IStrategy Strategy { get; set; }
    }
}