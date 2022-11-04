using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 3;
    public float unitOfMovement = 5;
    [SerializeField]
    public AnimationCurve lerpCurve;
    public Transform characterModel;
    [Tooltip("What is the angle of the model's transform so that forward is facing the correct direction")]
    public float characterForward;

    private Vector3 lerpStartPosition;
    private Vector3 destination;
    private float elapsedTime;
    private bool moving = false;


    private void Awake() {
        if(characterModel == null) {
            Debug.LogError("No character model added to " + this.name);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LerpMovement(Vector3 vec3) {
        Debug.Log("Runing LerpMovement");
        if(moving == false && vec3 != Vector3.zero) {
            moving = true;
            RotateModel(vec3);
            lerpStartPosition = transform.position;
            elapsedTime = 0f;
            destination = transform.position + vec3 * unitOfMovement;
            Debug.Log(gameObject.name + " moving to " + destination);
            StartCoroutine("Lerp");
        }
    }

    private void RotateModel(Vector3 vec3) {
        Vector3[] vectorDirections = { Vector3.left,Vector3.forward,Vector3.right,Vector3.down,Vector3.left };
        int result = System.Array.IndexOf(vectorDirections,vec3);
            characterModel.localRotation = Quaternion.Euler(0,characterForward + ((result-1)*90),0);
    }

    IEnumerator Lerp() {
        yield return null;
        transform.position = Vector3.Lerp(lerpStartPosition,destination,lerpCurve.Evaluate(elapsedTime / movementSpeed));
        if(elapsedTime < movementSpeed) {
            elapsedTime += Time.deltaTime;
            StartCoroutine("Lerp");
        } else {
            moving = false;
            transform.position = destination;
            Debug.Log(gameObject.name + " moved to " + destination);
        }
    }
}
