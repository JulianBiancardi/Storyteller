using UnityEngine;

public abstract class Button : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    public CursorState onHoverCursor = CursorState.Hover;  
    public CursorState onDefaultCursor = CursorState.Default;
    bool isHovering = false;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnMouseEnter()
    {
        isHovering = true;
        CursorManager.instance.SetCursor(onHoverCursor);
    }

    void OnMouseExit()
    {
        isHovering = false;
        CursorManager.instance.SetCursor(onDefaultCursor);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isHovering){
            OnClick();
        }
    }

    public abstract void OnClick();
}
