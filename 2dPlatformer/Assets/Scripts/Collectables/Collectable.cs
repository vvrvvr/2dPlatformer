using UnityEngine;

public class Collectable : MonoBehaviour
{
   private bool isTouched = false;
   [SerializeField] private int score = 1;
    
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.name == "character" && !isTouched)
      {
         isTouched = true;
         Debug.Log("collected");
         PlayerStats.Singleton.UpdateScore(score);
        Destroy(gameObject);
      }
   }
}
