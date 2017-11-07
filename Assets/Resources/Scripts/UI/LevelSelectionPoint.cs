using UnityEngine;

public class LevelSelectionPoint : MonoBehaviour
{
    [Header("Level to load if pressed (- if no level):")]
    public sbyte level = -1;

    [Header("Where to move in each direction:")]
    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;
}
