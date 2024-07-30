using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDetectPlayer : FSMDecision

{
    [Header("Config")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerMask;
    private NPCBrain npc;
    private void Awake() {
        npc = GetComponent<NPCBrain>();
    }
    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer(){
        Collider2D playerCollider = Physics2D.OverlapCircle(npc.transform.position, range, playerMask);

        if(playerCollider != null){
            npc.Player = playerCollider.transform;
            return true;
        }

        npc.Player = null;
        return false;   
    }

   private void OnDrawGizmosSelected() {
    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, range);
   }
}
