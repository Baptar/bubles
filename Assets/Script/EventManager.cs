using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject[] RandomEvents;
    [SerializeField] private GameObject[] SpecialSpeech;
    [SerializeField] private GameManager gameManager;
    private bool[] randomEventsProgress;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomEventsProgress = new bool[RandomEvents.Length];
        foreach (GameObject go in RandomEvents) go.SetActive(false);
        foreach (GameObject go in SpecialSpeech) go.SetActive(false);
    }

    public void ShowRandomEvent()
    {
        // Calculate number of available events
        int cpt = 0;
        for (int i = 0; i < randomEventsProgress.Length; i++)
        {
            if (!randomEventsProgress[i])
            {
                cpt++;
            }
        }
        
        // case we don't have any more event available
        if (cpt == 0)
        {
            RandomEvents[Random.Range(0, randomEventsProgress.Length)].SetActive(true);
            return;
        }
        
        // create array of number available missions
        int[] indexUnused = new int[cpt];
        int index = 0;
        for (int i = 0; i < randomEventsProgress.Length; i++)
        {
            if (!randomEventsProgress[i])
            {
                indexUnused[index] = i;
                index++;
            }
        }
        
        // get random event in array of available events 
        int rep = indexUnused[Random.Range(0, indexUnused.Length)];
        randomEventsProgress[rep] = true;
        RandomEvents[rep].SetActive(true);
    }
    
    public void ShowSpecialSpeech(int i) { SpecialSpeech[i].SetActive(true); }
    
    
    ////////// 
    ///Special Speech
    /////////

    public void OnSpecialBubbleFinished(int i)
    {
        switch (i)
        {
            case 0:
                ShowRandomEvent();
                break;
            case 1:
                ShowRandomEvent();
                break;
            case 2:
                gameManager.GameWin();
                break;
            default:
                ShowRandomEvent();
                break;
        }
    }
    
    
    
    ////////// 
    ///random Speech
    /////////
}
