using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
   [SerializeField] private float timeToTrigger = 2f;
   [SerializeField] private GameManager gameManager;
   
   private bool triggered = false;
   private float timer = 0f;
   private bool missionFinished = false;

   private void OnTriggerEnter2D(Collider2D other)
   {
      triggered = true;
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      triggered = false;
      timer = 0f;
   }

   void Update()
   {
      if (missionFinished) return;
      
      if (triggered)
      {
         timer += Time.deltaTime;
         if (timer >= timeToTrigger)
         {
            missionFinished = true;
            Time.timeScale = 0f;
            gameManager.OnMissionComplete();
            gameObject.SetActive(false);
         }
      }
   }
}
