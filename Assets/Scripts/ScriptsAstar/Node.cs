using UnityEngine;

public class Node : IHeapItem<Node>
{

	public bool isWalkable;
	public Vector2 worldPosition;
	public int gridX;
	public int gridY;
	public int gCost;   // path cost from start to target
	public int hCost;   // estimated cost to the target
	public Node parent; // The node we took to get here so we can get the final path
    int heapIndex;      //  The index this node has in the heap, to make sorting nodes faster


    public Node(bool _isWalkable, Vector2 _worldPos, int _gridX, int _gridY)
    {
		isWalkable = _isWalkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost
    {
		get
        {
			return gCost + hCost;
		}
	}

    //  The IHeapItem interface requires that we implement this
    public int HeapIndex
    {
		get
        {
			return heapIndex;
		}
		set
        {
			heapIndex = value;
		}
	}
    

    public int CompareTo(Node nodeToCompare)
    {
		int compare = fCost.CompareTo(nodeToCompare.fCost);

		if (compare == 0)   // 0 means f costs of two nodes are same
        {
			compare = hCost.CompareTo(nodeToCompare.hCost); // Comparing nodes' h costs
		}
		return compare;
	}
}