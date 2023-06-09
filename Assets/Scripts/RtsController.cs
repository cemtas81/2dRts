using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RtsController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private RectTransform SelectionBox;
    [SerializeField]
    private LayerMask UnitLayers, FloorLayers;
    [SerializeField]
    private float DragDelay = 0.1f,camSpeed=1,maxValueX,maxValueY,minValueX,minValueY;
    private float mouseDownTime;
    private Vector2 startPosition;
    public Transform target;
    private Barracks hittoB;
    public GameObject powerMenu, enemyBarracks;
    private void Update()
    {
        SelectionInput();
        HandleMovement();
    }
    private void LateUpdate()
    {
        MoveCam();
    }
    private void HandleMovement()
    {
        if (Input.GetMouseButtonUp(1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            List<Vector3> targetPoslist = GetPosListAround(target.position,new float[] {1,2,3f},new int[] {5,10,20});
                  
            int targetPosLÝstIndex = 0;
            foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
            {
                if (unit != null)
                {                    

                    if (unit.TryGetComponent<SeekerScript>(out var seekerScript))
                    {
                        seekerScript.Move(targetPoslist[targetPosLÝstIndex]);
                        targetPosLÝstIndex=targetPosLÝstIndex+1%targetPoslist.Count;
                    }
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
    private List<Vector3> GetPosListAround(Vector3 startPos,float distance,int positionCount)
    {
        List<Vector3> poslist = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
            Vector3 dir= ApplyRotationToVector(new Vector3(1,0),angle);
            Vector3 position =startPos + dir*distance;
            poslist.Add(position);
        }
        return poslist;
    }
    private Vector3 ApplyRotationToVector(Vector3 vec,float angle)
    {
        return Quaternion.Euler(0,0,angle)*vec;
    }
    private void SelectionInput()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject())
        {
            var selectedObject = EventSystem.current.currentSelectedGameObject;
            if (selectedObject != null)
            {
                var canvasGroup = selectedObject.GetComponent<CanvasGroup>();
                if (canvasGroup != null && canvasGroup.alpha == 0)
                {
                    return;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            startPosition=Input.mousePosition;

            Ray ray=cam.ScreenPointToRay(startPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {

                if (hit.collider.gameObject.CompareTag("BarracksIcon"))
                {

                    hittoB = hit.collider.gameObject.GetComponent<Barracks>();
                    hittoB.OpenUnits();
                    enemyBarracks.SetActive(false);
                    powerMenu.SetActive(false);
                }
                else if (hit.collider.gameObject.CompareTag("PowerPlantIcon"))
                {
                    if (hittoB!=null)
                    {
                        hittoB.CloseB();
                    }                 
                    enemyBarracks.SetActive(false);
                    powerMenu.SetActive(true);

                }

                else if (hit.collider.gameObject.CompareTag("EnemyBarracks"))
                {
                    if (hittoB != null)
                    {
                        hittoB.CloseB();
                    }
                    enemyBarracks.SetActive(true);
                    powerMenu.SetActive(false);
                }

            }
         
            else
            {
                if (hittoB != null)
                {
                    hittoB.CloseB();
                }
                enemyBarracks.SetActive(false);
                powerMenu.SetActive(false);
            }
               

        }
        else if (Input.GetMouseButton(0) && mouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,UnitLayers)
                &&hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (SelectionManager.Instance.IsSelected(unit))
                    {
                        SelectionManager.Instance.Deselect(unit);
                    }
                    else
                    {
                        SelectionManager.Instance.Select(unit);
                    }
                }
                else
                {
                    SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(unit);
                }
            }
            else if (mouseDownTime+DragDelay>Time.time)
            {
                SelectionManager.Instance.DeselectAll();
            }
            mouseDownTime = 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime*100;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5,13);
        }
    }
    private void MoveCam()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float movey = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(movex, movey).normalized;
        cam.transform.Translate(camSpeed * Time.deltaTime * move);
        cam.transform.position = new Vector3(Mathf.Clamp(transform.position.x,minValueX,maxValueX),Mathf.Clamp(transform.position.y,minValueY,maxValueY)
        ,cam.transform.position.z);
    }
    private void ResizeSelectionBox()
    {
        float width=Input.mousePosition.x-startPosition.x;
        float height=Input.mousePosition.y-startPosition.y;
        SelectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
        SelectionBox.sizeDelta=new Vector2(Mathf.Abs(width),Mathf.Abs (height));
        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);
        for (int i = 0; i < SelectionManager.Instance.AvailableUnits.Count; i++)
        {
            if(SelectionManager.Instance.AvailableUnits[i] != null) 
            {
                if (UnitIsInBox(cam.WorldToScreenPoint(SelectionManager.Instance.AvailableUnits[i].transform.position), bounds))
                {

                    SelectionManager.Instance.Select(SelectionManager.Instance.AvailableUnits[i]);

                }
                else
                {
                    SelectionManager.Instance.Deselect(SelectionManager.Instance.AvailableUnits[i]);
                }
            }
      
        }
    }
    private bool UnitIsInBox(Vector2 pos,Bounds bounds)
    {
        return pos.x>bounds.min.x && pos.x < bounds.max.x&&pos.y>bounds.min.y&&pos.y<bounds.max.y;
    }
}
