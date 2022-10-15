using UnityEngine;

public class Collectable : MonoBehaviour
{
   private bool isTouched = false;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("player") && !isTouched)
      {
         isTouched = true;
         Debug.Log("collected");
         //PlayerStats.Singleton.currentScore += 1;
         
      }
   }  
}
