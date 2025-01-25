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
    
    [SerializeField] private MissionTrigger[] missionTriggers;
    [SerializeField] private GameObject[] missionDescription;
    
    private int age = 0;
    private int actualMission = 0;
    private bool[] randomMissionsProgress;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomMissionsProgress = new bool[missionTriggers.Length];
    }

    private int SelectRandomMission()
    {
        // Calculate number of available events
        int cpt = 0;
        for (int i = 0; i < randomMissionsProgress.Length; i++)
        {
            if (!randomMissionsProgress[i])
            {
                cpt++;
            }
        }
        
        // case we don't have any more event available
        if (cpt == 0)
        {
            return Random.Range(0, randomMissionsProgress.Length);
        }
        
        // create array of number available missions
        int[] indexUnused = new int[cpt];
        int index = 0;
        for (int i = 0; i < randomMissionsProgress.Length; i++)
        {
            if (!randomMissionsProgress[i])
            {
                indexUnused[index] = i;
                index++;
            }
        }
        
        // get random event in array of available events 
        int rep = indexUnused[Random.Range(0, indexUnused.Length)];
        randomMissionsProgress[rep] = true;
        return rep;
    }

    public void ShowRandomMission()
    {
        int i = SelectRandomMission(); 
        missionDescription[i].SetActive(true);
        missionTriggers[i].gameObject.SetActive(true);
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
        // LANCER LA PREMIERE MISSION/tuto APRES LE SPAWN (quand on appuie sur bouton)
    }

    public void GameWin()
    {
        
    }
}
