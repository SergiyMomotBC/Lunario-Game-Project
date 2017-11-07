using UnityEngine;

public class DamageByTouch : MonoBehaviour 
{
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerLifeController>().WasHurt();
    }
}
