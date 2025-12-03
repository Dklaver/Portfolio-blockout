using UnityEngine;

public class Bouncer : MonoBehaviour, IHoverable
{
    [Header("Bounce Settings")]
    public float amplitude = 1f;  // How high it bounces
    public float frequency = 1f;  // How fast it bounces
    public float returnSpeed = 5f;
    private bool isHovered;
    private float startY;
    public float scaleAmount = 1.2f; // scale multiplier when hovered
    public float scaleSpeed = 5f; // speed of scale transition
    private Vector3 initialScale;

    void Start()
    {
        // Store the starting Y position
        startY = transform.position.y;
        
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (isHovered)
        {
            float newY = Mathf.Lerp(transform.position.y, startY, Time.deltaTime * returnSpeed);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            Vector3 targetScale = initialScale * scaleAmount;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        }
        else
        {
            float newY = startY + Mathf.Sin(Time.time * frequency * 2 * Mathf.PI) * amplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * scaleSpeed);
            
        }
    }

    public void OnHoverEnter() => isHovered = true;
    public void OnHoverExit() => isHovered = false;
}