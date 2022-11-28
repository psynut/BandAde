using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour {

    private bool startedSequence = false;
    private bool textCompleted = false;     //Message is rendered. Player can now advance
    private bool advance = false;          //Player pressed submit - advanced text;
    private FacePic facePic;
    private TMP_Text tMPtext;

    private GroupingCollider groupingCollider; //Will pass itself to this LevelUI script to be ivoked when LevelUI completes MiddleSequences.

    private void Awake() {
        facePic = GetComponentInChildren<FacePic>();
        tMPtext = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start() {
        facePic.image.enabled = false;
        tMPtext.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator DelayTextCompleted() {
        yield return new WaitForSeconds(2f);
        textCompleted = true;
    } 

    public void ClickAdvance (){
        if(textCompleted) {
            advance = true;
        }
    }

    public void StartMiddleSequence(GroupingCollider m_groupingCollider) {
        groupingCollider = m_groupingCollider;
        facePic.image.enabled = true;
        tMPtext.enabled = true;
        if(!startedSequence) {
            StartCoroutine(MiddleSequence01());
            startedSequence = true;
        }
    }

    IEnumerator MiddleSequence01() {
        facePic.CharacterSpeaks(3,6f);
        tMPtext.text = ("We're on our way! Now that we've got the general rhythm, let's work together!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(MiddleSequence02());
    }

    IEnumerator MiddleSequence02() {
        facePic.CharacterSpeaks(1,5f);
        tMPtext.text = ("We'll definitely breeze through if we stick togeter!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(MiddleSequence03());
    }

    IEnumerator MiddleSequence03() {
        facePic.CharacterSpeaks(2,5f);
        tMPtext.text = ("This storm really tore the place up! How in the blazes did it get so so jumbled?");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(MiddleSequence04());
    }

    IEnumerator MiddleSequence04() {
        facePic.CharacterSpeaks(0,5f);
        tMPtext.text = ("This charity concert is gonna be out of this world!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(MiddleSequence05());
    }

    IEnumerator MiddleSequence05() {
        facePic.CharacterSpeaks(1,6f);
        tMPtext.text = ("With a little heart and our four elemental powers combined, I'm sure we can handle it!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        CloseMiddleSequence();
    }

    private void CloseMiddleSequence() {
        facePic.image.enabled = false;
        tMPtext.enabled = false;
        startedSequence = false;
        textCompleted = false;
        groupingCollider.PlaceCharacterTrain();
    }


    public void StartEndSequence(string characterName) {
        facePic.image.enabled = true;
        tMPtext.enabled = true;
        if(!startedSequence) {
            StartCoroutine(EndSequence01(characterName));
            startedSequence = true;
        }
    }

    IEnumerator EndSequence01(string characterName) {
        int characterInt = 0;
        switch(characterName) {
            case "Adam":
                break;
            case "Ariana":
                characterInt = 1;
                break;
            case "Cole":
                characterInt = 2;
                break;
            case "River":
                characterInt = 4;
                break;
            default:
                Debug.LogWarning("Unkown character string in " + this.name + "LevelUI.StartEndSequence()");
                break;
        }
        facePic.CharacterSpeaks(characterInt,6f);
        tMPtext.text = ("We finally made it too the coffee house! \nJust in Time!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(()=>advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence02());
    }
    IEnumerator EndSequence02() {
        facePic.CharacterSpeaks(3,6f);
        tMPtext.text = ("Guys! We've got to get this concert going.\nThe bank's deadline is almost up!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence03());
    }
    IEnumerator EndSequence03() {
        facePic.CharacterSpeaks(0,5f);
        tMPtext.text = ("Help me with the speakers, man!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence04());
    }
    IEnumerator EndSequence04() {
        facePic.CharacterSpeaks(2,5f);
        tMPtext.text = ("I'm just so freakin' buzzed! The band's back together... one more time!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence05());
    }
    IEnumerator EndSequence05() {
        facePic.CharacterSpeaks(1,5f);
        tMPtext.text = ("This little coffee house brings back memories! Our first big gig here was amazing. Now we get to return the favor by saving this little underdog local business from going under.");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }
}

