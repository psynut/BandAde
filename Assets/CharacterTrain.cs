using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class CharacterTrain : MonoBehaviour
{
    private List<Vector3> lastMovements;
    private List<Movement> characters;

    private Vector3 controllerVector3 = Vector3.zero;

    // Start is called before the first frame update

    void Awake() {
        lastMovements = new List<Vector3> { Vector3.zero,Vector3.zero,Vector3.zero, Vector3.zero };
    }

    void Start()
    {
        ReceiveCharacters(new Vector3(-8,0,84));
        StartCoroutine("CheckControls");
    }

    public void ReceiveCharacters(Vector3 vec3) {
        characters = FindObjectsOfType<Movement>().ToList();
        RemoveObstacleComponents();
        foreach(Movement movement in characters) {
            movement.transform.position = vec3;
        }
    }

    public IEnumerator CheckControls() {
        MoveTrain(controllerVector3);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("CheckControls");
    }

    private void RemoveObstacleComponents() {
        foreach(Movement movement in characters) {
            Destroy(movement.GetComponent<Obstacle>());
        }
    }

    public void InputDirection(InputAction.CallbackContext context) {
        controllerVector3 = Vector3.zero;
        float xAxis = context.ReadValue<Vector2>().x;
        float yAxis = context.ReadValue<Vector2>().y;
        if(Mathf.Abs(yAxis) > Mathf.Abs(xAxis) && Mathf.Abs(yAxis) > .5) {
            if(yAxis > 0) {
                controllerVector3 = Vector3.forward;
            } else {
                controllerVector3 = Vector3.back;
            }
        } else if(Mathf.Abs(xAxis) > Mathf.Abs(yAxis) && Mathf.Abs(xAxis) > .5) {
            if(xAxis > 0) {
                controllerVector3 = Vector3.right;
            } else {
                controllerVector3 = Vector3.left;
            }
        }
    }


    public void MoveTrain(Vector3 vec3) {

        if(vec3 != Vector3.zero) {
            bool isMoving = false;
            bool isObstructed = false;
            foreach(Movement character in characters) {
                if(!isMoving) {
                    isMoving = character.moving;
                }
            }
            isObstructed = characters[0].isObstructed(vec3);
            for(int i = 1; i <= characters.Count - 1 && !isObstructed; i++) {
                isObstructed = characters[i].isObstructed(lastMovements[lastMovements.Count - i]);
                Debug.Log(characters[i].name + " " + isObstructed);
            }
            
            if(!isMoving && !isObstructed) {
                lastMovements.Add(vec3);
                lastMovements.RemoveAt(0);
                for(int i = 0; i <= characters.Count - 1; i++) {
                    characters[i].LerpMovement(lastMovements[lastMovements.Count - 1 - i]);
                }
            }
        }
    }

    public void UsePower() {
        characters[0].GetComponent<HasPower>().UsePower();
    }
}
