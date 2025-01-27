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
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform cameraSpawnPoint;
    [SerializeField] private Transform cameraHallPosition;
    [SerializeField] private TopText _topText;
    [SerializeField] private MissionTrigger TriggerOnBoarding;
    
    public bool gameStarted = false;
    private int age = 0;
    private int actualMission = -1;
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
        Debug.Log(i);
        missionDescriptionChild[i].SetActive(true);
        missionDescriptionChild[i].GetComponent<BubbleAnimation>().SpawnBubble();
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
        
        for (int i = 0; i < missionDescriptionAdo.Length; i++) missionDescriptionAdo[i].SetActive(false);
        for (int i = 0; i < missionDescriptionChild.Length; i++) missionDescriptionChild[i].SetActive(false);
        
        // finished inboarding
        if (actualMission == 0)
        {
            eventManager.ShowSpecialSpeech(0);
            return;
        }
        
        playerBubble.OnMissionSuccess();
        camera.transform.position = cameraHallPosition.position;
        
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
        playerBubble.SetAccessoryIndex(0);
        this.age = age;
        _topText.SetAge(age);
        // faire la transition d'un age à un autre ICI
        
        background.GetComponent<RawImage>().texture = texturesBackground[age];
    }
    
    public void RestartGame()
    {
        _topText.ResetPositionImage();
        StopFreeze();
        camera.transform.position = cameraSpawnPoint.position;
        TriggerOnBoarding.gameObject.SetActive(true);
        TriggerOnBoarding.RestartGame();
        
        SetAge(0);
        actualMission = -1;
        for (int i = 0; i < missionDescriptionAdo.Length; i++) missionDescriptionAdo[i].SetActive(false);
        for (int i = 0; i < missionDescriptionChild.Length; i++) missionDescriptionChild[i].SetActive(false);
        for (int i = 0; i < missionTriggersAdo.Length; i++)
        {
            missionTriggersAdo[i].gameObject.SetActive(false);
            missionTriggersAdo[i].RestartGame();
        }

        for (int i = 0; i < missionTriggersChild.Length; i++)
        {
            missionTriggersChild[i].gameObject.SetActive(false);
            missionTriggersChild[i].RestartGame();
        }
        for (int i = 0; i < eventManager.randomEventsProgressChild.Length; i++) eventManager.randomEventsProgressChild[i] = false;
        for (int i = 0; i < eventManager.randomEventsProgressAdo.Length; i++) eventManager.randomEventsProgressAdo[i] = false;
    }

    public void GameWin()
    {
        SceneManager.instance.LoadMenu();
    }

    public void StopFreeze()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        StopFreeze();
    }

    public void SetGameStarted()
    {
        gameStarted = true;
    }
}
