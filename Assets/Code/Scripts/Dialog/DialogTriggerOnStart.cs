using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTriggerOnStart : MonoBehaviour
{
    public Dialog dialog;

    // This method is called when the scene starts
    void Start()
    {
        // Get the current scene name as a unique key
        string sceneName = SceneManager.GetActiveScene().name;
        string sceneVisitKey = "Visited_" + sceneName;

        // Check if the scene has been visited before
        if (!PlayerPrefs.HasKey(sceneVisitKey))
        {
            // If not visited, trigger the dialog and set the flag
            StartCoroutine(TriggerDialogWithDelay(0.5f));
            PlayerPrefs.SetInt(sceneVisitKey, 1);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator TriggerDialogWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 1 second
        TriggerDialogOnStart();
    }

    public void TriggerDialogOnStart()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

}
