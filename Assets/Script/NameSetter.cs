using System;
using TMPro;
using UnityEngine;

public class NameSetter : MonoBehaviour
{
    [SerializeField] private string beforeNameText;
    [SerializeField] private string afterNameText;
    public string playername;
    [SerializeField] private PlayerBubble playerBubble;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = beforeNameText + playerBubble.GetPlayerName() + afterNameText;
    }

    public void ModifyTextPlayer(string text)
    {
        GetComponent<TextMeshProUGUI>().text = beforeNameText + text + afterNameText;
    }
}
