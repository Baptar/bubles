using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject[] RandomEventsChild;
    [SerializeField] private GameObject[] RandomEventsAdo;
    [SerializeField] private GameObject[] SpecialSpeech;
    [SerializeField] private GameObject onBoardingSpeech;
    [SerializeField] private GameManager gameManager;
    public bool[] randomEventsProgressChild;
    public bool[] randomEventsProgressAdo;
    
    void Start()
    {
        randomEventsProgressChild = new bool[RandomEventsChild.Length];
        randomEventsProgressAdo = new bool[RandomEventsAdo.Length];
        foreach (GameObject go in RandomEventsChild) go.SetActive(false);
        foreach (GameObject go in RandomEventsAdo) go.SetActive(false);
        foreach (GameObject go in SpecialSpeech) go.SetActive(false);
    }

    public void ShowOnBoardingSpeech()
    {
        onBoardingSpeech.SetActive(true);
    }
    
    ////////// 
    ///random Speech
    /////////
    public void ShowRandomEventChild()
    {
        // Calculate number of available events
        int cpt = 0;
        for (int i = 0; i < randomEventsProgressChild.Length; i++)
        {
            if (!randomEventsProgressChild[i])
            {
                cpt++;
            }
        }
        
        // case we don't have any more event available
        if (cpt == 0)
        {
            RandomEventsChild[Random.Range(0, randomEventsProgressChild.Length)].SetActive(true);
            return;
        }
        
        // create array of number available missions
        int[] indexUnused = new int[cpt];
        int index = 0;
        for (int i = 0; i < randomEventsProgressChild.Length; i++)
        {
            if (!randomEventsProgressChild[i])
            {
                indexUnused[index] = i;
                index++;
            }
        }
        
        // get random event in array of available events 
        int rep = indexUnused[Random.Range(0, indexUnused.Length)];
        randomEventsProgressChild[rep] = true;
        RandomEventsChild[rep].SetActive(true);
    }
    
    public void ShowRandomEventAdo()
    {
        // Calculate number of available events
        int cpt = 0;
        for (int i = 0; i < randomEventsProgressAdo.Length; i++)
        {
            if (!randomEventsProgressAdo[i])
            {
                cpt++;
            }
        }
        
        // case we don't have any more event available
        if (cpt == 0)
        {
            RandomEventsAdo[Random.Range(0, randomEventsProgressAdo.Length)].SetActive(true);
            return;
        }
        
        // create array of number available missions
        int[] indexUnused = new int[cpt];
        int index = 0;
        for (int i = 0; i < randomEventsProgressAdo.Length; i++)
        {
            if (!randomEventsProgressAdo[i])
            {
                indexUnused[index] = i;
                index++;
            }
        }
        
        // get random event in array of available events 
        int rep = indexUnused[Random.Range(0, indexUnused.Length)];
        randomEventsProgressAdo[rep] = true;
        RandomEventsAdo[rep].SetActive(true);
    }
    
    
    ////////// 
    ///Special Speech
    /////////
    public void ShowSpecialSpeech(int i)
    {
        SpecialSpeech[i].SetActive(true);
        SpecialSpeech[i].GetComponent<BubbleAnimation>().SpawnBubble();
    }
    
    public void OnSpecialBubbleFinished(int i)
    {
        switch (i)
        {
            case 0:
                gameManager.ShowRandomMissionChild();
                break;
            case 1:
                gameManager.ShowRandomMissionAdo();
                break;
            case 2:
                gameManager.GameWin();
                break;
            default:
                gameManager.ShowRandomMissionChild();
                break;
        }
    }

    public void RemoveAccessory() { gameManager.playerBubble.SetAccessoryIndex(0); }
    public void AccessoryLunette() { gameManager.playerBubble.SetAccessoryIndex(1); } 
    public void AccessoryUWU(){gameManager.playerBubble.SetAccessoryIndex(2); }
    public void AccessoryNoeud(){gameManager.playerBubble.SetAccessoryIndex(3); }
    public void AccessoryGene(){gameManager.playerBubble.SetAccessoryIndex(4); }
    public void AccessoryAlien(){gameManager.playerBubble.SetAccessoryIndex(5); }
    public void AccessoryMom(){gameManager.playerBubble.SetAccessoryIndex(6); }
    public void AccessoryDad(){gameManager.playerBubble.SetAccessoryIndex(7); }
}
