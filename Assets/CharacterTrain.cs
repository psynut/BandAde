using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class CharacterTrain : MonoBehaviour
{

    [System.Serializable]
    public class CharacterEvent : UnityEvent<Characters.Names> {}
    public CharacterEvent changedActiveCharacter;

    private Characters.Names currentLeader;
    private List<Vector3> lastMovements;
    private List<Movement> characters;
    private bool swappingCharacters = false; // without the method will run serval times at once

    private Vector3 controllerVector3 = Vector3.zero;

    // Start is called before the first frame update

    void Awake() {
        lastMovements = new List<Vector3> { Vector3.zero,Vector3.zero,Vector3.zero, Vector3.zero };
    }

    void Start()
    {
        ReceiveCharacters(transform.position);
        StartCoroutine("CheckControls");
    }

    public void ReceiveCharacters(Vector3 vec3) {
        characters = FindObjectsOfType<Movement>().ToList();
        RemoveObstacleComponents();
        currentLeader = (Characters.Names)System.Enum.Parse(typeof(Characters.Names),characters[0].name);
        foreach(Movement movement in characters) {
            movement.StopAllCoroutines();
            movement.GetComponent<Animator>().SetBool("isWalking",false);
            movement.transform.position = vec3;
            movement.moving = false;
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
                if(isObstructed) {
                    Debug.Log(characters[i].name + " " + isObstructed);
                }
            }

            if(!isMoving && !isObstructed && !swappingCharacters && !TrainIsObstructed(vec3)) {
                lastMovements.Add(vec3);
                lastMovements.RemoveAt(0);
                for(int i = 0; i <= characters.Count - 1; i++) {
                    characters[i].LerpMovement(lastMovements[lastMovements.Count - 1 - i]);
                }
            } else {
                characters[0].RotateModel(vec3);
            }
        }
    }


    public void ChangeActiveCharacter(InputAction.CallbackContext context) {
        ChangeActiveCharacter((Characters.Names)System.Enum.Parse(typeof(Characters.Names),context.action.name.Substring(9)));
    }

    private void ChangeActiveCharacter(Characters.Names m_character) {
        int newLead = characters.FindIndex(character => character.transform.name == m_character.ToString());
        if(newLead > 0 && !swappingCharacters) {
            swappingCharacters = true;
            StartCoroutine(SwappCharacterIteration(newLead,newLead));
        }
    }

    IEnumerator SwappCharacterIteration(int newLead, int iterationsToGo) {
        yield return null;
        if(iterationsToGo > 0) {
            if(characters[newLead].moving == false && characters[0].moving == false) {
                characters[0].LerpMovement(-lastMovements[(lastMovements.Count - 1) - (newLead - iterationsToGo)]);
                characters[newLead].LerpMovement(lastMovements[(lastMovements.Count-1)-(iterationsToGo-1)]);
                iterationsToGo--;
            }
                StartCoroutine(SwappCharacterIteration(newLead,iterationsToGo));
        } else {
            SwapCharactersInList(newLead);
        }
    }

    private void SwapCharactersInList(int newLead) {

        Movement newLeadMovement = characters[newLead];
        characters[newLead] = characters[0];
        characters[0] = newLeadMovement;
            Debug.Log("0: " + characters[0].name +" 1:"+ characters[1].name+" 2:"+ characters[2].name +" 3:" + characters[3].name);
        Debug.Log("lastMovements: "+ lastMovements[3] +", " + lastMovements[2] +", " +lastMovements[1] +", " +lastMovements[0]);
        swappingCharacters = false;

    }

    public bool TrainIsObstructed(Vector3 vec3) {
        bool m_bool = false;
        LayerMask layerMask = LayerMask.NameToLayer("Landform") | LayerMask.NameToLayer("Trees");
        RaycastHit hit;
        Physics.Raycast(characters[0].transform.position + Vector3.up,vec3,out hit,characters[0].unitOfMovement + .01f,layerMask.value);
        if(hit.transform) {
            m_bool = hit.transform.gameObject.GetComponent<Obstacle>();
        }
        return m_bool;
    }

    public void UsePower() {
        characters[0].GetComponent<HasPower>().UsePower();
    }
}
