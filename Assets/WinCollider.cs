using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinCollider : MonoBehaviour {

    public UnityEvent<string> levelComplete;


    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.name != null) {
            levelComplete.Invoke(collision.transform.name);
        }
    }
}
