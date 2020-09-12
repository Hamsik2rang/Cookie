using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    private Vector2 hotSpot;

    public Texture2D cursorTexture;
    public bool hotSpotIsCenter = false;
    public Vector2 adjustHotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        StartCoroutine("LoadCursorCoroutine");
    }

    IEnumerator LoadCursorCoroutine()
    {
        yield return new WaitForEndOfFrame();

        if (hotSpotIsCenter)
        {
            hotSpot.x = cursorTexture.width / 2;
            hotSpot.y = cursorTexture.height / 2;
        }
        else
        {
            hotSpot = adjustHotSpot;
        }
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
