using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInNpcRange : MonoBehaviour {

    private static bool _NPCInRange = false;
    private static GameObject _Npc = null;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Constants.HUMAN_TAG && _Npc == null) {
            _Npc = other.gameObject;
            Npc outline = (Npc)_Npc.GetComponentInChildren(typeof(Npc));
            if (outline) {
                outline.HighlightNpc(true);
            }
            _NPCInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == _Npc) {
            //end the dialog when walking away
            FindObjectOfType<DialogueManager>().EndDialogue(true);
            //unhighlight the npc when walking away
            Npc outline = (Npc)_Npc.GetComponentInChildren(typeof(Npc));
            if (outline) {
                outline.HighlightNpc(false);
            }
            _Npc = null;
            _NPCInRange = false;
        }
    }

    public static GameObject getCurrentNpc() {
        return _Npc;
    }

    public static bool IsInRange() {
        return _NPCInRange;
    }
}
