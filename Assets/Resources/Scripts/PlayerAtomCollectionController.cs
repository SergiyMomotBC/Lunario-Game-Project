using UnityEngine;
using System.Collections.Generic;

public class PlayerAtomCollectionController : MonoBehaviour
{
	private Dictionary<int, int> atomsCollection;

    public int collectedAtomsCount { get; private set; } 

	public void Awake ()
    {
		atomsCollection = new Dictionary<int, int>();
        collectedAtomsCount = 0;
	}

	public void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.CompareTag("Atom"))
		{	
			var key = other.gameObject.GetComponent<AtomInfo>().index;
			if (atomsCollection.ContainsKey(key))
				atomsCollection[key]++;
			else
				atomsCollection.Add(key, 1);

            collectedAtomsCount++;

            var sound = other.GetComponent<AudioSource>();
            sound.Play();

            other.GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<BoxCollider2D>().enabled = false;

            Destroy(other.GetComponentInChildren<ParticleSystem>().gameObject);
            Destroy(other.gameObject, sound.clip.length + 0.1f);
		}
	}

	public bool IsFormulaCollected(Dictionary<int, int> neededAtoms)
	{
		foreach (var atom in neededAtoms) 
			if (!atomsCollection.ContainsKey(atom.Key) || atomsCollection[atom.Key] < atom.Value)
				return false;

		return true;
	}
}
