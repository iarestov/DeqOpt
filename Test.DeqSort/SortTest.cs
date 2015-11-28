using System;
using DeqSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DeqSort
{
    [TestClass]
    public class SortTest
    {
        [TestMethod]
        public void TestEmpty()
        {
            var d = new Deq<int>();
            Sorter.Sort(d);
            Assert.IsTrue(d.Empty);
            Assert.AreEqual(0, d.Count);
        }

        [TestMethod]
        public void TestOneItem()
        {
            const int val = 100500;
            var d = new Deq<int>();
            d.PushBack(val);
            Sorter.Sort(d);
            Assert.IsFalse(d.Empty);

            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(val, d.PopBack());
        }

        [TestMethod]
        public void TestTwoItems()
        {
            const int val1 = 100500;
            const int val2 = 100600;
            var d = new Deq<int>();
            d.PushBack(val1);
            d.PushBack(val2);
            d = Sorter.Sort(d);
            Assert.IsFalse(d.Empty);

            Assert.AreEqual(2, d.Count);
            Assert.AreEqual(val1, d.PopFront());
            Assert.AreEqual(val2, d.PopFront());
        }
        
    }
}
