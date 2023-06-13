

using UnityEngine;
using UnityEngine.EventSystems;


public class RtsController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private RectTransform SelectionBox;
    [SerializeField]
    private LayerMask UnitLayers, FloorLayers;
    [SerializeField]
    private float DragDelay = 0.1f,camSpeed=1,maxValueX,maxValueY,minValueX,minValueY,mouseDownTime;
    //public float radius;
    private Vector2 startPosition;
    public Transform target;
    private Barracks hittoB;
    public GameObject powerMenu, enemyBarracks;
    MyGrid grid;
    private void Awake()
    {
        grid = FindObjectOfType<MyGrid>();
    }
    private void Update()
    {
        SelectionInput();
     
    }
    private void LateUpdate()
    {
        MoveCam();
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
         
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {

                if (hit.collider.gameObject.CompareTag("BarracksIcon"))
                {

                    hittoB = hit.collider.gameObject.GetComponentInChildren<Barracks>();
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
                if (Input.GetKeyDown(KeyCode.RightAlt))
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

        else if (Input.GetAxis("Mouse ScrollWheel") != 0 && !EventSystem.current.IsPointerOverGameObject())
        {
           
                cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 100;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5, 13);
           
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
