using UnityEngine;


public class Units : MonoBehaviour {
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(Sprite sprite) {
        _renderer.sprite = sprite;
    }

}
