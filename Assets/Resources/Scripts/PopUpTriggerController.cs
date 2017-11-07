using UnityEngine;

public class PopUpTriggerController : MonoBehaviour
{
    public string title;
    public string message;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameplayManager.PopUp.PresentMessage(title, message);
            Destroy(gameObject);
        }
    }
}
