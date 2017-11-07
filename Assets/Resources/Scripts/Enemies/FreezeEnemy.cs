using UnityEngine;

public class FreezeEnemy : MonoBehaviour
{
    public bool useAutomaticFreezing = false;

    private MonoBehaviour[] behaviours;
    private Rigidbody2D usedBody;
    private SpriteRenderer usedSprite;
    private Animator usedAnimator;
    private bool isFreezed;
	
	public void Awake()
    {
        usedBody = GetComponent<Rigidbody2D>();
        usedSprite = GetComponent<SpriteRenderer>();
        behaviours = GetComponents<MonoBehaviour>();
        usedAnimator = GetComponent<Animator>();
    }
	
	public void Update()
    {
        if (!useAutomaticFreezing)
            return;

        if (!isFreezed && !usedSprite.isVisible) {
            isFreezed = true;
            Freeze();
        }

        if(isFreezed && usedSprite.isVisible) {
            isFreezed = false;
            Unfreeze();
        }
	}

    public void Freeze()
    {
        if (usedBody != null)
            usedBody.velocity = Vector3.zero;

        if (usedAnimator != null)
            usedAnimator.enabled = false;

        foreach (MonoBehaviour behaviour in behaviours)
            if(behaviour != null)
                behaviour.enabled = false;

        this.enabled = true;

        if(usedSprite != null)
            usedSprite.enabled = true;
    }

    public void Unfreeze()
    {
        if (usedAnimator != null)
            usedAnimator.enabled = true;

        foreach (MonoBehaviour behaviour in behaviours)
            behaviour.enabled = true;
    }
}
