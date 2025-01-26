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
    public void SetPlayerName(string playerName)
    {
        /*GetComponent<TextMeshProUGUI>().text = playerName;
        GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;*/
        
        rawImageBefore.transform.localPosition = new Vector3(-rectTransform.rect.width / 2 - 25, 15.9f, 0);
        rawImageAfter.transform.localPosition = new Vector3(rectTransform.rect.width / 2 + 25, 15.9f, 0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetAge(int age)
    {
        rawImageBefore.texture = leftTextures[age];
        rawImageAfter.texture = rightTextures[age];
    }

    private void Update()
    {
        //Debug.Log(GetComponent<RectTransform>().rect.width);
    }
}
