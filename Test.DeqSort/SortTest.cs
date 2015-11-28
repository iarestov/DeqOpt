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
            Sorter.Sort2(d);
            Assert.IsTrue(d.Empty);
            Assert.AreEqual(0, d.Count);
        }

        [TestMethod]
        public void TestOneItem()
        {
            const int val = 100500;
            var d = new Deq<int>();
            d.PushBack(val);
            Sorter.Sort2(d);
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
            d = Sorter.Sort2(d);

            Assert.IsFalse(d.Empty);
            Assert.AreEqual(2, d.Count);
            Assert.AreEqual(val1, d.PopFront());
            Assert.AreEqual(val2, d.PopFront());
        }


        [TestMethod]
        public void TestThreeItems()
        {
            const int val1 = 100500;
            const int val2 = 100600;
            const int val3 = 100700;
            var d = new Deq<int>();
            d.PushBack(val2);
            d.PushBack(val3);
            d.PushBack(val1);
            d = Sorter.Sort2(d);

            Assert.IsFalse(d.Empty);
            Assert.AreEqual(3, d.Count);
            Assert.AreEqual(val1, d.PopFront());
            Assert.AreEqual(val2, d.PopFront());
            Assert.AreEqual(val3, d.PopFront());
        }


        [TestMethod]
        public void TestSortMoreItems()
        {
            int[] data = new[] {100500, 100600, 1, 1999, 200500, 1000, 100, 5};
            
            var d = new Deq<int>();
            foreach (var v in data) {
                d.PushBack(v);
            }

            d = Sorter.Sort2(d);

            Assert.IsFalse(d.Empty);
            Assert.AreEqual(data.Length, d.Count);

            Array.Sort(data);

            foreach (var v in data) {
                Assert.AreEqual(v, d.PopFront());
            }
        }


        [TestMethod]
        public void TestSortSortedMoreItems()
        {
            int[] data = new[] { 100500, 100600, 1, 1999, 200500, 1000, 100, 5 };
            Array.Sort(data);

            var d = new Deq<int>();
            foreach (var v in data)
            {
                d.PushBack(v);
            }

            d = Sorter.Sort2(d);

            Assert.IsFalse(d.Empty);
            Assert.AreEqual(data.Length, d.Count);

            foreach (var v in data)
            {
                Assert.AreEqual(v, d.PopFront());
            }
        }
        
    }
}
