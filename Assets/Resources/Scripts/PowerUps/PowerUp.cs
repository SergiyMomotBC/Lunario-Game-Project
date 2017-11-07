using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour
{
    public Sprite powerUpIcon;
    private const float DURATION = 15.0f;
    private float timeLeft;
    private GameObject powerUpBar;
    private Slider timeLeftBar;

	public void Awake()
    {
        timeLeft = DURATION;

        powerUpBar = Instantiate(Resources.Load("Prefabs/UI/PowerUpBar") as GameObject);
        powerUpBar.transform.SetParent(GameObject.Find("Panel").transform, false);

        timeLeftBar = powerUpBar.GetComponentInChildren<Slider>();
        timeLeftBar.value = 1.0f;

        OnStart();
	}

    public void Start()
    {
        var icon = powerUpBar.transform.Find("PowerUp Icon").GetComponent<Image>();
        icon.sprite = powerUpIcon;
    }
	
	public void Update()
    {
        timeLeft -= Time.deltaTime;

        timeLeftBar.value = timeLeft / DURATION;

        if(timeLeft <= 0.0f) {
            OnTimerEnd();
            Destroy(powerUpBar);
            Destroy(this);
        }

        OnUpdate();
	}

    protected abstract void OnStart();
    protected abstract void OnUpdate();
    protected abstract void OnTimerEnd();
}
