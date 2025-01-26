using System;
using TMPro;
using UnityEngine;

public class NameSetter : MonoBehaviour
{
    [SerializeField] private PlayerBubble playerBubble;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = playerBubble.GetPlayerName();
    }
}
