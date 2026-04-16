using Cinemachine;
using UnityEngine;

public class DeathZonesPos : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Reference to your main camera

    void Start()
    {
        GenerateBounds();
    }

    void GenerateBounds()
    {
        // Get the positions in world space of the camera bounds
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Create 4 edge colliders and set them as triggers
        CreateEdge("Left", new Vector3(bottomLeft.x - 1f, 15f, 0), new Vector2(1, 40+ topRight.y - bottomLeft.y), transform);
        CreateEdge("Right", new Vector3(topRight.x + 1f, 15f, 0), new Vector2(1, 40+ topRight.y - bottomLeft.y), transform);
    }

    void CreateEdge(string name, Vector3 position, Vector2 size, Transform parent)
    {
        int deathZone = LayerMask.NameToLayer("Death Zone");
        GameObject edge = new GameObject(name);
        edge.transform.position = position;
        edge.transform.SetParent(parent);
        BoxCollider2D collider = edge.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.isTrigger = true; // Mark as trigger to detect player passing through
        edge.layer = deathZone; // Use a specific tag for easier detection
    }
}
