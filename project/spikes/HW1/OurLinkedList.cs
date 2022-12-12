using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace HW1
{
    public class OurLinkedList
    {
        private Node head;

        public OurLinkedList()
        {
            head = null;
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
            if (head == null)
            {
                head = new Node(val);
            }
            else
            {
                Node tempHead = head;
                head = new Node(val);
                head.next = tempHead;
            }
        }

        public int Length()
        {
            int len = 0;
            Node curNode = head;
            while (curNode != null && curNode.next != null)
            {
                len++;
                curNode = curNode.next;
            }

            return len+1;
        }

        public int getVal(int index)
        {
            Node curNode = head;
            for (int i = 0; i < index; i++)
            {
                curNode = curNode.next;
            }

            return curNode.data;
        }

        public bool isEmpty()
        {
            return head == null;
        }
    }
}
