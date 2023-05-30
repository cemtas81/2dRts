
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highLight;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _baseColor,offsetColor;
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : _baseColor;
    }
    private void OnMouseEnter()
    {
        highLight.SetActive(true);
    }
    private void OnMouseExit()
    {
        highLight?.SetActive(false);
    }
}
