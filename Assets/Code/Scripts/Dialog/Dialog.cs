using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class Dialog
// {
//     public string name;

//     [TextArea(3, 10)]
//     public string[] sentences;
// }
[System.Serializable]
public class Dialog
{
    [System.Serializable]
    public class DialogLine
    {
        public string speakerName;
        [TextArea(3, 10)]
        public string sentence;
    }

    public DialogLine[] lines;
}