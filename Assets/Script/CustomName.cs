using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomName : MonoBehaviour
{
    public PlayerBubble playerBubble;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    public void GrabFromInputField(string input)
    {
        playerBubble.SetPlayerName(input);
        textMesh.text = input;
    }

}
