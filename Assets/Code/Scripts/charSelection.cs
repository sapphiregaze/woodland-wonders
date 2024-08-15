using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public Image previewImage;
    public Sprite[] spriteOptions;
    public Button[] selectionButtons;
    public Button confirmButton;
    public Button resetButton;

    private int selectedIndex = 0;

    private void Start()
    {
        if (previewImage == null)
        {
            Debug.LogError("Preview image is not assigned.");
            return;
        }

        if (spriteOptions == null || spriteOptions.Length == 0)
        {
            Debug.LogError("Sprite options are empty/not assigned.");
            return;
        }

        if (selectionButtons == null || selectionButtons.Length == 0)
        {
            Debug.LogError("Selection Buttons not assigned/empty.");
            return;
        }
        
        if (resetButton == null)
        {
            Debug.LogError("Reset button not assigned.");
            return;
        }
        if (spriteOptions.Length > 0)
        {
            previewImage.sprite = spriteOptions[0];
        }

        for (int i = 0; i < selectionButtons.Length; i++)
        {
            int index = i;
            selectionButtons[i].onClick.AddListener(()=> OnSelectionButtonClicked(index));
        }

        confirmButton.onClick.AddListener(OnConfirmSelection);
        resetButton.onClick.AddListener(OnResetSelection);
    }

    public void OnSelectionButtonClicked(int index)
    {
        //if (spriteIndex >= 0 && spriteIndex < spriteOptions.Length)
        if (index >= 0 && index < spriteOptions.Length)
        {
            previewImage.sprite = spriteOptions[index];
            selectedIndex = index;
            
        }
    }
    
    private void OnConfirmSelection()
    {
        if (selectedIndex >= 0 && selectedIndex < spriteOptions.Length)
        {
            PlayerPrefs.SetInt("selectedCharacter", selectedIndex);
            PlayerPrefs.Save();
        }
    }
    private void OnResetSelection()
    {
        selectedIndex = 0;
        previewImage.sprite = spriteOptions[selectedIndex];
    }
}