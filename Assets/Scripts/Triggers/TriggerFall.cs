using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TriggerFall : MonoBehaviour {

    public GameObject Gamemanger;

    private void OnTriggerEnter(Collider other) {
        if(other.tag != Constants.PLAYER_TAG) {
            return;
        }

        if (CutSceneManager.IsFirstRun() || !CutSceneManager.Helped /*|| !FightManager.helpedNpc()*/) {
            CutSceneManager cutSceneManager = (CutSceneManager)Gamemanger.GetComponent(typeof(CutSceneManager));
            cutSceneManager.LoadSecondRun();
        }
        else {
            CutSceneManager cutSceneManager = (CutSceneManager)Gamemanger.GetComponent(typeof(CutSceneManager));
            cutSceneManager.PlayCredits();
        }
    }
}
