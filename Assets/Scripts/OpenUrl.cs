using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUrl : MonoBehaviour {

	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate { Application.OpenURL("https://mittel-los.de/hingehen-wo-es-fehlt/"); });
	}
}
