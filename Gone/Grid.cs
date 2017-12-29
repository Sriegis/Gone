using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gone
{
    public class Grid : IGrid
    {
        public IEnumerable<ICell> Cells { get; set; }
        public void Iterate(List<Transaction> transactions)
        {
            
        }
    }

    public interface IGrid
    {
        IEnumerable<ICell> Cells { get; set; }
        void Iterate(List<Transaction> transactions);
    }

    public class Transaction : ITransaction
    {

    }

    public interface ITransaction
    {
        
    }

    public interface ICell
    {
        int Resources { get; set; }
    }

    public class MyCell : ICell
    {
        public int Resources { get; set; }
    }

    public class NeighbourCell : ICell
    {
        public int Resources { get; set; }
    }
}
