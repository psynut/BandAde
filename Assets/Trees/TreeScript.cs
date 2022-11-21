using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject[] trunks;
    public GameObject[] foliage;
    public GameObject downedTree;
    public Material[] foliageMaterials;
    public Transform[] transforms; //0. Tree Transform; 1. Foliage Transform
    public bool alive = true;
    public float flameSpreadDelay;
    public float randomPlacementThreshold;

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
        float randNumberX = Random.Range(0f,randomPlacementThreshold)-(randomPlacementThreshold/2);
        float randNumberZ = Random.Range(0f,randomPlacementThreshold) - (randomPlacementThreshold / 2);
        GameObject trunk = GameObject.Instantiate(trunks[Random.Range(0,trunks.Length)], transform.position+new Vector3(randNumberX,0,randNumberZ), Quaternion.Euler(-90, 0 , 0), this.transform);
        trunk.transform.name = "Trunk";
        if(alive) {
            GameObject leaves = GameObject.Instantiate(foliage[Random.Range(0,trunks.Length)],transforms[1].position + new Vector3(randNumberX,0,randNumberZ),Quaternion.Euler(-90,0,0),transforms[1]);
            leaves.transform.name = "Foliage";
            leaves.GetComponent<MeshRenderer>().material = foliageMaterials[Random.Range(0,foliageMaterials.Length)];
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
        } else if(!alive && powerUsed == Powers.power.Air) {
            Debug.Log("CharacterPowered recognized to invoke KnockedOver()");
            KnockOver();
        }
        if(onFire && (powerUsed == Powers.power.Water || powerUsed == Powers.power.Air)) {
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

    public void KnockOver() {
        bool knockedTreeOver = false;
        Landform[] rivers = FindNeighborRiver();
        Debug.Log("rivers Length: " + rivers.Length);
        for(int i=0; i<rivers.Length; i++) {
            if(rivers[i] != null){
                knockedTreeOver = true;
                Debug.Log(rivers[i].transform.position);
                GameObject deadTreePrefab = GameObject.Instantiate(downedTree,rivers[i].transform.position,Quaternion.Euler(0,90f * i,0),rivers[i].transform);
                deadTreePrefab.name = "Downed Tree Bridge";
                Debug.Log("Placing downed tree @ " + deadTreePrefab + " Parent is at " + deadTreePrefab.transform.parent.position);
                Destroy(rivers[i].GetComponent<Obstacle>());
                Destroy(rivers[i].GetComponent<Landform>()); //less sure if this is needed or problematic.
            }
        }
        if(knockedTreeOver) {
            Destroy(gameObject);
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
        }

        Vector3[] cardinalDirection = { Vector3.forward,Vector3.right,Vector3.back,Vector3.left };
        List<TreeScript> deadTrees = new List<TreeScript>();
        LayerMask layerMask = LayerMask.GetMask("Trees");
        for(int i = 0; i < cardinalDirection.Length; i++) {
            Physics.Raycast(transform.position,cardinalDirection[i],out RaycastHit hit, treeSizes[i % 2], layerMask.value);
            if(hit.transform) {
                TreeScript m_treeScript = hit.transform.GetComponent<TreeScript>();
                if(m_treeScript != null && hit.transform.GetComponent<TreeScript>().alive == false) {
                    deadTrees.Add(hit.transform.GetComponent<TreeScript>());
                }
            }
        }
        return deadTrees.ToArray();
    }

    private Landform[] FindNeighborRiver() {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if(boxCollider == null) {
            Debug.LogWarning("No boxcollider found in this Tree gameojbect at: " + gameObject.transform.position);
        }
        //Ideally trees are cube or squares -- trying to make this more idiot proof.
        float[] treeSizes = { boxCollider.size.z * transform.lossyScale.z,boxCollider.size.x * transform.lossyScale.x };
        foreach(float size in treeSizes) {
        }
        Vector3[] cardinalDirection = { Vector3.forward,Vector3.right,Vector3.back,Vector3.left };
        Landform[] rivers = new Landform[4];
        LayerMask layerMask = LayerMask.GetMask("Landforms");
        for(int i = 0; i < cardinalDirection.Length; i++) {
            Physics.Raycast(transform.position,cardinalDirection[i],out RaycastHit hit,treeSizes[i % 2],layerMask.value);
            if(hit.transform) {
                Landform m_landform= hit.transform.GetComponent<Landform>();
                Debug.Log("Raycast hit landform @ " + m_landform.transform.name);
                if(m_landform != null && hit.transform.GetComponent<Landform>().myLandFormType == Landform.LandFormType.River) {
                   rivers[i] = hit.transform.GetComponent<Landform>();
                    Debug.Log("Raycast hit river @ " + hit.transform.position);
                }
            }
        }
        return rivers;
    }
}
