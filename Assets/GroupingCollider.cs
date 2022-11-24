using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupingCollider : MonoBehaviour
{
    public GameObject characterTrain;
    public GameObject togetherParticleSystem;
    private int characterCount = 0;
    static bool AdamPassed = false;
    static bool ArianaPassed = false;
    static bool ColePassed = false;
    static bool RiverPassed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Movement>() != null) {
            switch(other.GetComponent<Movement>().name) {
                case "Adam":
                    AdamPassed = true;
                    break;
                case "Ariana":
                    ArianaPassed = true;
                    break;
                case "Cole":
                    ColePassed = true;
                    break;
                case "River":
                    RiverPassed = true;
                    break;
                default:
                    Debug.LogWarning("default case thrown in " + this.name + " OntTriggerEnter switch statement");
                    break;
            }
        }
        if(AdamPassed && ArianaPassed && ColePassed && RiverPassed) {
            GameObject m_CharController = FindObjectOfType<CharController>().gameObject;
            Destroy(m_CharController);
            GameObject m_charTrain = Instantiate(characterTrain,transform.position, Quaternion.identity);
            GameObject m_particleSystem = Instantiate(togetherParticleSystem,transform.position + new Vector3(-1.02f,2.4f,0.34134f),Quaternion.Euler(0,-14.69f,48.41f));
            GroupingCollider[] groupingColliders = FindObjectsOfType<GroupingCollider>();
            foreach(GroupingCollider gColllider in groupingColliders) {
                Destroy(gColllider.gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other) {
        
    }
}
