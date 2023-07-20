using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName ="StackReference", menuName = "References/Stack")]
public class StackReference : ScriptableObject
{
   public Stack<Image>obj;

    public void Push(Image obj)
    {
        this.obj.Push(obj);
    }

    public Image Pop()
    {
        return this.obj.Pop();
    }
}
