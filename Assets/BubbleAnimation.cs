using UnityEngine;

public class BubbleAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip clipSpawn;
    [SerializeField] private AudioClip clipPop;
    
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void SpawnBubble()
    {
        GetComponent<Animator>().Play("Spawn");
        if (audioSource) audioSource.PlayOneShot(clipSpawn);
    }

    public void DestroyBubble()
    {
        GetComponent<Animator>().Play("Pop");
        if (audioSource) audioSource.PlayOneShot(clipPop);
    }

    public void ActiveBubble()
    {
        gameObject.SetActive(true);
    }
    public void DeactiveBubble()
    {
        gameObject.SetActive(false);
    }
    
    
}
