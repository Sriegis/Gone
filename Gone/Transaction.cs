using System;

namespace Gone
{
    public class Transaction : ITransaction
    {
        public Guid AttackerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid EnemyId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TransferResources { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
