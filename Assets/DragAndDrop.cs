using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public static DragAndDrop instance { get; private set;}
    private Camera mainCamera;
    private GameObject currentDragObject;
    public bool isDragging { get; private set;}

    void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    void Start(){
        mainCamera = Camera.main;
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSelect(){
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if(hit.collider != null){
            isDragging = true;
            CursorManager.SetCursor(CursorManager.CursorState.Drag);
            BasicDraggeable basicDraggeable = hit.collider.gameObject.GetComponent<BasicDraggeable>();
            if(basicDraggeable != null){
                currentDragObject = basicDraggeable.OnDrag();
                currentDragObject.layer = LayerMask.NameToLayer("CurrentDraggedObject");
                StartCoroutine(DragObject(currentDragObject));
            }else {
                Debug.Log("Not a BasicDraggeable");
            }
        }
    }

    private IEnumerator DragObject(GameObject clickedObject){
        while(Input.GetMouseButton(0)){
            clickedObject.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            clickedObject.transform.position = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, -5);
            yield return null;
        }
    }

    void OnDeSelect(){
        isDragging = false;
        CursorManager.SetCursor(CursorManager.CursorState.Default);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("CurrentDraggedObject");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, ~layerMask);
        if(hit.collider != null){
            Container container = hit.collider.gameObject.GetComponent<Container>();
            Frame frame = container?.transform.parent.GetComponent<Frame>();
            frame.ReceiveDragOperation(currentDragObject, container);
        }

        Destroy(currentDragObject);
    }
}
