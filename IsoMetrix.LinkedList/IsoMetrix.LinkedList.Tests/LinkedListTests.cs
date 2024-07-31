using IsoMetrix.LinkedList.Core;

namespace IsoMetrix.LinkedList.Tests;

public class LinkedListTests
{
    private readonly CustomLinkedList<string> _linkedList;
    
    public LinkedListTests()
    {
        _linkedList = new CustomLinkedList<string>();
    }
    
    [Fact]
    public void GivenANewNodeAndEmptyList_InsertFirstAddsToLinkedListInFirstLastSlot()
    {
        var firstNode = new Node<string>("First Node");
        
        _linkedList.InsertFirst(firstNode);

        Assert.Equal(1, _linkedList.Count);
        Assert.Equal(firstNode, _linkedList.First);
        Assert.Equal(firstNode, _linkedList.Last);
        Assert.Null(firstNode.Previous);
        Assert.Null(firstNode.Next);
    }

    [Fact]
    public void GivenANewNodeAndExistingList_InsertFirstAddsNodeToFirstSlot()
    {
        var firstNode = new Node<string>("First Node");
        var newNode = new Node<string>("New node");

        _linkedList.InsertFirst(firstNode);
        _linkedList.InsertFirst(newNode);
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Equal(firstNode, _linkedList.Last);
        Assert.Equal(newNode, _linkedList.First);
        Assert.Equal(newNode, firstNode.Previous);
        Assert.Null(newNode.Previous);
    }

    [Fact]
    public void GivenANewNodeAndEmptyList_InsertLastAddsToLinkedListInFirstLastSlot()
    {
        var lastNode = new Node<string>("Last node");
        
        _linkedList.InsertLast(lastNode);
        
        Assert.Equal(1, _linkedList.Count);
        Assert.Equal(lastNode, _linkedList.First);
        Assert.Equal(lastNode, _linkedList.Last);
        Assert.Null(lastNode.Previous);
        Assert.Null(lastNode.Next);
    }

    [Fact]
    public void GivenANewNodeAndExistingList_InsertLastAddsNodeToLastSlot()
    {
        var firstNode = new Node<string>("Last node");
        var newNode = new Node<string>("New node");
        
        _linkedList.InsertFirst(firstNode);
        _linkedList.InsertLast(newNode);
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Equal(firstNode, _linkedList.First);
        Assert.Equal(newNode, _linkedList.Last);
        Assert.Equal(newNode, firstNode.Next);
        Assert.Null(firstNode.Previous);
        Assert.Null(newNode.Next);
    }

    [Fact]
    public void GivenANewNodeAndExistingNode_InsertBeforeAddsNodeBeforeExistingNode()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        var newNode = new Node<string>("New node");
        
        _linkedList.InsertLast(firstNode);
        _linkedList.InsertLast(secondNode);
        _linkedList.InsertLast(thirdNode);

        _linkedList.InsertBefore(thirdNode, newNode);

