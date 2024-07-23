using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string scene;
    public float positionX;
    public float positionY;

    public GameData()
    {
        this.scene = "Forest";
        this.positionX = -900;
        this.positionY = 30;
    }
}
