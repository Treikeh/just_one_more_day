using UnityEngine;
// This script was created to show off Dialogue events


public class ChangeSpriteColour : MonoBehaviour
{
    [SerializeField] private Color newColour;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpriteColour()
    {
        spriteRenderer.color = newColour;
    }
}
