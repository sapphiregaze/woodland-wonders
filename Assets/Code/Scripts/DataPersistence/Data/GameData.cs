using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string scene;
    public Vector2 playerPosition;

    public GameData()
    {
        this.scene = "Forest";
        this.playerPosition = new Vector2(-12f, 0f);
    }
}
