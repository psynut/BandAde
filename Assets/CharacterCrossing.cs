using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCrossing : MonoBehaviour {

    public Characters.Names characterCanCross;
    private ParticleSystem m_ParticleSystem;
    private Collider m_Collider;

    private void Awake() {
        m_Collider = GetComponent<Collider>();
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == characterCanCross.ToString()) {
            m_ParticleSystem.Play();
            other.GetComponent<HasSoundEffect>().PlayRandomSoundEffect();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == characterCanCross.ToString()) {
            m_ParticleSystem.Stop();
        }
    }
}

