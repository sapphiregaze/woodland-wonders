using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWalking : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speed;

    private Waypoint waypoint;
    private int pointIndex;
    private Vector3 nextPosition;
    private bool isIncrease;
     private Vector3 originalScale;

    private void Awake() {
        waypoint = GetComponent<Waypoint>(); 
        isIncrease = true;

    }
    private void Start() {
        originalScale = transform.localScale;
    }


    public override void Act()
    {
        FollowPath();
    }

    private void FollowPath(){
        transform.position = Vector3.MoveTowards(transform.position, GetCurrentPosition(), speed * Time.deltaTime);
        if(Vector3.Distance(transform.position,GetCurrentPosition()) <=0.1f){
            UpdateNextPosition();
        }
    }

    private void UpdateNextPosition(){
        if(isIncrease){
            pointIndex++;
            if(pointIndex >= waypoint.Points.Length -1){
                isIncrease = false;
               
                
            }
        }else{
                pointIndex--;
                if (pointIndex < 0){
                    pointIndex = 0;
                    isIncrease = true;
                }
            }
    UpdateScale();
    }

    private Vector3 GetCurrentPosition(){
        return waypoint.GetPosition(pointIndex);
    }

    void UpdateScale()
    { 
        if (transform.position.x > GetCurrentPosition().x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
  
}
