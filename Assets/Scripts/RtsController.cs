
using UnityEngine;


public class RtsController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private RectTransform SelectionBox;
    [SerializeField]
    private LayerMask UnitLayers;
    [SerializeField]
    private LayerMask FloorLayers;
    [SerializeField]
    private float DragDelay=0.1f;
    private Vector2 startPosition;
    private float mouseDownTime;
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
        if (Input.GetMouseButtonDown(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            startPosition=Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && mouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);
            mouseDownTime = 0;
        }
    }
    private void MoveCam()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float movey = Input.GetAxisRaw("Vertical");
        cam.transform.Translate(movex * Time.deltaTime, movey * Time.deltaTime, 0);
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
