using UnityEngine;
using UnityEngine.UI;

public class Canvas_Cantroller : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        Camera uicamera = canvas.worldCamera;

        bool isTablet = uicamera.aspect > (9f / 16f);

        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.matchWidthOrHeight = isTablet ? 1 : 0;
    }
}
