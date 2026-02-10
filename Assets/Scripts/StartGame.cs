using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {


    private Button startButton;

    private void Awake() {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(loadScene);
    }

    private void loadScene() {
        SceneManager.LoadScene("SampleScene");
    }

}
