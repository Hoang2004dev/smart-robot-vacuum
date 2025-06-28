using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Desired View Size (World Units)")]
    public float targetWidth = 20f;
    public float targetHeight = 10f;

    [Header("Clamp Zoom")]
    public float minSize = 2f;
    public float maxSize = 20f;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        ApplyCameraSize();
    }

    private void ApplyCameraSize()
    {
        if (targetWidth <= 0 || targetHeight <= 0)
        {
            Debug.LogWarning("❌ Chiều rộng hoặc chiều cao không hợp lệ.");
            return;
        }

        float screenAspect = (float)Screen.width / Screen.height;

        // Tính chiều cao cần có để nhìn đủ chiều rộng mong muốn
        float requiredHeightForWidth = targetWidth / screenAspect;

        float finalHeight = Mathf.Max(targetHeight, requiredHeightForWidth);

        float finalSize = Mathf.Clamp(finalHeight / 2f, minSize, maxSize);

        mainCamera.orthographicSize = finalSize;

        Debug.Log($"✅ Camera adjusted: View ≈ {finalHeight * screenAspect} x {finalHeight} (Size = {finalSize})");
    }
}
