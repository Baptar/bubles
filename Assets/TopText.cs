using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopText : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBefore;
    [SerializeField] private RawImage rawImageAfter;
    
    [SerializeField] private Texture[] leftTextures;
    [SerializeField] private Texture[] rightTextures;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private PlayerBubble playerBubble;
    
    public void SetPlayerName(string playerName, bool modifImagePosition)
    {
        playerBubble.SetPlayerName(playerName);
        GetComponent<TextMeshProUGUI>().text = playerName;
        if (modifImagePosition)
        {
            rawImageBefore.transform.localPosition = new Vector3(-rectTransform.rect.width / 2 - 43, 1.0f, 0);
            rawImageAfter.transform.localPosition = new Vector3(rectTransform.rect.width / 2 + 43, 1.0f, 0);
        }
        else
        {
            ResetPositionImage();
        }
        
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetAge(int age)
    {
        rawImageBefore.texture = leftTextures[age];
        rawImageAfter.texture = rightTextures[age];
    }

    public void ResetPositionImage()
    {
        rawImageBefore.transform.localPosition = new Vector3(-128, 1.0f, 0);
        rawImageAfter.transform.localPosition = new Vector3(128, 1.0f, 0);
    }
}
