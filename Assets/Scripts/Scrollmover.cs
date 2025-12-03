using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Splines;

public class DollyScroller : MonoBehaviour
{
    [Header("References")]
    public CinemachineSplineDolly dolly;
    public SplineContainer splineContainer;

    [Header("Scroll Settings")]
    public float scrollSpeed = 5f;       // How much scroll affects target position
    public float smoothTime = 0.2f;      // Smooth movement
    public float scrollThreshold = 0.2f; // Minimum scroll delta to trigger movement
    public bool invertScroll = false;
    public bool loop = false;

    private Spline spline;
    private float targetPosition;
    private float velocityRef = 0f;
    private float accumulatedScroll = 0f;

    private void Start()
    {
        if (splineContainer != null)
            spline = splineContainer.Spline;

        if (dolly != null)
            targetPosition = dolly.CameraPosition;
    }

    private void Update()
    {
        if (dolly == null || spline == null)
            return;

        HandleScrollInput();
        SmoothMoveDolly();
    }

    private void HandleScrollInput()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (invertScroll)
            scrollDelta = -scrollDelta;

        // Accumulate scroll
        accumulatedScroll += scrollDelta;

        // Only move if accumulated scroll exceeds threshold
        if (Mathf.Abs(accumulatedScroll) >= scrollThreshold)
        {
            // Move proportionally to the accumulated scroll
            targetPosition += accumulatedScroll * scrollSpeed;

            // Reset accumulated scroll
            accumulatedScroll = 0f;

            // Clamp or loop
            float maxLength = spline.GetLength();
            if (targetPosition > maxLength)
                targetPosition = loop ? 0f : maxLength;
            else if (targetPosition < 0f)
                targetPosition = loop ? maxLength : 0f;
        }
    }

    private void SmoothMoveDolly()
    {
        dolly.CameraPosition = Mathf.SmoothDamp(
            dolly.CameraPosition,
            targetPosition,
            ref velocityRef,
            smoothTime
        );
    }
}
