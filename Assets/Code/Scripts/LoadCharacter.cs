using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public TMP_Text label;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter",0);
        if (selectedCharacter >= 0 && selectedCharacter < charcterPrefabs.Length)
        {
            GameObject prefab = characterPrefabs[selectedCharacter];
            GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            label.text = prefab.name;
        }
        
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
