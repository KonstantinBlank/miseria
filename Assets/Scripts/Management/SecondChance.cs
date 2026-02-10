using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SecondChance : MonoBehaviour {

    public Button Option1;
    public Button Option2;
    public Button SecondTryButton;

    public int WaitTime = 2;
    public float LetterDelay = 0.04f;

    public string[] Answers;
    private bool saidSth = false;

    private int Counter = 0;
    private bool _waited = false;


    private void OnEnable() {
        Counter = 0;
        saidSth = false;
        _waited = true;
    }

    private void Update() {
        if (_waited && BorderHelperTrigger.IsWatchingToStreet()) {
            _waited = false;
            StartCoroutine(typeOptions());
        }
    }


    private IEnumerator typeOptions() {
        Button currentButton = Option1;
        string currentAnswer = Answers[Counter];
        if (Answers.Length - 1 == Counter) {
            Counter = 0;
        }
        else {
            Counter++;
        }
        for (int i = 0; i < 2; i++) {
            bool secondChanceEnabled = false;
            if (i == 1 && saidSth && !SecondTryButton.gameObject.activeSelf) {
                currentButton = SecondTryButton;
                currentAnswer = "2) Zweite Chance";
                i--;
                secondChanceEnabled = true;
            }
            currentButton.gameObject.SetActive(true);
            Text textObject = currentButton.GetComponentInChildren<Text>();
            textObject.color = Color.white;
            foreach (char letter in currentAnswer.ToCharArray()) {
                textObject.text += letter;
                yield return new WaitForSecondsRealtime(LetterDelay); //WaitForOneFrame
            }
            currentButton = Option2;
            currentAnswer = "2) Nichts sagen";
            if (secondChanceEnabled) {
                currentAnswer = "3) Nichts sagen";
            }
            
        }

        FirstPersonController.FreezePlayer(true);
        if (!saidSth) {
            Option1.onClick.AddListener(delegate { StartCoroutine(wait()); saidSth = true; moveButtonOne(true); });
        }
        else {
            Option1.onClick.AddListener(delegate { StartCoroutine(wait()); });
            SecondTryButton.onClick.AddListener(startSecondChance);
            SecondTryButton.interactable = true;
        }
        Option2.onClick.AddListener(delegate { StartCoroutine(wait()); });

        Option1.interactable = true;
        Option2.interactable = true;
    }

    private void moveButtonOne(bool up) {
        Vector3 pos = Option1.gameObject.transform.position;
        if (up && saidSth) {
            pos.y += 35;
        }
        else {
            pos.y -= 35;
        }
        Option1.gameObject.transform.position = pos;
    }

    private void startSecondChance() {
        moveButtonOne(false);
        CutSceneManager cutSceneManager = (CutSceneManager)gameObject.GetComponent(typeof(CutSceneManager));
        StartCoroutine(cutSceneManager.SetPlayerOnStreet());
        clearButtons();
    }

    private IEnumerator wait() {
        FirstPersonController.FreezePlayer(false);
        clearButtons();
        yield return new WaitForSeconds(WaitTime);
        _waited = true;
    }


    private void clearButtons() {
        clearButton(Option1);
        clearButton(Option2);
        clearButton(SecondTryButton);
    }

    private void clearButton(Button button) {
        button.GetComponentInChildren<Text>().text = "";
        button.onClick.RemoveAllListeners();
        button.interactable = false;
        button.gameObject.SetActive(false);
    }
}
