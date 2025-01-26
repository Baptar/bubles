using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
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
    [SerializeField] private Sprite[] spriteAccessory6;
    [SerializeField] private Sprite[] spriteAccessory7;
    [SerializeField] private Sprite[] spriteAccessory8;
    [SerializeField] private GameObject _missionSuccessPoint;
    [SerializeField] private GameObject killCounntText;
    [SerializeField] private TextMeshProUGUI nameText;
    
    private string name = "";
    private int accessoryIndex = 0;
    private int colorIndex = 0;
    private Sprite[][] sprites = new Sprite[5][];
    private string[] spriteNames = { "Pablo", "Bap", "Luna", "Seb", "Virgile", "Justine", "Nat", "Daph" };
    private int killCount = 0;
    
    //Damage
    [SerializeField] private float _resistance;
    private Vector3 _hitLastPos;
    private bool _hasLastPos;
    private Animator _animator;

    public override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
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
    
    public void IncreaseKillCount()
    {
        killCount++;
    }

    public string GetPlayerName()
    {
        return name;
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
        if (name == "")
        {
            name = spriteNames[Random.Range(0, spriteNames.Length)];
            nameText.text = name;
        }
        GetComponent<SpriteRenderer>().enabled = true;
        bubbleCustom.SetActive(false);
        UnblockMovement();
    }

    public void KillPlayer()
    {
        name = "";
        SetAccessoryIndex(0);
        IncreaseKillCount();
        killCounntText.GetComponent<TextMeshProUGUI>().text = killCount.ToString();
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = spawnTransform.position;
        bubbleCustom.SetActive(true);
    }

    public void ChangeSpeed(float multiplier)
    {
        _moveStrength *= multiplier;
    }

    public void ChangeGravity(float multiplier)
    {
        _rb.gravityScale *= multiplier;
    }

    public void ChangeResistance(float multiplier)
    {
        _resistance *= multiplier;
    }

    public void OnMissionSuccess()
    {
        BlockMovement();
        transform.position = _missionSuccessPoint.transform.position;
        UnblockMovement();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            if (obstacle)
            {
                float damage = obstacle.DamageAmount * _rb.linearVelocity.magnitude / Time.deltaTime / 100f;
                if (damage > _resistance)
                {
                    BlockMovement();
                    _animator.SetTrigger("Die");
                }
            }
        }
    }
}

