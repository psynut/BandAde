using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landform : MonoBehaviour {
    public float ditchDepth;
    public int ditchWidth;
    public int ditchHeight;
    private Transform groundBlock;

    public GameObject river;
    public GameObject[] riverRocks;
    [RangeAttribute(1,20)]
    public int rockProbabilityDenominator;

    public enum LandFormType{
        Cliff,
        Ditch,
        River
    }

    public LandFormType myLandFormType;

    private void OnDrawGizmos() {
        if(myLandFormType == LandFormType.Ditch) {
            Gizmos.DrawIcon(transform.position + Vector3.up,"DitchPlaceHolder.png",true);
        } else if(myLandFormType == LandFormType.River) {
            Gizmos.DrawIcon(transform.position + Vector3.up,"RiverPlaceHolder.png",true);
        } else if(myLandFormType == LandFormType.Cliff) {
            Gizmos.DrawIcon(transform.position + Vector3.up,"CliffPlaceHolder.png",true);
        }


    }

    // Start is called before the first frame update
    void Start() {
        groundBlock = GetGroundBlock();
        Place(myLandFormType);
    }

    // Update is called once per frame
    void Update() {

    }

    internal void CharacterPowered(Powers.power characterPower) {
        //TODO
    }

    private void fill() {
                
    }

    private Transform GetGroundBlock() {
        Transform m_transform = null;
        Physics.Raycast(transform.position+new Vector3(0,.5f,0),Vector3.down,out RaycastHit hit,2f);
        if(hit.transform) {
            if(hit.transform.name == "Ground Block") {
                m_transform = hit.transform;
            }
        }
        if(m_transform == null) {
            Debug.LogWarning("No Groundblock in " + this.name + " at " + transform.position);
        }
        return m_transform;
    }

    private void Place(LandFormType m_landFormType) {
        try {
            groundBlock.GetComponent<MeshRenderer>().enabled = false;
        } catch(System.NullReferenceException ex) {
            Debug.LogWarning("No Ground Black Found in Landform.Place(LandFormType) at " + transform.position);
        }

        switch(m_landFormType) {
            case LandFormType.Cliff:
                //Place Cliff
                break;
            case LandFormType.Ditch:
                //Place Ditch
                break;
            case LandFormType.River:
                GameObject riverObject = GameObject.Instantiate(river,transform.position,Quaternion.Euler(-90,0,0),transform);
                riverObject.name = "River Tile";
                int m_randomNumber = Random.Range(0,rockProbabilityDenominator);
                Debug.Log(m_randomNumber);
                if(m_randomNumber < riverRocks.Length) {
                    GameObject riverRockObject = GameObject.Instantiate(riverRocks[m_randomNumber],transform.position,Quaternion.Euler(-90,0,0),transform);
                    riverRockObject.name = "River Rock";
                }
                break;
            default:
                Debug.LogWarning("Undefined landform type in " + this.name + "Landform.Place()");
                break;
        }
    }
}
