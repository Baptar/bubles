using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numberMissionChild = 5;
    [SerializeField] private int numberMissionTeenager = 3;
    [SerializeField] private PlayerBubble playerBubble;
    
    private int age = 0;
    private int actualMissionChild = 0;
    private int actualMissionTeenager = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAge(int age)
    {
        this.age = age;
        
    }
    
    public void RestartGame()
    {
        SetAge(0);
        actualMissionChild = 0;
        actualMissionTeenager = 0;
        playerBubble.KillPlayer();
    }
}
