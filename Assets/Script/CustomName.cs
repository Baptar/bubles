using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomName : MonoBehaviour
{
    public PlayerBubble playerBubble;
    [SerializeField] private string inputText;

    public void GrabFromInputField(string input)
    {
        inputText = input;
        playerBubble.SetPlayerName(input);
    }
}
