using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource CoinDrop;
    public AudioSource Ticking;
    public AudioSource Reload;
    public AudioSource Talking;
    public AudioSource Fall;
    public AudioSource FallSafe;
    public float FallTimeDelay;

    private MusicManager _musicManager;

    private void Start() {
        _musicManager = (MusicManager)GameObject.Find("Musicmanager").GetComponent(typeof(MusicManager));
    }


    public void PlayCoinDrop() {
        CoinDrop.Play();
    }

    public void PlayTicking() {
        Ticking.Play();
    }

    public void PlayTalking() {
        Talking.Play();
    }

    private void playFall() {
        Fall.Play();
    }

    private void playFallSafe() {
        FallSafe.Play();
    }


    public void PlayReload() {
        Reload.Play();
    }

    //private IEnumerator waitForReload(float time) {
    //    yield return new WaitForSeconds(time);
    //    _musicManager.PlayPhaseI();
    //    CutSceneManager.CurrentPhase = Constants.PHASE_I;
    //}

    public IEnumerator waitForFall() {
        yield return new WaitForSeconds(FallTimeDelay);
        playFall();
    }
    public IEnumerator WaitForFallSafe() {
        yield return new WaitForSeconds(FallTimeDelay);
        playFallSafe();
    }
}
