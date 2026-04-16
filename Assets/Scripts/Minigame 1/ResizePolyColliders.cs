using UnityEngine;

public class ResizePolyColliders : MonoBehaviour
{
    public PolygonCollider2D[] collidersToUpdate;
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Update colliders initially on Start
        UpdateColliderBounds();
    }

    // Call this method whenever the screen resolution changes
    // (e.g., in Update() if testing in editor, or when a resolution change event fires)
    public void UpdateColliderBounds()
    {
        // Calculate the current world space boundaries of the camera
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Vector2 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        // Define the points for the screen boundary polygon
        Vector2[] screenPoints = new Vector2[]
        {
            bottomLeft,
            topLeft,
            topRight,
            bottomRight
        };

        // Update each collider
        foreach (PolygonCollider2D collider in collidersToUpdate)
        {
            // Note: Directly setting the 'points' array is how you modify the collider's shape at runtime
            collider.points = screenPoints;
        }
    }

    // Optional: Use this to update automatically when the screen size changes
    void Update()
    {
        // A simple way to check for resolution change in the editor/standalone builds
        // More sophisticated methods exist for mobile (using events/coroutines)
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            UpdateColliderBounds();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
        }
    }

    private int lastScreenWidth;
    private int lastScreenHeight;
}
