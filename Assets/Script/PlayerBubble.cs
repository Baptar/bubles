using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerBubble : Movable
{
    [SerializeField] private GameObject bubbleCustom;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Sprite[] spriteAccessory1;
    [SerializeField] private Sprite[] spriteAccessory2;
    [SerializeField] private Sprite[] spriteAccessory3;
    [SerializeField] private Sprite[] spriteAccessory4;
    [SerializeField] private Sprite[] spriteAccessory5;
    
    private string name;
    private int accessoryIndex = 0;
    private int colorIndex = 0;
    private Sprite[][] sprites = new Sprite[5][];
    private string[] spriteNames = { "Pablo", "Bap", "Luna", "Seb", "Virgile", "Justine", "Nat", "Daph" };

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        sprites[0] = spriteAccessory1;
        sprites[1] = spriteAccessory2;
        sprites[2] = spriteAccessory3;
        sprites[3] = spriteAccessory4;
        sprites[4] = spriteAccessory5;
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
        if (name == "") { name = spriteNames[Random.Range(0, spriteNames.Length)]; }
        GetComponent<SpriteRenderer>().enabled = true;
        bubbleCustom.SetActive(false);
    }

    public void KillPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = spawnTransform.position;
        bubbleCustom.SetActive(true);
    }
}
