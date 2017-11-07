using UnityEngine;

public class ChemicalTableBackButton : MonoBehaviour
{
    public void BackButtonPressed()
    {
        Transition.Instance.MakeTransitionTo("Level Selection");
    }
}
