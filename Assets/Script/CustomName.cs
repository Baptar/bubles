using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomName : MonoBehaviour
{
    public PlayerBubble playerBubble;
    [SerializeField] private string inputText;
    [SerializeField] private TextMeshProUGUI textMesh;

    public void GrabFromInputField(string input)
    {
        inputText = input;
        playerBubble.SetPlayerName(input);
        textMesh.text = input;
    }
}
