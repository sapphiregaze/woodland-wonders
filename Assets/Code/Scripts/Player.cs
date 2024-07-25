using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistence
{
    // Variables
    [SerializeField]
    private float movement_speed = 0.25f;
    [SerializeField]
    private float boundary_offset_x = 0.01f;
    [SerializeField]
    private float boundary_offset_y = 0.03f;

    private BoxCollider2D box_collider;
    private Animator animator;
    private Camera main_camera;

    private string scene;
    private float positionX;
    private float positionY;

    // Methods
    private void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        main_camera = Camera.main;  // Get the main camera
    }

    private void Update()
    {
        MovePlayer();

        this.scene = SceneManager.GetActiveScene().name;

        Vector2 playerPosition = transform.position;
        this.positionX = playerPosition.x;
        this.positionY = playerPosition.y;
    }

    // Method for player movement and Collision with NPC's and Blocks (walls, trees)
    private void MovePlayer()
    {
        // X, y
        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");
        // Vector Storage
        Vector2 move_change = new Vector2(move_x, move_y);

        // Collision
        RaycastHit2D cast_result;
        // Horizontal Axis Collision Check
        cast_result = Physics2D.BoxCast(transform.position, box_collider.size, 0, new Vector2(move_x, 0), Mathf.Abs(move_x * Time.fixedDeltaTime * movement_speed), LayerMask.GetMask("NPC", "BlockMove"));
        if (cast_result.collider)
        {
            move_change.x = 0;
        }
        // Vertical Axis Collision Check
        cast_result = Physics2D.BoxCast(transform.position, box_collider.size, 0, new Vector2(0, move_y), Mathf.Abs(move_y * Time.fixedDeltaTime * movement_speed), LayerMask.GetMask("NPC", "BlockMove"));
        if (cast_result.collider)
        {
            move_change.y = 0;
        }

        // Apply the movement
        Vector3 new_position = transform.position + new Vector3(move_change.x, move_change.y, 0) * Time.fixedDeltaTime * movement_speed;

        // Ensure the player stays within camera bounds
        Vector3 clamped_position = ClampToCameraBounds(new_position);

        transform.position = clamped_position;

        // Player Movement Animations
        if (move_change == Vector2.zero)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", move_change.x);
            animator.SetFloat("MoveY", move_change.y);
        }
    }

    private Vector3 ClampToCameraBounds(Vector3 position)
    {
        // Get the camera bounds
        Vector3 min_screen_bounds = main_camera.ViewportToWorldPoint(new Vector3(0 + boundary_offset_x, 0 + boundary_offset_y, main_camera.transform.position.z));
        Vector3 max_screen_bounds = main_camera.ViewportToWorldPoint(new Vector3(1 - boundary_offset_x, 1 - boundary_offset_y, main_camera.transform.position.z));

        // Clamp the player's position to the camera bounds
        position.x = Mathf.Clamp(position.x, min_screen_bounds.x, max_screen_bounds.x);
        position.y = Mathf.Clamp(position.y, min_screen_bounds.y, max_screen_bounds.y);

        return position;
    }

    public void LoadData(GameData data)
    {
        this.scene = data.scene;
        this.positionX = data.positionX;
        this.positionY = data.positionY;

        if (data.scene != this.scene)
        {
            SceneManager.LoadScene(data.scene);
        }
        transform.position = new Vector2(data.positionX, data.positionY);
    }

    public void SaveData(ref GameData data)
    {
        data.scene = this.scene;
        data.positionX = this.positionX;
        data.positionY = this.positionY;
    }
}
