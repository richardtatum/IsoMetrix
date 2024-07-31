
namespace IsoMetrix.LinkedList.Core;

public class Node<T>(T data)
{
    public T Data { get; set; } = data;
    public Node<T>? Next { get; set; }
    public Node<T>? Previous { get; set; }
    
    internal CustomLinkedList<T>? List { get; set; }

    internal void Invalidate()
    {
        List = null;
        Next = null;
        Previous = null;
    }
}