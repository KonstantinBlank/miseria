using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToRandomPoint : MonoBehaviour {

    private Transform CurrentTarget;

    private int NumberOfCurrentTarget = 0;

    public Transform[] Points;


    void Start() {
        CurrentTarget = Points[NumberOfCurrentTarget];
        this.GetComponent<NavMeshAgent>().SetDestination(CurrentTarget.position);
    }


    void Update() {
        if (Vector3.Distance(CurrentTarget.position, this.transform.position) < 2f) {
            NumberOfCurrentTarget += 1;
            if (NumberOfCurrentTarget == Points.Length) {
                NumberOfCurrentTarget = 0;
            }
            CurrentTarget = Points[NumberOfCurrentTarget];
            this.GetComponent<NavMeshAgent>().SetDestination(CurrentTarget.position);
        }
    }
}
