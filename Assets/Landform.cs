using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landform : MonoBehaviour
{
    public float ditchDepth;
    public int ditchWidth;
    public int ditchHeight;
    private Terrain _targetTerrain;

    // Start is called before the first frame update
    void Start()
    {
        LowerTerrain(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void CharacterPowered(Powers.power characterPower) {
        throw new NotImplementedException();
    }



    //From WinterboltGames https://forum.unity.com/threads/simple-runtime-terrain-editor.502650/
    //Didn't work...

    //Now Try from https://answers.unity.com/questions/478454/modify-terrain-through-codescript.html
    private void LowerTerrain() {
        Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit);
        Terrain m_Terrain = hit.transform.GetComponent<Terrain>();
    }

    

}
