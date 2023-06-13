
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class SpriteFromAtlas : MonoBehaviour
{
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] string spriteName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
    }

}
