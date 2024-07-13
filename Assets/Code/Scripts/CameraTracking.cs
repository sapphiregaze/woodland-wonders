using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    //Vars
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float x_limit = 0.1f;
    [SerializeField]
    private float y_limit = 0.2f;

    //Methods

    private void LateUpdate(){
        FollowPlayer();
    }
    private void FollowPlayer(){
        Vector2 move_direction = Vector2.zero;

        float x_change = player.position.x - transform.position.x;
        float y_change = player.position.y - transform.position.y;

        if(x_change > x_limit || x_change < -x_limit){
            if(player.position.x > transform.position.x){
                move_direction.x = x_change - x_limit;
            }
            else{
                move_direction.x = x_change + x_limit;
            }
        }

        if(y_change > y_limit || y_change < -y_limit){
            if(player.position.y > transform.position.y){
                move_direction.y = y_change - y_limit;
            }
            else{
                move_direction.y = y_change + y_limit;
            }
        }

         transform.position += new Vector3(move_direction.x, move_direction.y, 0);
    }
}
