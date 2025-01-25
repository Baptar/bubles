using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

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
    
    //Damage
    [SerializeField] private float _resistance;
    private Vector3 _hitLastPos;
    private bool _hasLastPos;

    private Rigidbody2D _rb;

    public override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("triggerrrrr");
        if (other.CompareTag("Obstacle"))
        {
            print("ezuohfiuezuhf");
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            if (obstacle)
            {
                if (!_hasLastPos)
                {
                    _hitLastPos = transform.position;
                    _hasLastPos = true;
                }
                else
                {
                    Vector2 velocity = transform.position - _hitLastPos;
                    float damage = obstacle.DamageAmount * velocity.magnitude;
                    if(damage > _resistance)
                        KillPlayer();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            print("uzeugfiyezgbf");
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            if (obstacle)
            {
                float damage = obstacle.DamageAmount * _rb.linearVelocity.magnitude;
                if (damage > _resistance)
                    KillPlayer();
            }
        }
    }
}

