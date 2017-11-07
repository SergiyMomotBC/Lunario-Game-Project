using UnityEngine;
using System.Collections.Generic;

public class RuneController : MonoBehaviour
{
    public Sprite sprite;
    public int indexOfElementItUnlocks;
    public int[] atomsRequirements;

    public int totalAtomsNeeded
    {
        get 
        {
            int sum = 0;
            foreach (var atom in atomsNeeded)
                sum += atom.Value;

            return sum;
        }
    }

    private Dictionary<int, int> atomsNeeded;

	public void Awake()
    {
        if(sprite != null)
            GetComponent<SpriteRenderer>().sprite = sprite;

        atomsNeeded = new Dictionary<int, int>();
        for (int i = 0; i < atomsRequirements.Length; i += 2)
            atomsNeeded.Add(atomsRequirements[i], atomsRequirements[i + 1]);
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerAtomCollectionController>().IsFormulaCollected(atomsNeeded))
               // GameplayManager.PopUp.PresentMessage(Game.Atoms[indexOfElementItUnlocks].name + " Unlocked", Game.Atoms[indexOfElementItUnlocks].description);
                FindObjectOfType<GameplayManager>().LevelCompleted();
            else
                FindObjectOfType<PlayerLifeController>().OnDeath(2);
        }
    }
}
