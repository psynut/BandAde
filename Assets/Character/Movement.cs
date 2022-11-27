using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 0.25f;
    public float unitOfMovement = 5f;
    [SerializeField]
    public AnimationCurve lerpCurve;
    public Transform characterNest;
    [Tooltip("What is the angle of the model's transform so that forward is facing the correct direction")]
    public float characterForward;

    [HideInInspector]
    public bool moving = false;

    private Vector3 lerpStartPosition;
    private Vector3 destination;
    private float elapsedTime;


    private Animator animator;


    private void Awake() {
        if(characterNest == null) {
            Debug.LogError("No character nest added to " + this.name);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LerpMovement(Vector3 vec3) {
        if(moving == false && vec3 != Vector3.zero && !isObstructed(vec3)) {
            moving = true;
            animator.SetBool("isWalking",true);
            RotateModel(vec3);
            lerpStartPosition = transform.position;
            elapsedTime = 0f;
            destination = transform.position + vec3 * unitOfMovement;
            //Debug.Log(gameObject.name + " moving to " + destination);
            StartCoroutine("Lerp");
        } else if(moving == false && vec3 != Vector3.zero && isObstructed(vec3)){
            RotateModel(vec3);
        }
    }

    public void RotateModel(Vector3 vec3) {
        Vector3[] vectorDirections = { Vector3.left,Vector3.forward,Vector3.right,Vector3.down,Vector3.left };
        int result = System.Array.IndexOf(vectorDirections,vec3);
            characterNest.localRotation = Quaternion.Euler(0,characterForward + ((result-1)*90),0);
    }

    public bool isObstructed(Vector3 vec3) {
        bool m_bool = false;
        LayerMask layerMask = LayerMask.NameToLayer("Landform") | LayerMask.NameToLayer("Trees");
        RaycastHit hit;
        Physics.Raycast(transform.position+Vector3.up,vec3,out hit,unitOfMovement+.01f, layerMask.value);
        if(hit.transform) {
            m_bool = hit.transform.gameObject.GetComponent<Obstacle>();
            if(hit.transform.GetComponent<Landform>() != null) {
                if(hit.transform.GetComponent<Landform>().myLandFormType == Landform.LandFormType.Ditch && GetComponent<HasPower>().power == Powers.power.Air) {
                    m_bool = false;
                } else if(hit.transform.GetComponent<Landform>().myLandFormType == Landform.LandFormType.River && GetComponent<HasPower>().power == Powers.power.Water) {
                    m_bool = false;
                }
            }
        }
        return m_bool;
    }

    IEnumerator Lerp() {
        yield return null;
        transform.position = Vector3.Lerp(lerpStartPosition,destination,lerpCurve.Evaluate(elapsedTime / movementSpeed));
        if(elapsedTime < movementSpeed) {
            elapsedTime += Time.deltaTime;
            StartCoroutine("Lerp");
        } else {
            moving = false;
            animator.SetBool("isWalking",false);
            transform.position = destination;
            //Debug.Log(gameObject.name + " moved to " + destination);
        }
    }
}
