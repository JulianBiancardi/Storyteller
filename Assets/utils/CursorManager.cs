using System.Collections.Generic;
using UnityEngine;

public static class CursorManager{

    private static Dictionary<CursorState, Texture2D> cursorTextures = new Dictionary<CursorState, Texture2D>();
    public enum CursorState{
        Default,
        Drag,
        Hover,
    }

    static CursorManager(){
        cursorTextures.Add(CursorState.Default, Resources.Load<Texture2D>("cursor/ui_mouse"));
        cursorTextures.Add(CursorState.Drag, Resources.Load<Texture2D>("cursor/ui_mouse_hand_close"));
        cursorTextures.Add(CursorState.Hover, Resources.Load<Texture2D>("cursor/ui_mouse_hand"));

        SetCursor(CursorState.Default);
    }

    public static void SetCursor(CursorState cursorState = CursorState.Default){
        Cursor.SetCursor(cursorTextures.GetValueOrDefault(cursorState), Vector2.zero, CursorMode.Auto);
    }
}
