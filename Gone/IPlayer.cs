using System;

namespace Gone
{
    public interface IPlayer
    {
        Guid Id { get; set; }

        string Name { get; set; }

        IStrategy Strategy { get; set; }
    }
}