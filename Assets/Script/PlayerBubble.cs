using TMPro;
using UnityEngine;
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

    [SerializeField] private GameManager gameManager;

    //[SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TopText topText;

    private string playerName = "";
    private int accessoryIndex = 0;
    private int colorIndex = 0;
    private Sprite[][] sprites = new Sprite[8][];
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

    public override void Start()
    {
        base.Start();
        
        GetComponent<SpriteRenderer>().enabled = false;
        sprites[0] = spriteAccessory1;
        sprites[1] = spriteAccessory2;
        sprites[2] = spriteAccessory3;
        sprites[3] = spriteAccessory4;
        sprites[4] = spriteAccessory5;
        sprites[5] = spriteAccessory6;
        sprites[6] = spriteAccessory7;
        sprites[7] = spriteAccessory8;
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string pName)
    {
        playerName = pName;
    }

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

    private void SetPlayerSprite(int colorIndex, int accessoryIndex)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[accessoryIndex][colorIndex];
    }

    public void SpawnPlayer()
    {
        if (playerName == "")
        {
            playerName = spriteNames[Random.Range(0, spriteNames.Length)];
        }

        topText.SetPlayerName(playerName);
        //nameText.text = playerName;
        GetComponent<SpriteRenderer>().enabled = true;
        bubbleCustom.SetActive(false);
        UnblockMovement();
    }

    public void KillPlayer()
    {
        // PLAYER DEATH SOUND
        
        gameManager.RestartGame();
        playerName = "";
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
                float rate = 1 / Time.fixedDeltaTime;
                float dfr = 60.0f / rate;
                float damage = obstacle.DamageAmount * _rb.linearVelocity.magnitude / dfr;
                if (damage > _resistance)
                {
                    BlockMovement();
                    _animator.SetTrigger("Die");
                }
                else
                {
                    Feedback(_rb.linearVelocity);
                }
            }
        }
    }
}


