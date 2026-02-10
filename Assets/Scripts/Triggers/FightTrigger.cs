using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour {

    private FightManager _fightManager;
    private bool _inRange = false;


    private void Start() {
        _fightManager = (FightManager)GameObject.Find("Gamemanager").GetComponent(typeof(FightManager));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Constants.PLAYER_TAG && !CutSceneManager.IsFirstRun()) {
            _inRange = true;
            StartCoroutine(_fightManager.ShowOptions());    
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == Constants.PLAYER_TAG) {
            _inRange = false;
        }
    }

    public bool IsInRange() {
        return _inRange;
    }
}
