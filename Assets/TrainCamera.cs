using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCamera : MonoBehaviour
{

    private AnimationCurve lerpCurve;
    private float movementSpeed = .025f;
    private float unitOfMovement = 2f;

    private float startTime;
    private Vector3 lerpStartPosition;
    private Vector3 destination;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AcceptAssignment(AnimationCurve m_curve,float m_speed,float m_units) {
        lerpCurve = m_curve;
        movementSpeed = m_speed;
        unitOfMovement = m_units;
    }

    public void LerpMovement(Vector3 vec3) {
        startTime = Time.time;
        lerpStartPosition = transform.position;
        destination = lerpStartPosition + vec3 * unitOfMovement;
            StartCoroutine("Lerp");
    }

    IEnumerator Lerp() {
        yield return null;
        float elapsedTime = Time.time - startTime;
        transform.position = Vector3.Lerp(lerpStartPosition,destination,lerpCurve.Evaluate(elapsedTime / movementSpeed));
        if(elapsedTime < movementSpeed) {
            elapsedTime += Time.deltaTime;
            StartCoroutine("Lerp");
        } else {
            transform.position = destination;
        }
    }
}
