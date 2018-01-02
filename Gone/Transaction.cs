using System;

namespace Gone
{
    public class Transaction : ITransaction
    {
        public Guid AttackerId { get; }
        public Guid EnemyId { get; }
        public int TransferResources { get; }

        public Transaction(Guid attackerId, Guid enemyId, int transferResources)
        {
            AttackerId = attackerId;
            EnemyId = enemyId;
            TransferResources = transferResources;
        }
    }
}
