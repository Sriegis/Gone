using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Moq;

namespace Gone.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void Grid_Should_Iterate()
        {
            var grid = new Mock<IGrid>();
            var transactions = new List<Transaction>();
            //grid.Iterate(transactions);
            grid.Verify(x => x.Iterate(It.IsAny<List<Transaction>>()));
        }
    }
}