using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numberMissionChild = 3;
    [SerializeField] private int numberMissionTeenager = 3;
    public PlayerBubble playerBubble;
    [SerializeField] private GameObject background;
    [SerializeField] private Texture[] texturesBackground;
    [SerializeField] private EventManager eventManager;
    
    [SerializeField] private MissionTrigger[] missionTriggersChild;
    [SerializeField] private MissionTrigger[] missionTriggersAdo;
    [SerializeField] private GameObject[] missionDescriptionChild;
    [SerializeField] private GameObject[] missionDescriptionAdo;
    
    private int age = 0;
    private int actualMission = 0;
    private bool[] randomMissionsProgressChild;
    private bool[] randomMissionsProgressAdo;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomMissionsProgressChild = new bool[missionTriggersChild.Length];
        randomMissionsProgressAdo = new bool[missionTriggersAdo.Length];
    }

    private int SelectRandomMissionChild()
    {
        // Calculate number of available events
        int cpt = 0;
        for (int i = 0; i < randomMissionsProgressChild.Length; i++)
        {
            if (!randomMissionsProgressChild[i])
            {
                cpt++;
            }
        }
        
        // case we don't have any more event available
        if (cpt == 0)
        {
            return Random.Range(0, randomMissionsProgressChild.Length);
        }
        
        // create array of number available missions
        int[] indexUnused = new int[cpt];
        int index = 0;
        for (int i = 0; i < randomMissionsProgressChild.Length; i++)
        {
            if (!randomMissionsProgressChild[i])
            {
                indexUnused[index] = i;
                index++;
            }
        }
        
        // get random event in array of available events 
        int rep = indexUnused[Random.Range(0, indexUnused.Length)];
        randomMissionsProgressChild[rep] = true;
        return rep;
    }
    
    private int SelectRandomMissionAdo()
    {
        // Calculate number of available events
        int cpt = 0;
        for (int i = 0; i < randomMissionsProgressAdo.Length; i++)
        {
            if (!randomMissionsProgressAdo[i])
            {
                cpt++;
            }
        }
        
        // case we don't have any more event available
        if (cpt == 0)
        {
            return Random.Range(0, randomMissionsProgressAdo.Length);
        }
        
        // create array of number available missions
        int[] indexUnused = new int[cpt];
        int index = 0;
        for (int i = 0; i < randomMissionsProgressAdo.Length; i++)
        {
            if (!randomMissionsProgressAdo[i])
            {
                indexUnused[index] = i;
                index++;
            }
        }
        
        // get random event in array of available events 
        int rep = indexUnused[Random.Range(0, indexUnused.Length)];
        randomMissionsProgressAdo[rep] = true;
        return rep;
    }

    public void ShowRandomMissionGlobal()
    {
        if (age == 0)
        {
            ShowRandomMissionChild();
        }
        else
        {
             ShowRandomMissionAdo();
        }
    }
    public void ShowRandomMissionChild()
    {
        int i = SelectRandomMissionChild(); 
        missionDescriptionChild[i].SetActive(true);
        missionTriggersChild[i].gameObject.SetActive(true);
    }
    
    public void ShowRandomMissionAdo()
    {
        int i = SelectRandomMissionAdo(); 
        missionDescriptionAdo[i].SetActive(true);
        missionTriggersAdo[i].gameObject.SetActive(true);
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
        else if (actualMission < numberMissionChild)
        {
            eventManager.ShowRandomEventChild();
        }
        else
        {
            eventManager.ShowRandomEventAdo();
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
