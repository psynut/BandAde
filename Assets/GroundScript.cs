using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject[] groundBlocks;
    public Transform groundCollection;

    // Start is called before the first frame update
    void Start() {
        GameObject groundBlock = GameObject.Instantiate(groundBlocks[Random.Range(0,groundBlocks.Length)],transform.position,Quaternion.Euler(-90,0,0),this.transform);
        groundBlock.transform.name = "Ground Block";
        Debug.Log(groundBlock.name);
        groundBlock.transform.parent = groundCollection;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
