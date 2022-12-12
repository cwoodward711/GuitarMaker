using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace HW1
{
    public class OurLinkedList
    {
        private Node _Head;

        public OurLinkedList()
        {
            _Head = null;
        }


        public static void Main(string[] args)
        {
            
        }
        
        class Node {  
            public int data;  
            public Node next;  
            public Node(int d) {  
                data = d;  
                next = null;  
            }  
        }  

        public static OurLinkedList CreateList()
        {
            // LinkedList<int> list = new LinkedList<int>();
            //
            // return list;

            return new OurLinkedList();
        }

        public void AddToHead(int val)
        {
            if (_Head == null)
            {
                _Head = new Node(val);
            }
            else
            {
                Node tempHead = _Head;
                _Head = new Node(val);
                _Head.next = tempHead;
            }
        }

        public int Length()
        {
            int len = 0;
            Node curNode = _Head;
            while (curNode != null && curNode.next != null)
            {
                len++;
                curNode = curNode.next;
            }

            return len+1;
        }

        public int getVal(int index)
        {
            Node curNode = _Head;
            for (int i = 0; i < index; i++)
            {
                curNode = curNode.next;
            }

            return curNode.data;
        }

        public bool isEmpty()
        {
            return _Head == null;
        }
    }
}
