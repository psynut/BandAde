using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMaterial : MonoBehaviour {
    public Material material;
    public Vector2 ScaleMin, ScaleMax;
    public string textureName = "_MainTex";
    [Tooltip("How many times to iterate the animation before switching back")]
    public float period = 5f;

    private Vector2 originalScale;
    private float increment = 0;
    private bool forwardBack = true;

    private void OnEnable() {
        originalScale = material.GetTextureOffset(textureName);
    }

    private void Start() {
        StartCoroutine(MoveMaterial());
    }

    private IEnumerator MoveMaterial() {

        Vector2 uvScale = new Vector2(Mathf.Lerp(ScaleMin.x,ScaleMax.x,increment / period),Mathf.Lerp(ScaleMin.y,ScaleMax.y,increment/period));
        if(forwardBack) {
            increment += 1f;
        } else {
            increment -= 1f;
        }
        if(increment < 0  || increment > period) {
            forwardBack = !forwardBack;
        }
        material.SetTextureOffset(textureName,uvScale);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(MoveMaterial());
    }

    private void OnDisable() {
        material.SetTextureOffset(textureName,originalScale);
    }
}
