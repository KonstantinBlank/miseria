using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {

    private Transform _player;
    private bool _following = false;
    private NavMeshAgent _agent;
    private Animator _animator;

    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;

    public bool Talkable = true;



    void Start() {
        _spawnPosition = transform.localPosition;
        _spawnRotation = transform.localRotation;
        _player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        if (_agent == null) {
            Debug.Log(gameObject.name + " agent not found");
        }
    }


    void Update() {
        if (!_following) {
            return;
        }
        if (Vector3.Distance(_player.position, this.transform.position) > 5f) {
            _animator.SetBool("Stand", false);
            _agent.SetDestination(_player.position);
        }
        else {
            _animator.SetBool("Stand", true);
        }
    }

    public void Follow() {
        Talkable = false;
        _following = true;
    }
    public void ResetNpc() {
        Talkable = true;
        _following = false;
        transform.localPosition = _spawnPosition;
        transform.localRotation = _spawnRotation;
        _animator.SetBool("Stand", false);
        _animator.SetBool("StandUp", false);
        _animator.SetBool("Sit", true);
        StartCoroutine(disableAnimIn(3f));
        _agent.isStopped = true;
        _agent.ResetPath();
    }

    private IEnumerator disableAnimIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        _animator.enabled = false;
    }
}
