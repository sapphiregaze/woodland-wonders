using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDetectPlayer : FSMDecision

{
    [Header("Config")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject triggleDialogPanel;
    // public GameObject triggleDialogPanel;
        
    
    private NPCBrain npc;
    private Animator animator;

    private void Awake() {
        npc = GetComponent<NPCBrain>();

    }
    private void Start() {
        animator = GetComponent<Animator>();
        ShowDialogTrigger(false);

    }
    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer(){
        Collider2D playerCollider = Physics2D.OverlapCircle(npc.transform.position, range, playerMask);

        if(playerCollider != null){
            npc.Player = playerCollider.transform;
            animator.SetBool("isTalking", true);
            
            ShowDialogTrigger(true);
            triggleDialogPanel.transform.position = transform.position + Vector3.up * 1.5f;



            if(Input.GetKeyDown(KeyCode.E)){
                ShowDialogTrigger(false);
                Debug.Log("show dialog~~");
                //TODO: add cat's dialog here
            }

            return true;
        }

        npc.Player = null;
        animator.SetBool("isTalking", false);
        ShowDialogTrigger(false);


        return false;   
    }

   private void OnDrawGizmosSelected() {
    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, range);
   }

   public void ShowDialogTrigger(bool value){
    triggleDialogPanel.SetActive(value);
}
}