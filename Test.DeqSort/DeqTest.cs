using System;
using DeqSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DeqSort
{
    [TestClass]
    public class DeqTest
    {
        [TestMethod]
        public void TestEmpty()
        {
            var d = new Deq<int>();

            Assert.IsTrue(d.Empty);
        }

        [TestMethod]
        public void TestEmptyCountZero()
        {
            var d = new Deq<int>();

            Assert.IsTrue(d.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestEmptyPopBack()
        {
            var d = new Deq<int>();

            d.PopBack();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestEmptyPopFront()
        {
            var d = new Deq<int>();

            d.PopFront();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestEmptyGetBack()
        {
            var d = new Deq<int>();

            var val = d.Back;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestEmptyGetFront()
        {
            var d = new Deq<int>();

            var val = d.Front;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestEmptyGetDeqElementByPosition0()
        {
            var d = new Deq<int>();

            d.GetDeqElementByPosition(0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestEmptyGetDeqElementByPositionMinus0()
        {
            var d = new Deq<int>();

            d.GetDeqElementByPosition(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestEmptyGetDeqElementByPosition1()
        {
            var d = new Deq<int>();

            d.GetDeqElementByPosition(1);
        }

        [TestMethod]
        public void TestEmptySetDeqElementByPosition0()
        {
            const int val = 100;

            var d = new Deq<int>();

            d.SetDeqElementByPosition(0, val);

            Assert.IsTrue(d.Count == 1);
            Assert.IsFalse(d.Empty);
            Assert.AreEqual(val, d.Back);
            Assert.AreEqual(val, d.Front);
            Assert.AreEqual(val, d.GetDeqElementByPosition(0));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetDeqElementByPositionOutOfRangeNegative()
        {
            var d = new Deq<int>();

            d.SetDeqElementByPosition(0, 1);
            d.GetDeqElementByPosition(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetDeqElementByPositionOutOfRangePositive()
        {
            var d = new Deq<int>();

            d.SetDeqElementByPosition(0, 1);
            d.GetDeqElementByPosition(1);
        }

        [TestMethod]
        public void TestSetDeqElementByPositionCount()
        {
            var d = new Deq<int>();

            d.SetDeqElementByPosition(0, 1);
            d.SetDeqElementByPosition(1, 2);
            d.SetDeqElementByPosition(2, 3);

            Assert.IsFalse(d.Empty);
            Assert.AreEqual(3, d.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestEmptySetDeqElementByPositionOoRMinus1()
        {
            var d = new Deq<int>();

            d.SetDeqElementByPosition(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestEmptySetDeqElementByPositionOoR1()
        {
            var d = new Deq<int>();

            d.SetDeqElementByPosition(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSetDeqElementByPositionOoRNegative()
        {
            var d = new Deq<int>();
            d.SetDeqElementByPosition(0, 1);
            d.SetDeqElementByPosition(-1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSetDeqElementByPositionOoRPositive()
        {
            var d = new Deq<int>();
            d.SetDeqElementByPosition(0, 1);
            d.SetDeqElementByPosition(2, 2);
        }

        [TestMethod]
       
        public void TestPushBack()
        {
            const int val = 100500;
            var d = new Deq<int>();

            d.PushBack(val);
            Assert.IsFalse(d.Empty);
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(val, d.Back);
            Assert.AreEqual(val, d.Front);
        }

        [TestMethod]
        public void TestPushFront()
        {
            const int val = 100501;
            var d = new Deq<int>();

            d.PushFront(val);
            Assert.IsFalse(d.Empty);
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(val, d.Back);
            Assert.AreEqual(val, d.Front);
        }

        [TestMethod]
        public void TestPushFrontPopBackOne()
        {
            const int val = 100502;
            var d = new Deq<int>();

            d.PushFront(val);
            Assert.AreEqual(val, d.PopBack());
            Assert.IsTrue(d.Empty);
            Assert.AreEqual(0, d.Count);
        }

        [TestMethod]
        public void TestPushBackPopFrontOne()
        {
            const int val = 100503;
            var d = new Deq<int>();

            d.PushBack(val);
            Assert.AreEqual(val, d.PopFront());
            Assert.IsTrue(d.Empty);
            Assert.AreEqual(0, d.Count);
        }

        [TestMethod]
        public void TestPushBack1To4()
        {
            var d = new Deq<int>();

            d.PushBack(1);
            d.PushBack(2);
            d.PushBack(3);
            d.PushBack(4);
            
            Assert.IsFalse(d.Empty);
            Assert.AreEqual(4, d.Count);
        }

        [TestMethod]
        public void TestPushFront1To4()
        {
            var d = new Deq<int>();

            d.PushFront(1);
            d.PushFront(2);
            d.PushFront(3);
            d.PushFront(4);

            Assert.IsFalse(d.Empty);
            Assert.AreEqual(4, d.Count);
        }


        [TestMethod]
        public void TestOrderPushFrontPopBack()
        {
            var d = new Deq<int>();

            d.PushFront(1);
            d.PushFront(2);
            d.PushFront(3);
            d.PushFront(4);

            Assert.AreEqual(1, d.PopBack());
            Assert.AreEqual(2, d.PopBack());
            Assert.AreEqual(3, d.PopBack());
            Assert.AreEqual(4, d.PopBack());
        }

        [TestMethod]
        public void TestOrderPushFrontPopFront()
        {
            var d = new Deq<int>();

            d.PushFront(1);
            d.PushFront(2);
            d.PushFront(3);
            d.PushFront(4);

            Assert.AreEqual(4, d.PopFront());
            Assert.AreEqual(3, d.PopFront());
            Assert.AreEqual(2, d.PopFront());
            Assert.AreEqual(1, d.PopFront());
        }

        [TestMethod]
        public void TestOrderPushBackPopFront()
        {
            var d = new Deq<int>();

            d.PushBack(1);
            d.PushBack(2);
            d.PushBack(3);
            d.PushBack(4);

            Assert.AreEqual(1, d.PopFront());
            Assert.AreEqual(2, d.PopFront());
            Assert.AreEqual(3, d.PopFront());
            Assert.AreEqual(4, d.PopFront());
        }


        [TestMethod]
        public void TestOrderPushBackPopBack()
        {
            var d = new Deq<int>();

            d.PushBack(1);
            d.PushBack(2);
            d.PushBack(3);
            d.PushBack(4);

            Assert.AreEqual(4, d.PopBack());
            Assert.AreEqual(3, d.PopBack());
            Assert.AreEqual(2, d.PopBack());
            Assert.AreEqual(1, d.PopBack());
        }
    }
}
