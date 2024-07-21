using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;
    public Vector3 EntityPosition { get; set; }
    private bool gameStarted;

    private void Start() {
        EntityPosition = transform.position;
        gameStarted = true;
    }

    public Vector3 GetPosition(int pointIndex){
        return EntityPosition + points[pointIndex];  
    }
    private void OnDrawGizmos() {
        if(gameStarted == false && transform.hasChanged){
            EntityPosition = transform.position;
        }
    }
}
