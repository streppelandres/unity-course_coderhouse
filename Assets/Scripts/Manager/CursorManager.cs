using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public enum CursorType { Default, Aiming }
    [SerializeField] private Texture2D defaultCursorTexture;
    [SerializeField] private Texture2D aimingCursorTexture;

    private static Dictionary<CursorType, Texture2D> cursorTextureDictionary;

    void Awake()
    {
        cursorTextureDictionary = new Dictionary<CursorType, Texture2D> {
            { CursorType.Default, defaultCursorTexture },
            { CursorType.Aiming, aimingCursorTexture }
        };

        SetCursor(CursorType.Default);
    }

    public static void SetCursor(CursorType type) {
        Cursor.SetCursor(cursorTextureDictionary[type], new Vector2(0f, 0f), CursorMode.Auto);
    }
}
