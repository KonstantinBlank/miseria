using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    public float FadeSpeed = 10f;

    public bool FadeOut = false;
    public bool FadeIn = false;

    private Color TempColor;


    void Start() {
        TempColor = this.GetComponent<Image>().color;
        FadeInNow();
    }


    /// <summary>
    /// Turn the lights back on.
    /// </summary>
    public void FadeInNow() {
        FadeIn = true;
    }

    /// <summary>
    /// Turn the lights off.
    /// </summary>
    public void FadeOutNow() {
        FadeOut = true;
    }

    void Update() {
        if (FadeOut == true) {
            TempColor.a += Time.deltaTime * FadeSpeed;
            if (TempColor.a >= 1) {
                FadeOut = false;
            }
        }

        if (FadeIn == true) {
            TempColor.a -= Time.deltaTime * FadeSpeed;
            if (TempColor.a <= 0) {
                FadeIn = false;
            }
        }

        this.GetComponent<Image>().color = TempColor;
    }
}
