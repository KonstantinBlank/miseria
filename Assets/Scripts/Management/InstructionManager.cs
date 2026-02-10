using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {
    
    private static bool _alreadyRead = false;
    private static Text _instructionText;
    /// <summary>
    /// the time the initial instruction is shown in seconds
    /// </summary>
    public float InitalInstructionTime = 4f;

    private void Start() {
        _instructionText = GameObject.Find("Instruction").GetComponent<Text>();
        StartCoroutine(showStartInstructions(InitalInstructionTime));
    }

    private IEnumerator showStartInstructions(float seconds) {
        _instructionText.text = "Benutze die PFEILTASTEN oder WASD um zu laufen. \nBenutze die Maus um Dich umzuschauen.";
        yield return new WaitForSeconds(seconds);
        _instructionText.text = "";
    }

    public static void ShowTalkInstructions(bool active) {
        if (_alreadyRead) {
            return;
        }
        if (active) {
            _instructionText.text = "LINKSKLICK um anzusprechen.";
        }
        else {
            _instructionText.text = "";
            _alreadyRead = true;
        }
    }
}

