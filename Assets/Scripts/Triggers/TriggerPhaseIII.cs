using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhaseIII : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        CutSceneManager.CurrentPhase = Constants.PHASE_III;
        MusicManager musicManager = (MusicManager) GameObject.Find("Musicmanager").GetComponent(typeof(MusicManager));
        musicManager.PlayPhaseIII();
        Debug.Log("PhaseIII");
    }
}
