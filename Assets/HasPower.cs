using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPower : MonoBehaviour
{

    public Powers power;

    private Movement movement;
    private Transform characterModel;

    void Start() {
        movement = GetComponent<Movement>();
        characterModel = movement.characterModel;
    }

    public void UsePower() {
        float characterRotationX = characterModel.rotation.eulerAngles.y;
        RaycastHit hit;
        Physics.Raycast(transform.position,Quaternion.Euler(0,characterRotationX - movement.characterForward,0)*Vector3.forward,out hit,movement.unitOfMovement);
        if(hit.transform) {
            Obstacle obstacle = hit.transform.gameObject.GetComponent<Obstacle>();
            Debug.Log("Will need to use this to transform " + obstacle.name);
        }
    }

}
