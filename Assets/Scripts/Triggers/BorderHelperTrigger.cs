using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderHelperTrigger : MonoBehaviour {

    private static bool _watchingToStreet = true;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constants.PLAYER_TAG)) {
            _watchingToStreet = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(Constants.PLAYER_TAG)) {
            _watchingToStreet = false;
        }
    }

    public static bool IsWatchingToStreet() {
        return _watchingToStreet;
    }
}
