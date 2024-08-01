namespace IsoMetrix.LinkedList.Core;

public class CustomLinkedList<T>
{
    public Node<T>? First { get; private set; }
    public Node<T>? Last { get; private set; }
    public int Count { get; private set; }

    public event Action<Node<T>>? OnPrint;

    public void InsertFirst(Node<T> node)
    {
        if (First is null)
        {
            First = node;
            Last = node;
        }
        else
        {
            First.Previous = node;
            node.Next = First;
            First = node;
        }

        node.List = this;
        Count++;
    }

    public void InsertLast(Node<T> node)
    {
        if (Last is null)
        {
            First = node;
            Last = node;
        }
        else
        {
            Last.Next = node;
            node.Previous = Last;
            Last = node;
        }

        node.List = this;
        Count++;
    }

    public void InsertBefore(Node<T> existingNode, Node<T> newNode)
    {
        ValidateNode(existingNode);

        if (First == existingNode)
        {
            First = newNode;
        }
        else
        {
            // If it's not first, the previous node needs updating with the new next ref
            existingNode.Previous!.Next = newNode;
        }

        newNode.Previous = existingNode.Previous;
        newNode.Next = existingNode;
        existingNode.Previous = newNode;

        Count++;
    }

    public void InsertAfter(Node<T> existingNode, Node<T> newNode)
    {
        ValidateNode(existingNode);

        if (Last == existingNode)
        {
            Last = newNode;
        }
        else
        {
            // If it's not last, the next node needs updating with new prev ref
            existingNode.Next!.Previous = newNode;
        }

        newNode.Next = existingNode.Next;
        newNode.Previous = existingNode;
        existingNode.Next = newNode;

        Count++;
    }

    public void DeleteFirst()
    {
        if (First is null)
        {
            throw new InvalidOperationException(LinkedListIsEmptyExceptionMessage);
        }
        
        Delete(First);
    }

    public void DeleteLast()
    {
        if (Last is null)
        {
            throw new InvalidOperationException(LinkedListIsEmptyExceptionMessage);
        }
        
        Delete(Last);
    }

    public void Delete(Node<T> node)
    {
        ValidateNode(node);

        if (node == First)
        {
            First = node.Next;
        }

        if (node == Last)
        {
            Last = node.Previous;
        }

        if (node.Next != null)
        {
            node.Next.Previous = node.Previous;
        }

        if (node.Previous != null)
        {
            node.Previous.Next = node.Next;
        }
        
        node.Invalidate();
        Count--;
    }

    public void PrintList()
    {
        EnsureDefaultPrint();

        if (First is null)
        {
            throw new InvalidOperationException(LinkedListIsEmptyExceptionMessage);
        }

        var current = First;
        while (current is not null)
        {
            OnPrint!.Invoke(current);
            current = current.Next;
        }
    }

    private void EnsureDefaultPrint()
    {
        if (OnPrint is not null)
        {
            return;
        }

        OnPrint += node =>
        {
            Console.WriteLine(node.Data?.ToString() ?? "No Data");
        };
    }

    private void ValidateNode(Node<T> node)
    {
        if (node.List == this)
        {
            return;
        }

        throw new InvalidOperationException(NodeDoesNotBelongExceptionMessage);
    }

    private string LinkedListIsEmptyExceptionMessage => "Linked list is empty";
    private string NodeDoesNotBelongExceptionMessage => "Node does not belong to this list";
}