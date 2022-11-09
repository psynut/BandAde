using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject[] trunks;
    public GameObject[] foliage;
    public Transform[] transforms; //0. Tree Transform; 1. Foliage Transform
    public bool alive = true;
    public float flameSpreadDelay;

    private TreeScript[] deadTreeNieghbors;


    [HideInInspector]
    public bool onFire = false;
    private ParticleSystem m_particleSystem;

    private void OnDrawGizmos() {
        if(alive) {
            Gizmos.DrawIcon(transform.position + Vector3.up,"TreePlaceHolder.png",true);
        } else {
            Gizmos.DrawIcon(transform.position + Vector3.up,"DeadTreePlaceHolder.png",true);
        }

    }

    private void Awake() {
        transforms = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject trunk = GameObject.Instantiate(trunks[Random.Range(0,trunks.Length)], transform.position, Quaternion.Euler(-90, 0 , 0), this.transform);
        trunk.transform.name = "Trunk";
        if(alive) {
            GameObject leaves = GameObject.Instantiate(foliage[Random.Range(0,trunks.Length)],transforms[1].position,Quaternion.Euler(-90,0,0),transforms[1]);
            leaves.transform.name = "Foliage";
        } else {
            deadTreeNieghbors = FindNeighborDeadTrees();
        }
        m_particleSystem = GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterPowered(Powers.power powerUsed) {
        if(!alive && powerUsed == Powers.power.Fire) {
            CatchFire();
        } else if(onFire && (powerUsed == Powers.power.Water || powerUsed == Powers.power.Air)){
            Destroy(gameObject);
        }
            
    }

    public void CatchFire() {
        onFire = true;
        m_particleSystem.Play();
        StartCoroutine("SpreadFire");
    }

    private IEnumerator SpreadFire() {
        yield return new WaitForSeconds(flameSpreadDelay);
        foreach(TreeScript treeScript in deadTreeNieghbors) {
            if(treeScript.onFire == false) {
                treeScript.CatchFire();
            }
        }
    }

    private TreeScript[] FindNeighborDeadTrees() {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if(boxCollider == null) {
            Debug.LogWarning("No boxcollider found in this Tree gameojbect at: " + gameObject.transform.position);
        }
        //Ideally trees are cube or squares -- trying to make this more idiot proof.
        float[] treeSizes = { boxCollider.size.z * transform.lossyScale.z,boxCollider.size.x * transform.lossyScale.x };
        foreach(float size in treeSizes) {
            Debug.Log(size);
        }

        Vector3[] cardinalDirection = { Vector3.forward,Vector3.right,Vector3.back,Vector3.left };
        List<TreeScript> deadTrees = new List<TreeScript>();
        LayerMask layerMask = LayerMask.GetMask("Trees");
        for(int i = 0; i < cardinalDirection.Length; i++) {
            Physics.Raycast(transform.position,cardinalDirection[i],out RaycastHit hit, 3f/*treeSizes[i % 2]*/, layerMask.value);
            if(hit.transform) {
                Debug.Log(hit.transform.name);
                TreeScript m_treeScript = hit.transform.GetComponent<TreeScript>();
                if(m_treeScript != null && hit.transform.GetComponent<TreeScript>().alive == false) {
                    deadTrees.Add(hit.transform.GetComponent<TreeScript>());
                }
            }
        }
        return deadTrees.ToArray();
    }
}
