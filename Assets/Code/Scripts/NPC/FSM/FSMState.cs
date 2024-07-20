using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;

[Serializable]
public class FSMState
{
   public string ID;
   public FSMAction[] Actions;
   public FSMTransition[] Transitions;

   public void UpdateState(NPCBrain npcBrain){
    ExecuteActions();
    ExecuteTransitions(npcBrain);
   }
   
   private void ExecuteActions(){
    for (int i = 0; i<Actions.Length;i++){
        Actions[i].Act();
    }
   }

   private void ExecuteTransitions(NPCBrain npcBrain){
    if(Transitions == null || Transitions.Length <= 0) return;

    for (int i = 0; i<Transitions.Length; i++){
        bool value = Transitions[i].Decision.Decide();
        if(value){
            npcBrain.ChangeState(Transitions[i].TrueState);
        }else{
            npcBrain.ChangeState(Transitions[i].FalseState);
        }
    }
   }
}
