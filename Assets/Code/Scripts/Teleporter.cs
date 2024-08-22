using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string targetScene;
    public string targetTeleportPoint;
    public float offsetX;
    public float offsetY;

    private static GameObject playerInstance;
    private static string pendingTargetPoint;
    private static Vector3 pendingOffset;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InitiateTeleport(other.gameObject, targetScene, targetTeleportPoint, new Vector3(offsetX, offsetY, 0));
        }
    }

    public static void InitiateTeleport(GameObject player, string scene, string targetPoint, Vector3 offset)
    {
        if (playerInstance == null)
        {
            playerInstance = player;
            DontDestroyOnLoad(player);
        }

        pendingTargetPoint = targetPoint;
        pendingOffset = offset;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(scene);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Find any Teleporter in the new scene to start the positioning coroutine
        Teleporter newTeleporter = FindObjectOfType<Teleporter>();
        if (newTeleporter != null)
        {
            newTeleporter.StartCoroutine(PositionPlayerAfterLoad());
        }
        else
        {
            Debug.LogError("No Teleporter found in the new scene!");
        }

        // Remove any duplicate player objects
        RemoveDuplicatePlayers();
    }

    private static IEnumerator PositionPlayerAfterLoad()
    {
        yield return null; // Wait for the next frame to ensure all objects are initialized

        Debug.Log("Attempting to find target point: " + pendingTargetPoint);
        GameObject targetPoint = GameObject.Find(pendingTargetPoint);

        if (targetPoint != null && playerInstance != null)
        {
            playerInstance.transform.position = targetPoint.transform.position + pendingOffset;
            Debug.Log("Player teleported to: " + playerInstance.transform.position);
        }
        else
        {
            Debug.Log("Target point not found: " + pendingTargetPoint + " or player instance is null");
        }

        // Clear pending data
        pendingTargetPoint = null;
        pendingOffset = Vector3.zero;
    }

    private static void RemoveDuplicatePlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player != playerInstance)
            {
                Debug.Log("Destroying duplicate player: " + player.name);
                Destroy(player);
            }
        }
    }
}