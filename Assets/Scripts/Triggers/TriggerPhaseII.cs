using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhaseII : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        CutSceneManager.CurrentPhase = Constants.PHASE_II;
        MusicManager musicManager = (MusicManager)GameObject.Find("Musicmanager").GetComponent(typeof(MusicManager));
        musicManager.PlayPhaseII();
        Debug.Log("PhaseII");
    }
}
