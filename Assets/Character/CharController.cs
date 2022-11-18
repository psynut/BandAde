using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;


//To get the characters to move in a chain...
//All the characters are listed in the movement values
//May have to use this to keep track of where the other three go in the MoveCharacter
public class CharController : MonoBehaviour
{
    public Movement adam, ariana, cole, river;

    public Characters.Names activeCharacter = Characters.Names.Adam;

    [System.Serializable]
    public class Vector3Event : UnityEvent<Vector3> {}
    public Vector3Event moveCommand;

    [System.Serializable]
    public class CharacterEvent : UnityEvent<Characters.Names> {}
    public CharacterEvent changedActiveCharacter;

    public UnityEvent usedPower;

    private Movement[] characters;
    private Vector3 controllerVector3 = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine("CheckControls");
        characters = new Movement[] {adam,ariana,cole,river};
        moveCommand.AddListener(characters[(int)activeCharacter].LerpMovement);
        usedPower.AddListener(characters[(int)activeCharacter].GetComponent<HasPower>().UsePower);
        changedActiveCharacter.Invoke(activeCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Gets invoked by Character Controlls Input
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

    public IEnumerator CheckControls() {
        MoveCharacter(controllerVector3);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("CheckControls");
    }

    private void MoveCharacter(Vector3 vec3) {
        moveCommand.Invoke(vec3);     
    }

    public void UsePowers() {
        usedPower.Invoke();
    }

    private void ChangeActiveCharacter(Characters.Names m_character) {
        moveCommand.RemoveAllListeners();
        usedPower.RemoveAllListeners();
        activeCharacter = m_character;
        changedActiveCharacter.Invoke(m_character);
        moveCommand.AddListener(characters[(int)m_character].LerpMovement);
        usedPower.AddListener(characters[(int)m_character].GetComponent<HasPower>().UsePower);
    }

    public void ChangeActiveCharacter(InputAction.CallbackContext context) {
        ChangeActiveCharacter((Characters.Names)System.Enum.Parse(typeof(Characters.Names),context.action.name.Substring(9)));
    }
}
