using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerBubble : Movable
{
    private string name;
    private int accessoryIndex = 0;
    private int colorIndex = 0;
    
    public Sprite[][] sprites; // first tab is for accessorie and under it is color
    public Transform spawnTransform;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
    public void SetName(string name) { this.name = name; }

    public void SetColorIndex(int colorIndex)
    {
        this.colorIndex = colorIndex;
        SetPlayerSprite(colorIndex, accessoryIndex);
    }

    public void SetAccessoryIndex(int accessoryIndex)
    {
        this.accessoryIndex = accessoryIndex; 
        SetPlayerSprite(colorIndex, accessoryIndex);
    }
    
    private void SetPlayerSprite(int colorIndex, int accessoryIndex) { GetComponent<SpriteRenderer>().sprite = sprites[accessoryIndex][colorIndex]; }

    public void SpawnPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void RespawnPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = spawnTransform.position;
    }
}
