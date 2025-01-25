using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomColor : MonoBehaviour
{
    public RawImage image;
    public Texture[] textures;
    public PlayerBubble bubble;

    public void HandleInputData(int val)
    {
        image.texture = textures[val];
        bubble.SetColorIndex(val);
    }
}
