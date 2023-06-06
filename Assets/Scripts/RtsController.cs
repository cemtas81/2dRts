
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


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
            foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
            {
                SeekerScript seekerScript = unit.GetComponent<SeekerScript>();

                if (seekerScript != null)
                {
                    seekerScript.enabled = true;
                    //target.gameObject.SetActive(true);
                }
            }
        }
    }

    private void SelectionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            startPosition=Input.mousePosition;

            Ray ray=cam.ScreenPointToRay(startPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 150))
            {
                if (hit.collider.gameObject.CompareTag("BarracksIcon"))
                {
                    hittoB = hit.collider.gameObject.GetComponent<Barracks>();
                    hittoB.OpenUnits();
                 
                }
                else if (hit.collider.gameObject.CompareTag("PowerPlantIcon"))
                {
                    if (hittoB!=null)
                    {
                        FindObjectOfType<Barracks>().CloseB();
                    }
                    Debug.Log("yoo");
                    
                    
                } 
                 else if (hit.collider.gameObject.CompareTag("Player")|| hit.collider.gameObject.CompareTag("Enemy") && hittoB != null)
                {
                    if (hittoB!=null)
                    {
                        FindObjectOfType<Barracks>().CloseB();
                    }
                    Debug.Log("yeee");
            
                }
           
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
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3,8);
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
            if (UnitIsInBox(cam.WorldToScreenPoint(SelectionManager.Instance.AvailableUnits[i].transform.position),bounds))
            {
                SelectionManager.Instance.Select(SelectionManager.Instance.AvailableUnits[i]);
            }
            else
            {
                SelectionManager.Instance.Deselect(SelectionManager.Instance.AvailableUnits[i]);
            }
        }
    }
    private bool UnitIsInBox(Vector2 pos,Bounds bounds)
    {
        return pos.x>bounds.min.x && pos.x < bounds.max.x&&pos.y>bounds.min.y&&pos.y<bounds.max.y;
    }
}
