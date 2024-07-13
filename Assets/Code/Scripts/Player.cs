using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Find out why when movement in unity is set to 1 player moves at Speed of light
    //Variables
    [SerializeField]
    private  float movement_speed = 1;
    private BoxCollider2D box_collider;
    private Animator animator;
    
    //Methods
    private void Start(){
        box_collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();    
    }

    private void Update(){
        move_player();
    }

//Method for player movement and Collision with NPC's and Blocks(walls, trees)
    private void move_player(){
        //X,y
        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");
        //Vector Storage
        Vector2 move_change = new Vector2(move_x, move_y);
        
        //NEED TO DO:
        //Animate Player according to movement.
        //Create individual sprites for anmating based on each movement direction.

        //Collision
        RaycastHit2D cast_result;
        //Horizontal Axis Collison Check
        cast_result = Physics2D.BoxCast(transform.position, box_collider.size, 0, new Vector2(move_x, 0), Mathf.Abs(move_x * Time.fixedDeltaTime * movement_speed), LayerMask.GetMask("NPC","BlockMove"));
        if (cast_result.collider){
            move_change.x = 0;
            
        }
        //Vertical Axis Collison Check
        cast_result = Physics2D.BoxCast(transform.position, box_collider.size, 0, new Vector2(0, move_y), Mathf.Abs(move_y * Time.fixedDeltaTime * movement_speed), LayerMask.GetMask("NPC", "BlockMove"));
        if (cast_result.collider){
            move_change.y = 0;
        }
       
        //Apply the movement
        transform.Translate(move_change * Time.fixedDeltaTime * movement_speed);
    

        // Player Movement Animations
        if (move_change == Vector2.zero){
            animator.SetBool("Moving", false);
        }else{
             animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", move_change.x);
            animator.SetFloat("MoveY", move_change.y);
        }   
    }
}
