using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAnimationRandomizer : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update

        private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(RandomSwitch());

    }

    private IEnumerator RandomSwitch() {
        yield return new WaitForSeconds(3f);
        int coinFlip = Random.Range(0,2);
        if(coinFlip == 0) {
            animator.SetTrigger("Switch");
        }
        StartCoroutine(RandomSwitch());
    }
}
