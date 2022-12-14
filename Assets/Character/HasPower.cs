using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HasPower : MonoBehaviour
{
    [HideInInspector]
    public Powers.power power;
    public UnityEvent usedPower;

    private Movement movement;
    private Transform characterModel;

    void Start() {
        movement = GetComponent<Movement>();
        characterModel = movement.characterNest;
        GivePowers();
    }

    private void GivePowers() {
        switch(name) {
            case "Adam":
                power = Powers.power.Earth;
                break;
            case "Ariana":
                power = Powers.power.Air;
                break;
            case "Cole":
                power = Powers.power.Fire;
                break;
            case "River":
                power = Powers.power.Water;
                break;
            default:
                Debug.LogWarning("Character name does not match known character in " + name + "HasPower.GivePowers()");
                break;
        }
    }

    public void UsePower() {
        float characterRotationX = characterModel.rotation.eulerAngles.y;
        RaycastHit hit;
        Physics.Raycast(transform.position+new Vector3(0,2,0),Quaternion.Euler(0,characterRotationX - movement.characterForward,0)*Vector3.forward,out hit,movement.unitOfMovement);
        if(hit.collider) {
            if(hit.transform.gameObject.GetComponent<AcceptCharacterPower>()) {
                hit.transform.gameObject.GetComponent<AcceptCharacterPower>().AcceptPower(power);
                usedPower.Invoke();
            }
        }
    }
}
