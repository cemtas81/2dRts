
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicScrollView : MonoBehaviour
{
    private ScrollRect scrollRect;
    public List<GameObject> objectPool;
    public GameObject objectToPool;
    public int poolSize;
    public float space=100;
    public Transform content;
    private int topItemIndex;
    private int botItemIndex;
    private int topNextItemPos;
    private int botNextItemPos;

    public Vector3 spacing = Vector3.down * 50;

    [SerializeField]
    private Transform topLimit;

    [SerializeField]
    private Transform botLimit;

    private bool initComplete = false;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
     
        InitScrollView();
    }

    private void Update()
    {
        if (initComplete) 
        {
            if (objectPool[botItemIndex].transform.position.y < botLimit.position.y)
            {
                //Scrolling Up
                InfiniteView(true);
            }
            else if (objectPool[topItemIndex].transform.position.y > topLimit.position.y)
            {
                //Scrolling Down
                InfiniteView(false);
            }
        }
    }

    public void InitScrollView()
    {
        objectPool = new List<GameObject>();
        GameObject tmp;
        //ScrollViewItem item;
        for (int i = 0; i < poolSize; i++)
        {
            tmp = Instantiate(objectToPool, content);
            objectPool.Add(tmp);
            
        }

        topItemIndex = 0;
        botItemIndex = poolSize - 1;
        topNextItemPos = 1;
        botNextItemPos = -poolSize;
        OrderListItems();
        initComplete = true;
    }

    private void OrderListItems()
    {
        for (int i = 0; i < poolSize ; i++)
        {
            if (objectPool[i] != null)
            {
                objectPool[i].transform.localPosition = (spacing * i);
            }
        }
        topLimit.position = objectPool[0].transform.position - spacing;
        botLimit.position = objectPool[poolSize - 1].transform.position + spacing;
    }

    private GameObject GetPooledObject(int objectIndex)
    {
        if (objectIndex >= 0 && objectIndex < poolSize)
        {
            return objectPool[objectIndex];
        }
        return null;
    }

    private void InfiniteView(bool goingUp)
    {
       
        if (goingUp)
        {
           
            GameObject newObj = GetPooledObject(botItemIndex);
            if (newObj != null)
            {
                newObj.transform.localPosition = Vector3.up * space * topNextItemPos;
                topItemIndex = botItemIndex;

                if (botItemIndex == 0)
                {
                    botItemIndex = poolSize - 1;
                }
                else
                {
                    botItemIndex--;
                }
                topNextItemPos++;
                botNextItemPos++;
            }
        }

        if (!goingUp)
        {
            GameObject newObj = GetPooledObject(topItemIndex);
            if (newObj != null)
            {
                newObj.transform.localPosition = Vector3.up * space * botNextItemPos;
                botItemIndex = topItemIndex;

                if (topItemIndex == poolSize - 1)
                {
                    topItemIndex = 0;
                }
                else
                {
                    topItemIndex++;
                }
                botNextItemPos--;
                topNextItemPos--;
            }
        }
    }
}
