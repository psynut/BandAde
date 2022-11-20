using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDialogue : MonoBehaviour
{
    private bool textCompleted = false;     //Message is rendered. Player can now advance
    private bool advance = false;          //Player pressed submit - advanced text;
    private FacePic facePic;
    private TMP_Text tMPtext;

    private void Awake() {
        facePic = GetComponentInChildren<FacePic>();
        tMPtext = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start() {
        facePic.image.enabled = false;
        tMPtext.enabled = false;
    }

    private IEnumerator DelayTextCompleted() {
        yield return new WaitForSeconds(2f);
        textCompleted = true;
    }

    public void ClickAdvance() {
        if(textCompleted) {
            advance = true;
        }
    }

    public void StartSeqeunce() {
        facePic.image.enabled = true;
        tMPtext.enabled = true;
        StartCoroutine(EndSequence01());
    }

    IEnumerator EndSequence01() {
        facePic.CharacterSpeaks(2,6f);
        tMPtext.text = ("Hey, Adam! It's been a while. You doing all right?");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence02());
    }
    IEnumerator EndSequence02() {
        facePic.CharacterSpeaks(0,5f);
        tMPtext.text = ("That was a crazy storm last night. \nI'm still stuck at home. Feels like when I use to be grounded.\nWhat's going on Cole?");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence03());
    }

    IEnumerator EndSequence03() {
        facePic.CharacterSpeaks(2,7f);
        tMPtext.text = ("Ariela and I stopped by Little Guy Coffee House today. \nThe bank's shutting it down! We gotta do something!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence04());
    }

    IEnumerator EndSequence04() {
        facePic.CharacterSpeaks(0,2f);
        tMPtext.text = ("What!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence05());
    }

    IEnumerator EndSequence05() {
        facePic.CharacterSpeaks(1,6f);
        tMPtext.text = ("I know! Little Guy's where we got our first big break! \nIt's like the last corner for REAL people around here!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence06());
    }

    IEnumerator EndSequence06() {
        facePic.CharacterSpeaks(0,3f);
        tMPtext.text = ("You guys talk with River yet?");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence07());
    }

    IEnumerator EndSequence07() {
        facePic.CharacterSpeaks(3,6f);
        tMPtext.text = ("Oh! Hey Adam! Yeah, it's no good. \nIt's tearful news.");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence09());
    }

    IEnumerator EndSequence09() {
        facePic.CharacterSpeaks(1,4f);
        tMPtext.text = ("You guys thinking what I'm thinking?");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence10());
    }

    IEnumerator EndSequence10() {
        facePic.CharacterSpeaks(2,6f);
        tMPtext.text = ("That's a hot idea, Ariela");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence11());
    }

    IEnumerator EndSequence11() {
        facePic.CharacterSpeaks(3,6f);
        tMPtext.text = ("Maybe we can use our ELEMENTAL powers to clear the way to the coffee house...\n...We'll get the band back together one last time!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        StartCoroutine(EndSequence12());
    }

    IEnumerator EndSequence12() {
        facePic.CharacterSpeaks(2,6f);
        tMPtext.text = ("And save the little guy coffee house with a charity show!");
        StartCoroutine(DelayTextCompleted());
        yield return new WaitUntil(() => advance);
        textCompleted = false;
        advance = false;
        facePic.StopAllCoroutines();
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }
}
