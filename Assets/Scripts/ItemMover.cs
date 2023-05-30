using UnityEngine;

public class ItemMover : MonoBehaviour
{ 
    private Vector3 targetPosition;
   
    void Update()
    {
       
        if (Input.GetMouseButtonDown(1))
        {
      
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f; 
            Move();
        
        }

    }
    void Move()
    {
        transform.position = targetPosition;
    }
    
}
