using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public GameObject PhaseI;
    public GameObject PhaseII;
    public GameObject PhaseIII;
    public GameObject PhaseIV;

    public void PlayPhaseI() {
        //PhaseII.SetActive(false);
        PhaseI.SetActive(true);
    }

    public void PlayPhaseII() {
        //PhaseII.SetActive(true);
        //PhaseIII.SetActive(false);
        //PhaseIV.SetActive(false);
    }

    public void PlayPhaseIII() {
        //PhaseIII.SetActive(true);
        //PhaseIV.SetActive(false);
    }
    public void PlayPhaseIV() {
        PhaseI.SetActive(false);
        PhaseIV.SetActive(true);
    }

    public void StopMusic() {
        PhaseI.SetActive(false);
        PhaseII.SetActive(false);
        PhaseIII.SetActive(false);
        PhaseIV.SetActive(false);
    }
}
