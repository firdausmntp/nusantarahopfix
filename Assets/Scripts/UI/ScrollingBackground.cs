using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = new Vector2(0f, scrollSpeed);
    }

    void Update()
    {
        mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
