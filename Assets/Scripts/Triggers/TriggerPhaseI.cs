using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhaseI : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        CutSceneManager.CurrentPhase = Constants.PHASE_I;
        MusicManager musicManager = (MusicManager)GameObject.Find("Musicmanager").GetComponent(typeof(MusicManager));
        if (!CutSceneManager.IsRestarting()) {
            musicManager.PlayPhaseI();
        }
        Debug.Log("PhaseI");
    }
}
