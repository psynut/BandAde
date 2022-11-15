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

    private void OnTriggerEnter(Collider m_collider) {
        Debug.Log("Collision");
        if(m_collider.transform.name != null) {
            levelComplete.Invoke(m_collider.transform.name);
            Debug.Log("collision with " + m_collider.transform.name);
        }
    }
}
