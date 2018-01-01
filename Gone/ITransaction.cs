using System;

namespace Gone
{
    public interface ITransaction
    {
        Guid AttackerId { get; set; }
        Guid EnemyId { get; set; }
        int TransferResources { get; set; }
    }
}
