using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject instructionPanelA, instructionPanelB, creditsPanel;
    public TMP_Text title;
    public TMP_Text[] menuItems;
    public Slider[] sliders;

    private ArrayList sliderImages;
    private MenuDialogue menuDialogue;

    private void Awake() {
        sliderImages = new ArrayList();
        Color32 titleColor = title.color;
        title.color = new Color32(titleColor.r,titleColor.g,titleColor.b,0);
        foreach(TMP_Text text in menuItems) {
            Color32 color = text.color;
            text.color = new Color32(color.r,color.g,color.b,0);
        }
        foreach(Slider slider in sliders) {
            sliderImages.AddRange(slider.GetComponentsInChildren<Image>());
        }
        foreach(Image image in sliderImages) {
            Color32 color = image.color;
            image.color = new Color32(color.r,color.g,color.b,0);
            Debug.Log(image.name + " " + image.color.a);
        }
        instructionPanelA.SetActive(false);
        instructionPanelB.SetActive(false);
        creditsPanel.SetActive(false);
       
    }

    private void Start() {
        menuDialogue = GetComponentInChildren<MenuDialogue>();
    }

    public void MenuPanelOff() {
        menuPanel.SetActive(false);
    }

    private IEnumerator FadeMenuItems(float startTime) {
        yield return new WaitForFixedUpdate();
        Debug.Log("ShowMenItems");
        foreach(TMP_Text text in menuItems) {
            Color o_Color = text.color;
            float alpha = Mathf.Lerp(0,1,(Time.time - startTime) / 1f);
            text.color = new Color(o_Color.r,o_Color.g,o_Color.b,alpha);
        }
        foreach(Image image in sliderImages) {
            Color o_Color = image.color;
            float alpha = Mathf.Lerp(0,1,(Time.time - startTime) / 1f);
            image.color = new Color(o_Color.r,o_Color.g,o_Color.b,alpha);
        }
        if(menuItems[0] == null || menuItems[0].alpha != 1f) {
            StartCoroutine(FadeMenuItems(startTime));
        }
    }

    private IEnumerator FadeTitle(float startTime) {
        yield return new WaitForFixedUpdate();
        Color o_Color = title.color;
        float alpha = Mathf.Lerp(0,1,(Time.time - startTime) / 1f);
        title.color = new Color(o_Color.r,o_Color.g,o_Color.b,alpha);
        if(title == null || title.alpha != 1f) {
            StartCoroutine(FadeTitle(startTime));
        }
    }

    public void ShowMenItems() {
        StartCoroutine(FadeMenuItems(Time.time));

    }

    public void ShowTitle() {
        StartCoroutine(FadeTitle(Time.time));

    }

    public void StartScene() {
        menuDialogue.StartSeqeunce();
    }

    public void DisplayInstructionsPanelAorB(bool aB) {
            instructionPanelB.SetActive(!aB);
            instructionPanelA.SetActive(aB);
    }

    public void InstructionsDisplay(bool onOff) {
        menuPanel.SetActive(!onOff);
        if(onOff) {
            DisplayInstructionsPanelAorB(onOff);
        } else {
            instructionPanelB.SetActive(onOff);
            instructionPanelA.SetActive(onOff);
        }
        
    }

    public void DisplayCreditsPanel(bool onOff) {
        menuPanel.SetActive(!onOff);
        creditsPanel.SetActive(onOff);
    }
}
