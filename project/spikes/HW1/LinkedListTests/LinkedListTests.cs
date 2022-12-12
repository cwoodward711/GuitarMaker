using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using HW1;

namespace LinkedListTests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void TestCreateList_True()
        {
            OurLinkedList testList = OurLinkedList.CreateList();
            Debug.Assert(testList != null, "testList is null, Object not created");
        }

        [TestMethod]
        public void TestListLength_3()
        {
            OurLinkedList testList = OurLinkedList.CreateList();
            testList.AddToHead(0);
            testList.AddToHead(8);
            testList.AddToHead(-3);

            Debug.Assert (testList.Length()==3);
            

        }

        [TestMethod]
        public void TestIsEmpty_true()
        {
            OurLinkedList testList = OurLinkedList.CreateList();
            Debug.Assert(testList.isEmpty());

            
        }

        [TestMethod]
        public void TestAddToHead_2()
        {
            OurLinkedList testList = OurLinkedList.CreateList();

            testList.AddToHead(0);
            testList.AddToHead(8);
            testList.AddToHead(-3);

            Debug.Assert(testList.getVal(0) == -3);
            Debug.Assert(testList.getVal(1) == 8);
            Debug.Assert(testList.getVal(2) == 0);
        }
    }
}
