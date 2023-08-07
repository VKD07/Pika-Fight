using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomLinkedList<T>
{
    public Node<T> first;
    public Node<T> last;
    public int count;

    public void AddFirst(Node<T> newNode)
    {
        if (first == null)
        {
            first = newNode;
            last = newNode;
        }
        else
        {
            newNode.next = first;
            first = newNode;
        }
        count++;
    }

    public void AddLast(Node<T> newNode)
    {
        if (first == null)
        {
            first = newNode;
            last = newNode;
        }
        else
        {
            last.next = newNode;
            last = newNode;
        }
        count++;
    }

    public Node<T> Find(T nodeData)
    {
        Node<T> currentNode = first;

        while (currentNode != null && !currentNode.data.Equals(nodeData))
        {
            currentNode = currentNode.next;
        }
        return currentNode;
    }

    public void RemoveFirst()
    {
        if (first != null || count > 0)
        {
            first = first.next;
            count--;
        }
    }

    public void Remove(Node<T> node)
    {
        if (first == null || count == 0)
        {
            Debug.Log("List is Empty");
            return;
        }

        if (first == node)
        {
            RemoveFirst();
            return;
        }

        Node<T> previous = first;
        Node<T> current = previous.next;

        while (current != null && current != node)
        {
            previous = current;
            current = previous.next;
        }

        if (current != null)
        {
            previous.next = current.next;
            count--;
        }
    }

    public void ShowList()
    {
        Debug.Log($"First: {first.data}");
        Debug.Log($"Last: {last.data}");

        Node<T> node = first;
        while (node.next != null)
        {
            Debug.Log(node.data);
            node = node.next;
        }
        Debug.Log(node.data);
    }
}
