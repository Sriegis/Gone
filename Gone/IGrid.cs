using System.Collections.Generic;

namespace Gone
{
    public interface IGrid
    {
        IEnumerable<ICell> Cells { get; set; }

        Grid InitializeWith(IEnumerable<Player> players);

        void ProcessTransactions(List<Transaction> transactions);

        MyCell[] GetPlayerCells(Player player);
    }
}