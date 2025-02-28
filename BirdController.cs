using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float maxYVelocity = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Flap(float faceY)
    {
        // Map face Y position to screen space
        float screenY = Camera.main.ViewportToScreenPoint(new Vector3(0, faceY, 0)).y;
        float targetY = Mathf.Clamp(screenY / Screen.height, 0.1f, 0.9f) * Camera.main.orthographicSize * 2;

        // Move bird towards target Y position
        Vector2 newPosition = new Vector2(transform.position.x, targetY);
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * 5f);

        // Clamp velocity
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxYVelocity, maxYVelocity));
    }
}
