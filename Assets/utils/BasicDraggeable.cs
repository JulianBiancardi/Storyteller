using UnityEngine;

public abstract class BasicDraggeable: MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnMouseEnter()
    {
        if(!DragAndDrop.instance.isDragging){
            CursorManager.SetCursor(CursorManager.CursorState.Hover);
        }
    }

    void OnMouseExit()
    {
        if(!DragAndDrop.instance.isDragging){
            CursorManager.SetCursor(CursorManager.CursorState.Default);
        }
    }

    public abstract GameObject OnDrag();
}
