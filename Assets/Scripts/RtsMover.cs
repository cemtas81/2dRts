
using System.Collections.Generic;
using UnityEngine;

public class RtsMover : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (Input.GetMouseButtonUp(1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            List<Vector3> targetPoslist = GetPosListAround(target.position, new float[] { 1, 2, 4f }, new int[] { 5, 10, 20 });

            int targetPosLÝstIndex = 0;
            foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
            {
                if (unit != null)
                {

                    if (unit.TryGetComponent<SeekerScript>(out var seekerScript))
                    {
                        seekerScript.Move(targetPoslist[targetPosLÝstIndex]);
                        targetPosLÝstIndex = targetPosLÝstIndex + 1 % targetPoslist.Count;
                        seekerScript.LookAtTarget();
                    }
                }

            }
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("dsadfsaf");
                }
                if (hit.collider.CompareTag("EnemyBarracks"))
                {
                    Debug.Log("dsadfsaf");
                }

            }
        }
    }
    private List<Vector3> GetPosListAround(Vector3 startPos, float[] ringDistanceArray, int[] ringPosCountArray)
    {
        List<Vector3> poslist = new List<Vector3>();
        poslist.Add(startPos);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            poslist.AddRange(GetPosListAround(startPos, ringDistanceArray[i], ringPosCountArray[i]));

        }
        return poslist;
    }
    private List<Vector3> GetPosListAround(Vector3 startPos, float distance, int positionCount)
    {
        List<Vector3> poslist = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPos + dir * distance;
            poslist.Add(position);
        }
        return poslist;
    }
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
  
  
}
