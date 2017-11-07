using UnityEngine;
using UnityEngine.EventSystems;

public class DisableMouseInput : MonoBehaviour
{
    private GameObject lastSelect;

    public void Awake()
    {
        lastSelect = new GameObject();
    }
    
    public void Update()
    {
        if (EventSystem.current == null)
            return;

        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(lastSelect);
        else
            lastSelect = EventSystem.current.currentSelectedGameObject;
    }
}
