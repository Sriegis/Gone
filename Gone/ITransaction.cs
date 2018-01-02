using System;

namespace Gone
{
    public interface ITransaction
    {
        Guid AttackerId { get; }
        Guid EnemyId { get; }
        int TransferResources { get; }
    }
}
