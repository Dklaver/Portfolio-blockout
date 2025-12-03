using UnityEngine;

public class HoverManager : MonoBehaviour
{
    private IHoverable lastHovered;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        IHoverable hoverable = null;

        if (Physics.Raycast(ray, out hit))
            hoverable = hit.transform.GetComponent<IHoverable>();

        if (lastHovered != null && lastHovered != hoverable)
            lastHovered.OnHoverExit();

        if (hoverable != null && hoverable != lastHovered)
            hoverable.OnHoverEnter();

        lastHovered = hoverable;
    }
}
