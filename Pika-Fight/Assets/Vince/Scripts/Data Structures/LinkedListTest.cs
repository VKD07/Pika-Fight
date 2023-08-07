using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListTest : MonoBehaviour
{
    CustomLinkedList<int> list;
    void Start()
    {
        list = new CustomLinkedList<int>();
        Node<int> a = new Node<int>(1);
        list.AddLast(a);
        Node<int> b = new Node<int>(2);
        list.AddLast(b);
        Node<int> c = new Node<int>(3);
        list.AddLast(c);
        list.RemoveFirst();
        list.ShowList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
