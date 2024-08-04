using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDetectPlayer : FSMDecision

{
    [Header("Config")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject prefab;
    
    private NPCBrain npc;
    private Animator animator;
    private GameObject newInstance;

    private void Awake() {
        npc = GetComponent<NPCBrain>();

    }
    private void Start() {
        animator = GetComponent<Animator>();
        newInstance = CreateInstance();

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


   private GameObject CreateInstance(){
    GameObject newInstance = Instantiate(prefab);
    Canvas canvas = FindObjectOfType<Canvas>();

    if (canvas != null) {
            newInstance.transform.SetParent(canvas.transform, false); 
        } else {
            Debug.LogWarning("Canvas not found!");
        }
    newInstance.transform.position = npc.transform.position + Vector3.up * 1.5f;
    newInstance.SetActive(false);
    return newInstance;
   }

   private void LateUpdate() {
        if (newInstance != null) {
            newInstance.transform.position = npc.transform.position + Vector3.up * 1.5f;
        }
    }

    public void ShowDialogTrigger(bool value){
        newInstance.SetActive(value);
    }

   
}