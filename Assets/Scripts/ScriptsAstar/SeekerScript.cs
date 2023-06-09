﻿
using UnityEngine;
using System.Collections;
using Unity.Mathematics;


public class SeekerScript : MonoBehaviour
{
	public Vector3 target;
	public float speed = 1;
 
    Vector2[] path;
	int targetIndex;

    public Vector3 currenttarget;
    public float angle;

  
    
    public void Start()
    {
     
        currenttarget = target;
		PathRequestManager.RequestPath(transform.position,target, OnPathFound);
        
    }

    public void Move(Vector3 target)
    {
    
        //  if the target position have change already
        if ((target!= currenttarget))
        {
           
            //Debug.Log ("Path changed to " + target.position);
            currenttarget = target;
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
       
        }
    }

    public void Stop()
    {
     
        StopCoroutine("FollowPath");
    }

    private void OnDisable()
    {
        Stop();
    }
    public void StopCoroutines()
    {
        StopAllCoroutines();
    }
    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (!gameObject.activeInHierarchy)
            return; // Exit if the game object is inactive

        if (target == null)
        {
            Stop();
            return;
        }
        if (pathSuccessful)
        {
            if (target != null) // Check if the target object is still valid
            {
                path = newPath;
                targetIndex = 0;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
            else
            {
                Stop();
            }
        }
        else
        {
            Stop();
        }
    }

    IEnumerator FollowPath()
    {
        targetIndex = 0;
        if (path == null || path.Length == 0)
        {
            yield break; // Exit the coroutine if the path is empty
        }

        Vector3 currentWaypoint = path[0];


        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector2[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            // Calculate the desired movement direction towards the current waypoint
            Vector3 targetWaypointDirection = currentWaypoint - transform.position;
            Vector3 movementDirection = targetWaypointDirection.normalized;

            // Check for collisions using a raycast in the movement direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, movementDirection.magnitude);

            if (hit.collider != null)
            {
                // If a collision is detected, adjust the movement direction to avoid the obstacle
                movementDirection = Vector3.Reflect(movementDirection, hit.normal);
          
            }

            // Move towards the next waypoint using the adjusted movement direction
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection, Vector2.up), rotationSpeed * Time.fixedDeltaTime);
            angle =math.atan2 (targetWaypointDirection.y, targetWaypointDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
    public void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 45);
        }
    }
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}