        Assert.Equal(4, _linkedList.Count);
        Assert.Equal(secondNode, newNode.Previous);
        Assert.Equal(thirdNode, newNode.Next);
        Assert.Equal(newNode, thirdNode.Previous);
        Assert.Equal(newNode, secondNode.Next);
    }
    
    [Fact]
    public void GivenANewNodeAndExistingFirstNode_InsertBeforeAddsNodeToFirst()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var newNode = new Node<string>("New node");
        
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);

        _linkedList.InsertBefore(firstNode, newNode);

        Assert.Equal(3, _linkedList.Count);
        Assert.Equal(newNode, _linkedList.First);
        Assert.Equal(firstNode, newNode.Next);
        Assert.Equal(newNode, firstNode.Previous);
        Assert.Null(newNode.Previous);
    }
    
    [Fact]
    public void GivenANewNodeAndNodeNotFromList_InsertBeforeThrowsInvalidOperationException()
    {
        var existingNodeNotFromList = new Node<string>("First node");
        var newNode = new Node<string>("New node");

        Assert.Throws<InvalidOperationException>(() => _linkedList.InsertBefore(existingNodeNotFromList, newNode));
    }

    [Fact]
    public void GivenANewNodeAndExistingNode_InsertAfterAddsNodeAfterExistingNode()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        var newNode = new Node<string>("New node");
        
        _linkedList.InsertLast(firstNode);
        _linkedList.InsertLast(secondNode);
        _linkedList.InsertLast(thirdNode);

        _linkedList.InsertAfter(secondNode, newNode);

        Assert.Equal(4, _linkedList.Count);
        Assert.Equal(secondNode, newNode.Previous);
        Assert.Equal(thirdNode, newNode.Next);
        Assert.Equal(newNode, secondNode.Next);
        Assert.Equal(newNode, thirdNode.Previous);
    }
    
    [Fact]
    public void GivenANewNodeAndExistingLastNode_InsertAfterAddsNodeToLast()
    {
        var firstNode = new Node<string>("First node");
        var lastNode = new Node<string>("Last node");
        var newNode = new Node<string>("New node");
        
        _linkedList.InsertFirst(firstNode);
        _linkedList.InsertLast(lastNode);

        _linkedList.InsertAfter(lastNode, newNode);

        Assert.Equal(3, _linkedList.Count);
        Assert.Null(newNode.Next);
        Assert.Equal(newNode, _linkedList.Last);
        Assert.Equal(lastNode, newNode.Previous);
        Assert.Equal(newNode, lastNode.Next);
    }
    
    [Fact]
    public void GivenANewNodeAndNodeNotFromList_InsertAfterThrowsInvalidOperationException()
    {
        var existingNodeNotFromList = new Node<string>("First node");
        var newNode = new Node<string>("New node");

        Assert.Throws<InvalidOperationException>(() => _linkedList.InsertAfter(existingNodeNotFromList, newNode));
    }

    [Fact]
    public void GivenAnEmptyList_DeleteFirstThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _linkedList.DeleteFirst());
    }

    [Fact]
    public void GivenAListWithOneEntry_DeleteFirstEmptiesTheList()
    {
        var firstNode = new Node<string>("First node");

        _linkedList.InsertFirst(firstNode);
        _linkedList.DeleteFirst();
        
        Assert.Equal(0, _linkedList.Count);
        Assert.Null(_linkedList.First);
        Assert.Null(_linkedList.Last);
    }

    [Fact]
    public void GivenAPopulatedList_DeleteFirstRemovesTheFirstEntry()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        
        _linkedList.InsertFirst(thirdNode);
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);
        
        _linkedList.DeleteFirst();
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Equal(secondNode, _linkedList.First);
        Assert.Null(secondNode.Previous);
    }

    [Fact]
    public void GivenAnEmptyList_DeleteLastThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _linkedList.DeleteLast());
    }
    
    [Fact]
    public void GivenAListWithOneEntry_DeleteLastEmptiesTheList()
    {
        var lastNode = new Node<string>("Last node");

        _linkedList.InsertLast(lastNode);
        _linkedList.DeleteLast();
        
        Assert.Equal(0, _linkedList.Count);
        Assert.Null(_linkedList.First);
        Assert.Null(_linkedList.Last);
    }
    
    [Fact]
    public void GivenAPopulatedList_DeleteLastRemovesTheLastEntry()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        
        _linkedList.InsertFirst(thirdNode);
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);
        
        _linkedList.DeleteLast();
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Equal(secondNode, _linkedList.Last);
        Assert.Null(secondNode.Next);
    }

    [Fact]
    public void GivenAnExistingNode_DeleteRemovesTheNodeFromItsCurrentPosition()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        
        _linkedList.InsertFirst(thirdNode);
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);
        
        _linkedList.Delete(secondNode);
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Equal(firstNode, thirdNode.Previous);
        Assert.Equal(thirdNode, firstNode.Next);
    }
    
    [Fact]
    public void GivenAnExistingNodeIsLast_DeleteRemovesTheNodeFromLastPosition()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        
        _linkedList.InsertFirst(thirdNode);
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);
        
        _linkedList.Delete(thirdNode);
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Null(secondNode.Next);
        Assert.Equal(secondNode, _linkedList.Last);
    }
    
    [Fact]
    public void GivenAnExistingNodeIsFirst_DeleteRemovesTheNodeFromFirstPosition()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        
        _linkedList.InsertFirst(thirdNode);
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);
        
        _linkedList.Delete(firstNode);
        
        Assert.Equal(2, _linkedList.Count);
        Assert.Null(secondNode.Previous);
        Assert.Equal(secondNode, _linkedList.First);
    }

    [Fact]
    public void GivenADeletedNode_DeleteThrowsAnInvalidOperationException()
    {
        var firstNode = new Node<string>("First node");
        
        _linkedList.InsertFirst(firstNode);
        _linkedList.Delete(firstNode);
        
        Assert.Throws<InvalidOperationException>(() => _linkedList.Delete(firstNode));
    }

    [Fact]
    public void GivenAnExistingList_PrintListPrintsAllElements()
    {
        var firstNode = new Node<string>("First node");
        var secondNode = new Node<string>("Second node");
        var thirdNode = new Node<string>("Third node");
        
        _linkedList.InsertFirst(thirdNode);
        _linkedList.InsertFirst(secondNode);
        _linkedList.InsertFirst(firstNode);

        var prints = new List<string>();
        _linkedList.OnPrint += print =>
        {
            prints.Add(print);
        };
        
        _linkedList.PrintList();
        
        Assert.Equal(3, prints.Count);
        Assert.Contains(firstNode.Data, prints);
        Assert.Contains(secondNode.Data, prints);
        Assert.Contains(thirdNode.Data, prints);
    }
    
    [Fact]
    public void GivenAnEmptyList_PrintListThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _linkedList.PrintList());
    }
}