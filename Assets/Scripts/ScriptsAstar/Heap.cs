using System;

// T in this case will be Node
// T : IHeapItem<T> means that the Node has to implement the interface
public class Heap<T> where T : IHeapItem<T>
{    
    T[] items; // The array that will hold the heap
    int currentItemCount; // Number of nodes we have stored in the heap

    public Heap(int maxHeapSize) 
	{
		
		items = new T[maxHeapSize];  //  instantiate the array with the maxheapsize specified by the user
    }
    

    public void Add(T item) 
	{
        if (currentItemCount + 1 > items.Length)
        {
            //Debug.Log("Heap is full cannot add new item");

            return;
        }

        item.HeapIndex = currentItemCount;

		items[currentItemCount] = item; // Adding item to the end of the array
		SortUp(item);   // Sorting the item's position in the heap
		currentItemCount++;
	}
  
    public T RemoveFirst()
    {
        if (Count > 1)
        {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);

            return firstItem;
        }
        else if (Count == 1)
		{
            T firstItem = items[0];
            currentItemCount--;
            items[0] = default(T);

            return firstItem;
        }

        return default(T);
    }    

	public int Count 
	{
		get 
		{
			return currentItemCount;
		}
	}    

    public bool Contains(T item) 
	{
		return Equals(items[item.HeapIndex], item);
	}    

    void SortDown(T item) 
	{
		while (true) 
		{
            // Formula for finding childs
            // left child = (2n + 1)
            // right child = (2n + 2)
            // n is position of item in array ( T[])
            int leftChild =  2 * item.HeapIndex + 1;
			int rightChild = 2 * item.HeapIndex + 2;

			int swapIndex = 0;

			if (leftChild < currentItemCount) // Check if left child is last item
			{
				swapIndex = leftChild;

				if (rightChild < currentItemCount) // Check if right child is last item
				{
                    //  Compare the left and the right node, to find if we should swap with the left or the right node
                    if (items[leftChild].CompareTo(items[rightChild]) > 0) 
					{
						swapIndex = rightChild;
					}
				}

				if (item.CompareTo(items[swapIndex]) > 0) 
				{
					Swap (item,items[swapIndex]);
				}
				else 
				{
					return;
				}

			}
			else 
			{
				return;
			}

		}
	}

    void SortUp(T item) 
	{
        // Formula for finding parent
        // parentIndex = (n - 1) / 2
        // n is position of item in array ( T[])
		int parentIndex = (item.HeapIndex - 1) / 2; //From heap index to array index

        while (true) 
		{
			T parentItem = items[parentIndex];

			if (item.CompareTo(parentItem) < 0) //If item has a lower f cost than the parent
            {
				Swap (item,parentItem);
			}
			else 
			{
				break;
			}

			parentIndex = (item.HeapIndex - 1) / 2;
		}
	}    

    void Swap(T itemA, T itemB) 
	{
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;
        //  Swapping the heap indexes
        int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

public interface IHeapItem<T> : IComparable<T> 
{
	int HeapIndex 
	{
		get;
		set;
	}
}