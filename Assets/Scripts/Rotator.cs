using UnityEngine;

public class Rotator : MonoBehaviour, IHoverable
{
    [Header("Wobble Settings")]
    public Vector3 maxWobble = new Vector3(15f, 15f, 15f); // max angle offset per axis
    public float wobbleSpeed = 1f; // how fast the wobble moves
    public float scaleAmount = 1.2f; // scale multiplier when hovered
    public float scaleSpeed = 5f; // speed of scale transition
    public float returnSpeed = 100f; // speed to return to initial rotation

    private Vector3 initialRotation;
    private Vector3 initialScale;
    private Vector3 phase;
    private bool isHovered = false;

    void Start()
    {
        initialRotation = transform.localEulerAngles;
        initialScale = transform.localScale;
        // Randomize initial phase for each axis
        phase = new Vector3(
            Random.Range(0f, 2f * Mathf.PI),
            Random.Range(0f, 2f * Mathf.PI),
            Random.Range(0f, 2f * Mathf.PI)
        );
    }

    void Update()
    {
        if (!isHovered)
        {
            // Normal wobble
            phase += Vector3.one * wobbleSpeed * Time.deltaTime;
            ApplyWobble();
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * scaleSpeed);
            
        }
        else
        {
            // Smoothly return to initial rotation
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                Quaternion.Euler(initialRotation),
                returnSpeed * Time.deltaTime
            );
            Vector3 targetScale = initialScale * scaleAmount;

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        }
    }

    private void ApplyWobble()
    {
        float xAngle = Mathf.Sin(phase.x) * maxWobble.x;
        float yAngle = Mathf.Sin(phase.y) * maxWobble.y;
        float zAngle = Mathf.Sin(phase.z) * maxWobble.z;

        transform.localRotation = Quaternion.Euler(
            initialRotation.x + xAngle,
            initialRotation.y + yAngle,
            initialRotation.z + zAngle
        );
    }

    public void OnHoverEnter() => isHovered = true;
    public void OnHoverExit() => isHovered = false;
}
