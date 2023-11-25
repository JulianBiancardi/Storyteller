using UnityEngine;

public abstract class BasicDraggeable: MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public CursorState onHoverCursor = CursorState.Hover;  
    public CursorState onDefaultCursor = CursorState.Default;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnMouseEnter()
    {
        if(!DragAndDrop.instance.isDragging){
            CursorManager.instance.SetCursor(onHoverCursor);
        }
    }

    void OnMouseExit()
    {
        if(!DragAndDrop.instance.isDragging){
            CursorManager.instance.SetCursor(onDefaultCursor);
        }
    }

    public abstract GameObject OnDrag();
}
