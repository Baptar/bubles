using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numberMissionChild = 5;
    [SerializeField] private int numberMissionTeenager = 3;
    [SerializeField] private PlayerBubble playerBubble;
    [SerializeField] private GameObject background;
    [SerializeField] private Texture[] texturesBackground;
    [SerializeField] private EventManager eventManager;
    
    private int age = 0;
    private int actualMission = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMissionComplete()
    {
        actualMission++;
        
        // finished inboarding
        if (actualMission == 1)
        {
            eventManager.ShowSpecialSpeech(0);
        }
        
        // finished childhood
        if (actualMission == numberMissionChild)
        {
            eventManager.ShowSpecialSpeech(1);
            SetAge(1);
        }
        // finished to be teenager (win game)
        else if (actualMission == numberMissionChild + numberMissionTeenager)
        {
            eventManager.ShowSpecialSpeech(2);
            SetAge(2);
        }
        // just finished basic mission
        else
        {
            eventManager.ShowRandomEvent();
        }
    }

    public void SetAge(int age)
    {
        this.age = age;
        
        // faire la transition d'un age à un autre ICI
        
        background.GetComponent<RawImage>().texture = texturesBackground[age];
    }
    
    public void RestartGame()
    {
        SetAge(0);
        actualMission = 0;
        playerBubble.KillPlayer();
    }

    public void GameWin()
    {
        
    }
}
