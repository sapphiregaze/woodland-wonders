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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        // Ensure only one instance of player exists with DontDestroyOnLoad
        if (playerInstance == null)
        {
            playerInstance = player;
            DontDestroyOnLoad(player);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        yield return new WaitUntil(() => asyncLoad.isDone);

        GameObject targetPoint = GameObject.Find(targetTeleportPoint);
        if (targetPoint != null)
        {
            player.transform.position = targetPoint.transform.position + new Vector3(offsetX, offsetY, 0);
        }
        else
        {
            Debug.Log("Target point not found: " + targetTeleportPoint);
        }
    }
}
