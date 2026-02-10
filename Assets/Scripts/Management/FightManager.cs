using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class FightManager : MonoBehaviour {

    private static bool _helped = false;

    public float LetterDelay = 0.04f;

    public GameObject Attacker;
    public GameObject Victim;

    public Button ButtonOne;
    public Button ButtonTwo;

    public IEnumerator ShowOptions() {
        if (!_helped) {
            FirstPersonController.StunPlayer(true);
            FirstPersonController.FreezePlayer(true);

            Button currentButton = ButtonOne;
            string currentAnswer = "Helfen (visuals in progress)";
            for (int i = 0; i < 2; i++) {
                currentButton.gameObject.SetActive(true);
                Text textObject = currentButton.GetComponentInChildren<Text>();
                textObject.color = Color.white;

                foreach (char letter in currentAnswer.ToCharArray()) {
                    textObject.text += letter;
                    yield return new WaitForSecondsRealtime(LetterDelay);
                }
                currentButton = ButtonTwo;
                currentAnswer = "Weitergehen";
            }
            ButtonOne.onClick.AddListener(() => help());
            ButtonTwo.onClick.AddListener(() => goAlong());
            ButtonOne.interactable = true;
            ButtonTwo.interactable = true;
        }
    }

    private void help() {
        _helped = true;

        Attacker.SetActive(false);
        Victim.SetActive(false);

        clearButtons();

        FirstPersonController.FreezePlayer(false);
        FirstPersonController.StunPlayer(false);
    }

    private void goAlong() {
        clearButtons();
        FirstPersonController.FreezePlayer(false);
        FirstPersonController.StunPlayer(false);
    }

    private void clearButtons() {
        clearButton(ButtonOne);
        clearButton(ButtonTwo);
    }

    private void clearButton(Button button) {
        button.GetComponentInChildren<Text>().text = "";
        button.onClick.RemoveAllListeners();
        button.interactable = false;
        button.gameObject.SetActive(false);
    }

    public static bool helpedNpc() {
        return _helped;
    }
}
