
using UnityEngine;
using System.Collections;

public class SeekerScript : MonoBehaviour
{
	public Transform target;
	public float speed = 1;
	public float rotationSpeed=1;
	Vector2[] path;
	int targetIndex;
	float timer;
	Vector3 currenttarget;	

	void Start()
    {

		currenttarget = target.position;
		PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
	}

 
	void Update()
	{		
		timer += Time.deltaTime;

		if (timer > 0.30)
        {
			timer = 0;

			//  if the target position have change already
			if ((target.position != currenttarget))
			{				
				//Debug.Log ("Path changed to " + target.position);
				currenttarget = target.position;
				PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
			}
		}

	}

	public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
		if (pathSuccessful)
        {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
    {

		targetIndex = 0;

		Vector3 currentWaypoint = path[0];

        while (true)
        {

			//if you reach a waypoint
			if (transform.position == currentWaypoint) 
			{
				//go to the next waypoint
				targetIndex ++;



				//if the next waypoint is bigger than the path length, reset the waypoint and path to 0
				if (targetIndex >= path.Length) 
				{
					targetIndex = 0;
					path = new Vector2[0];
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);



			Vector2 targetWaypointDirection = currentWaypoint - transform.position;
			if (targetWaypointDirection != Vector2.zero)    // not looking in the direction of the path
			{
                // Seeker rotates/looks in the direction of the path
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetWaypointDirection, Vector2.up), rotationSpeed * Time.fixedDeltaTime);
			}

			yield return null;

		}
	}

	public void OnDrawGizmos()
    {
		if (path != null)
        {
			for (int i = targetIndex; i < path.Length; i ++)
            {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex)
                {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
                {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}