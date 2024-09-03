using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistence
{
    // Variables
    [SerializeField]
    private float movement_speed = 1f;
    [SerializeField]
    private float boundary_offset_x = 0.01f;
    [SerializeField]
    private float boundary_offset_y = 0.03f;

    private BoxCollider2D box_collider;
    private Animator animator;
    private Camera main_camera;

    private string scene;
    private Vector2 playerPosition;

    //added for character sprites
    [SerializeField]
    private Sprite[] characterSprites;
    private SpriteRenderer spriteRenderer;

    // Methods
    private void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); //starts SpriteRenderer
        UpdateCameraReference();
        ApplySelectedCharacter(); //applies the selected char sprite
    }

    private void Update()
    {
        if (main_camera == null)
        {
            UpdateCameraReference();
        }
        MovePlayer();

        this.scene = SceneManager.GetActiveScene().name;
        this.playerPosition = transform.position;
    }

    public void UpdateCameraReference()
    {
        main_camera = Camera.main;
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
        this.playerPosition = data.playerPosition;

        if (data.scene != this.scene)
        {
            SceneManager.LoadScene(data.scene);
        }
        transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.scene = this.scene;
        data.playerPosition = this.playerPosition;
    }

    //gets selected index from player prefs, applies selected sprites or defaults when no selection
   private void ApplySelectedCharacter()
   {
        int selectedIndex = PlayerPrefs.GetInt("selectedCharacter", 0);
        Debug.Log($"Applying character with index: {selectedIndex}");
        if (selectedIndex >= 0 && selectedIndex < characterSprites.Length)
        {
            spriteRenderer.sprite = characterSprites[selectedIndex];
        }
        else
        {
            Debug.LogWarning("Selected index is out of bounds. Applying default sprite.");
            spriteRenderer.sprite = characterSprites[0];
        }
    }
}
