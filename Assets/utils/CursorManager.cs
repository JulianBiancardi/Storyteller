using System.Collections.Generic;
using UnityEngine;
public enum CursorState{
    Default,
    Drag,
    Hover,
    NextPage,
    PreviusPage
}
public class CursorManager: MonoBehaviour{

    public static CursorManager instance { get; private set;}
    private SpriteRenderer spriteRenderer;
    private Dictionary<CursorState, Sprite> cursorTextures = new Dictionary<CursorState, Sprite>();

    void Awake() {
        if(instance == null){
            instance = this;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        cursorTextures.Add(CursorState.Default, Resources.Load<Sprite>("cursor/ui_mouse"));
        cursorTextures.Add(CursorState.Drag, Resources.Load<Sprite>("cursor/ui_mouse_hand_close"));
        cursorTextures.Add(CursorState.Hover, Resources.Load<Sprite>("cursor/ui_mouse_hand"));
        cursorTextures.Add(CursorState.NextPage, Resources.Load<Sprite>("cursor/ui_mouse_pageflip_right"));
        cursorTextures.Add(CursorState.PreviusPage, Resources.Load<Sprite>("cursor/ui_mouse_pageflip_left"));

        Cursor.visible = false;
        SetCursor(CursorState.Default);
    }

    void Start(){
        Cursor.visible = false;
    }

    public void SetCursor(CursorState cursorState = CursorState.Default){
        spriteRenderer.sprite = cursorTextures.GetValueOrDefault(cursorState);   
    }

    void Update(){
        Vector2 currPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currPos;
    }
}
