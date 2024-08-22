// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class DialogManager : MonoBehaviour
// {
//     public TextMeshProUGUI nameText;
//     public TextMeshProUGUI dialogText;

//     public Animator animator;

//     // private Queue<string> sentences;

//     // // Start is called before the first frame update
//     // void Start()
//     // {
//     //     sentences = new Queue<string>();
//     // }

//     // public void StartDialog(Dialog dialog)
//     // {
//     //     animator.SetBool("IsOpen", true);
//     //     nameText.text = dialog.name;

//     //     sentences.Clear();

//     //     foreach (string sentence in dialog.sentences)
//     //     {
//     //         sentences.Enqueue(sentence);
//     //     }

//     //     DisplayNextSentence();
//     // }

//     // public void DisplayNextSentence()
//     // {
//     //     if (sentences.Count == 0)
//     //     {
//     //         EndDialog();
//     //         return;
//     //     }

//     //     string sentence = sentences.Dequeue();
//     //     StopAllCoroutines();
//     //     StartCoroutine(TypeSentence(sentence));
//     // }

//         private Queue<Dialog.DialogLine> dialogLines;

//     void Start()
//     {
//         dialogLines = new Queue<Dialog.DialogLine>();
//     }

//     private void Update() {
//         Debug.Log(dialogLines.Count);
//         if(animator.GetBool("IsOpen") && Input.GetKeyDown(KeyCode.Space))
//         {
//             if(dialogLines.Count == 0) {
//                 EndDialog();
//             }else{
//                 DisplayNextSentence();
//             }
//         }
//     }

//     public void StartDialog(Dialog dialog)
//     {
//         animator.SetBool("IsOpen", true);

//         dialogLines.Clear();

//         foreach (Dialog.DialogLine line in dialog.lines)
//         {
//             dialogLines.Enqueue(line);
//         }

//         DisplayNextSentence();
//     }

//     public void DisplayNextSentence()
//     {
//         if (dialogLines.Count == 0)
//         {
//             dialogText.text = " ";
//             // EndDialog();
//             dialogText.text = "Press 'Space' to close.";

//             return;
//         }

//         Dialog.DialogLine line = dialogLines.Dequeue();
//         nameText.text = line.speakerName;
//         StopAllCoroutines();
//         StartCoroutine(TypeSentence(line.sentence));

//     }


//     IEnumerator TypeSentence(string sentence)
//     {
//         dialogText.text = "";
//         foreach (char letter in sentence.ToCharArray())
//         {
//             dialogText.text += letter;
//             yield return new WaitForSeconds(0.05f);
//         }
//     }

//     void EndDialog()
//     {
//         animator.SetBool("IsOpen", false);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Animator animator;

    private Queue<Dialog.DialogLine> dialogLines;

    void Start()
    {
        dialogLines = new Queue<Dialog.DialogLine>();
    }

    private void Update()
    {
        if (animator.GetBool("IsOpen") && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogLines.Count == 0 && dialogText.text == "")
            {
                EndDialog();  // Close the dialog panel when the player presses space after the conversation is over
            }
            else
            {
                DisplayNextSentence();  // Continue to the next sentence
            }
        }
    }

    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("IsOpen", true);

        dialogLines.Clear();

        foreach (Dialog.DialogLine line in dialog.lines)
        {
            dialogLines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogLines.Count == 0)
        {
            dialogText.text = "";
            return;
        }

        Dialog.DialogLine line = dialogLines.Dequeue();
        nameText.text = line.speakerName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        // yield return null; 
            Debug.Log("Sentence to display: " + sentence);

        foreach (char letter in sentence.ToCharArray())
        {   
             
            dialogText.text += letter;

            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialog()
    {
        dialogText.text = "";
        animator.SetBool("IsOpen", false);
    }
}
