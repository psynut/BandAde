using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject[] trunks;
    public GameObject[] foliage;
    public Transform[] transforms; //0. Tree Transform; 1. Foliage Transform

    private void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position+Vector3.up,"TreePlaceHolder.png",true);
    }

    private void Awake() {
        transforms = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject trunk = GameObject.Instantiate(trunks[Random.Range(0,trunks.Length)], transform.position, Quaternion.Euler(-90, 0 , 0), this.transform);
        trunk.transform.name = "Trunk";
        GameObject leaves = GameObject.Instantiate(foliage[Random.Range(0,trunks.Length)],transforms[1].position,Quaternion.Euler(-90, 0, 0),transforms[1]);
        leaves.transform.name = "Foliage";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
