using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{

    [SerializeField]
    private float tileX = 1, tileY = 1;

    private Mesh mesh;

    private Material material;

    private void Awake() {
        material = GetComponent<Renderer>().material;
        mesh = GetComponent<MeshFilter>().mesh;

    }
    // Start is called before the first frame update
    void Start()
    {
        material.mainTextureScale = new Vector2((mesh.bounds.size.x * transform.localScale.x) / 100 * tileX,(mesh.bounds.size.y * transform.localScale.y) / 100 * tileY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
