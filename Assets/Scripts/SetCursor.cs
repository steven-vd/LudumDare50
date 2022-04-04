using UnityEngine;

public class SetCursor : MonoBehaviour {
    
    public Texture2D texture;
    public Vector2 hotspot;
    public CursorMode cursorMode;
    
    private void Awake() {
        Cursor.SetCursor(texture, hotspot, cursorMode);
    }

}
