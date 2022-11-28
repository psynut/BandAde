using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptCharacterPower : MonoBehaviour
{
    private TreeScript treeScript;
    private Landform landform;
    
    // Start is called before the first frame update
    void Start()
    {
        treeScript = GetComponent<TreeScript>();
        landform = GetComponent<Landform>();
    }

    public void AcceptPower(Powers.power characterPower) {
        if(treeScript != null) {
            treeScript.CharacterPowered(characterPower);
        }
        if(landform != null) {
            landform.CharacterPowered(characterPower);
        }
    }
}
