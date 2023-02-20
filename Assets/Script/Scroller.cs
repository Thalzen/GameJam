using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [Range(0f, 5f)] public float scrollSpeed= 0.5f;
    private float offset;
    private Renderer mat;
    void Start()
    {
        mat = GetComponent<Renderer>();
        mat.sortingLayerName = "Background";
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
    }
}
