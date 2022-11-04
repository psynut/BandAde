using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 3;
    public float unitOfMovement = 5;
    [SerializeField]
    public AnimationCurve lerpCurve;


    private Vector3 lerpStartPosition;
    private Vector3 destination;
    private float elapsedTime;
    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moving == false) {
            LerpMovement(Vector3.left);
        }
    }

    public void LerpMovement(Vector3 direction) {
        moving = true;
        lerpStartPosition = transform.position;
        elapsedTime = 0f;
        destination = transform.position + direction;
        Debug.Log(gameObject.name + " moving to " + destination);
        StartCoroutine("Lerp");
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
