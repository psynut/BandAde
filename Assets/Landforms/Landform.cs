using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landform : MonoBehaviour {
    public float ditchDepth;
    public int ditchWidth;
    public int ditchHeight;
    
    public GameObject river;
    public GameObject[] riverRocks;
    [RangeAttribute(1,20)]
    public int rockProbabilityDenominator;

    public GameObject cliff;
    public GameObject ditch;

    public enum LandFormType{
        Cliff,
        Ditch,
        River
    }

    public LandFormType myLandFormType;
    private Transform groundBlock;

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
        switch(myLandFormType) {
            case LandFormType.Cliff:
                if(characterPower == Powers.power.Earth) {
                    DestroyLandform();
                }
                break;
            case LandFormType.Ditch:
                if(characterPower == Powers.power.Earth) {
                    DestroyLandform();
                } else if(characterPower == Powers.power.Water) {
                    FormRiver();
                }
                break;
            case LandFormType.River:
                if(characterPower == Powers.power.Earth) {
                    DestroyLandform();
                }
                break;
            default:
                Debug.LogWarning("Undefined landform type in " + this.name + "Landform.CharacterPowered()");
                break;
        }
    }

    private void FormRiver() {
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        Destroy(meshFilter.gameObject);
        myLandFormType = LandFormType.River;
        Place(myLandFormType);
    }

    private void DestroyLandform() {
        SwitchGroundBlock(true);
        Destroy(gameObject);
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
        switch(m_landFormType) {
            case LandFormType.Cliff:
                GameObject cliffObject = GameObject.Instantiate(cliff,transform.position,Quaternion.Euler(-90f,0,0),transform);
                cliffObject.name = "Cliff Tile";
                break;
            case LandFormType.Ditch:
                SwitchGroundBlock(false);
                GameObject ditchObject = GameObject.Instantiate(ditch,transform.position,Quaternion.Euler(-90f,0,0),transform);
                ditchObject.name = "Ditch Tile";
                break;
            case LandFormType.River:
                SwitchGroundBlock(false);
                GameObject riverObject = GameObject.Instantiate(river,transform.position,Quaternion.Euler(-90f,0,0),transform);
                riverObject.name = "River Tile";
                int m_randomNumber = Random.Range(0,rockProbabilityDenominator);
                if(m_randomNumber < riverRocks.Length) {
                    GameObject riverRockObject = GameObject.Instantiate(riverRocks[m_randomNumber],transform.position,Quaternion.Euler(-90f,0,0),transform);
                    riverRockObject.name = "River Rock";
                }
                break;
            default:
                Debug.LogWarning("Undefined landform type in " + this.name + "Landform.Place()");
                break;
        }
    }

    private void SwitchGroundBlock(bool onOff) {
        try {
            groundBlock.GetComponent<MeshRenderer>().enabled = onOff;
        } catch(System.NullReferenceException ex) {
            Debug.LogWarning("No Ground Block Found in Landform.Place(LandFormType) at " + transform.position + "\n" + ex.Message);
        }
    }
}
